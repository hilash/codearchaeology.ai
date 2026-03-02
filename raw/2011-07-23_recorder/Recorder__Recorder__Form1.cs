using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using Win32;
using System.Threading;
using System.Reflection;

//a simple recorder, 8-bit per sample, with one channel.
//by Hila Shmuel
// 
namespace Recorder
{
    enum errors_e : int
    {
        ERROR_RECORDER_NO_MIC_DETECTED = 0x1000
    };

    public partial class Form1 : Form
    {
        const uint NumberOfSamples = 512;
        const uint SamplesPerSec = 4000;
        int result;

        IntPtr hWaveIn = IntPtr.Zero;
        IntPtr dwCallback = IntPtr.Zero;
        IntPtr DataPtr;

        WAVEINCAPS WaveInCaps;
        WAVEFORMATEX WaveFormat;
        WAVEHDR WaveHdr;

        byte[] WaveArrayBuffer = new byte[NumberOfSamples];// byte = for 8-bit samle
        byte[] WaveArray = new byte[NumberOfSamples];// byte = for 8-bit samle - copy for graphics
        byte[] buffer = new byte[NumberOfSamples];

        // varibles for the frequency analysis
        double[] REX = new double[NumberOfSamples + 1];
        double[] IMX = new double[NumberOfSamples + 1];
        double[] output = new double[NumberOfSamples + 1];

        // graphics
        int[] WaveArrayNormalized = new int[NumberOfSamples];
        Pen RedPen = new Pen(Color.Red);
        Pen BlackPen = new Pen(Color.Black); //TODO: where to init this?
        
        Graphics timeDomainGraphics;
        Graphics timeDomainBufferGraphics;
        Image timeDomainBuffer;
        int timeDomainWidth;
        int timeDomainHeight;
        int timeDomainHalfHeight;
        
        Graphics frequencyDomainGraphics;
        Graphics frequencyDomainBufferGraphics;
        Image frequencyDomainBuffer;
        int frequencyDomainWidth;
        int frequencyDomainHeight;
        int frequencyDomainHalfHeight;

        // notes
        ABCHandler abc_handler = new ABCHandler();

        // threads synchronization
        bool is_recording = false;
        Mutex WaveArrayBufferAvailable;
        Mutex RecordingLoopDone;
        Mutex DrawingLoopDone;

        public Form1()
        {
            InitializeComponent();
            
            start_record.Enabled = false;
            stop_record.Enabled = false;

            useFFT.Checked = true;
            
            //graphics
            timeDomain.BackColor = System.Drawing.Color.LightGray;
            timeDomainGraphics = timeDomain.CreateGraphics();
            timeDomainWidth = timeDomain.Size.Width;
            timeDomainHeight = timeDomain.Size.Height;
            timeDomainHalfHeight = timeDomain.Size.Height / 2;
            timeDomainBuffer = new Bitmap(timeDomainWidth, timeDomainHeight);
            timeDomainBufferGraphics = Graphics.FromImage(timeDomainBuffer);

            frequencyDomain.BackColor = System.Drawing.Color.LightGray;
            frequencyDomainGraphics = frequencyDomain.CreateGraphics();
            frequencyDomainWidth = frequencyDomain.Size.Width;
            frequencyDomainHeight = frequencyDomain.Size.Height;
            frequencyDomainHalfHeight = frequencyDomain.Size.Height / 2;
            frequencyDomainBuffer = new Bitmap(frequencyDomainWidth, frequencyDomainHeight);
            frequencyDomainBufferGraphics = Graphics.FromImage(frequencyDomainBuffer);

            WaveArrayBufferAvailable = new Mutex(false);
            RecordingLoopDone = new Mutex(false);
            DrawingLoopDone = new Mutex(false);
           
            // STEP 1: get the number of waveform-audio input devices present in the system.
            int NumOfDevs = WinMM.waveInGetNumDevs();

            if (NumOfDevs <= 0)
            {
                MessageBox.Show("No microphone detected!\nPlease connect a microphone and run the program again.", "Recorder By Hila Shmuel");
                Environment.Exit((int)errors_e.ERROR_RECORDER_NO_MIC_DETECTED);
            }
            else
            {
                // STEP 2: let the user select a sound input device
                InputDevice.Items.Add("Choose Input Device");
                InputDevice.SelectedIndex = 0;
                InputDevice.Show();
                for (int i = 1; i <= NumOfDevs; i++)
                {
                    WinMM.waveInGetDevCaps(i - 1, ref WaveInCaps, Marshal.SizeOf(WaveInCaps));
                    InputDevice.Items.Add(WaveInCaps.szPname);
                }
            }
        }

        private void InputDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (InputDevice.SelectedIndex == 0)
            {
                start_record.Enabled = false;
                stop_record.Enabled = false;
            }
            else
            {
                start_record.Enabled = true;
                stop_record.Enabled = false;
            }
        }

        private void start_record_Click(object sender, EventArgs e)
        {
            InputDevice.Enabled = false;
            start_record.Enabled = false;
            stop_record.Enabled = true;

            // initialize the WAVEFORMAT structure
            WaveFormat.wFormatTag = WinMM.WAVE_FORMAT_PCM;
            WaveFormat.nChannels = 1;
            WaveFormat.nSamplesPerSec = SamplesPerSec;
            WaveFormat.wBitsPerSample = 8;
            WaveFormat.nBlockAlign = (short)((WaveFormat.nChannels) * (WaveFormat.wBitsPerSample / 8)); //= 2, number of bytes per sample
            WaveFormat.nAvgBytesPerSec = (uint)(WaveFormat.nSamplesPerSec * WaveFormat.nBlockAlign); //  SamplesPerSec; 
            WaveFormat.cbSize = 0;

            // STEP 3: open the selected input device
            //result = WinMM.waveInOpen(ref hWaveIn, (uint)(InputDevice.SelectedIndex - 1), ref WaveFormat, dwCallback, 0, WinMM.CALLBACK_NULL);
            result = WinMM.waveInOpen(ref hWaveIn, (uint)(InputDevice.SelectedIndex - 1), ref WaveFormat, 0, 0, 0);
            if (result != WinMM.MMSYSERR_NOERROR)
            {
                MessageBox.Show("Failed to open sound input device!", "Recorder By Hila Shmuel");
                InputDevice.Enabled = true;
                start_record.Enabled = false;
                stop_record.Enabled = false;
                return;
            }

            // STEP 4: start the device. At this point, the sound bytes will not reach your program
            result = WinMM.waveInStart(hWaveIn);
            if (result != WinMM.MMSYSERR_NOERROR)
            {
                MessageBox.Show("Failed to start recording!", "Recorder By Hila Shmuel");
                InputDevice.Enabled = true;
                start_record.Enabled = false;
                stop_record.Enabled = false;
                return;
            }

            // make DataPtr a pointer to the buffer array, where the device will put the samples
            unsafe
            {
                fixed (byte* p = buffer)
                {
                    DataPtr = (IntPtr)p;
                }
            }

            // create & open for writing a temporery ABC music sheet file
            abc_handler.init();

            // create 2 threads: one for recording a buffer, the other for drawing it
            is_recording = true;

            ThreadStart recordingLoopThreadStart = new ThreadStart(recording_loop);
            Thread recordingLoopThread = new Thread(recordingLoopThreadStart);
            recordingLoopThread.Start();

            ThreadStart drawingLoopThreadStart = new ThreadStart(drawing_loop);
            Thread drawingLoopThread = new Thread(drawingLoopThreadStart);
            drawingLoopThread.Start();
        }

        private void recording_loop() // TODO: add mutex!
        {
            while (is_recording)
            {
                RecordingLoopDone.WaitOne();

                WaveHdr.lpData = DataPtr;
                WaveHdr.dwBufferLength = (int)NumberOfSamples * WaveFormat.nBlockAlign; // length in bytes
                WaveHdr.dwFlags = 0;

                // STEP 5: Call wave InPrepareHeader and  waveInAddBuffer to allow Windows to stream the sound bytes to your own buffer
                int result = WinMM.waveInPrepareHeader(hWaveIn, ref WaveHdr, Marshal.SizeOf(WaveHdr));
                if (result != WinMM.MMSYSERR_NOERROR)
                {
                    MessageBox.Show("Failed to prepare buffer!", "Recorder By Hila Shmuel");
                    InputDevice.Enabled = true;
                    start_record.Enabled = false;
                    stop_record.Enabled = false;
                    RecordingLoopDone.ReleaseMutex();
                    return;
                }

                result = WinMM.waveInAddBuffer(hWaveIn, ref WaveHdr, Marshal.SizeOf(WaveHdr));
                if (result != WinMM.MMSYSERR_NOERROR)
                {
                    MessageBox.Show("Failed to add buffer!", "Recorder By Hila Shmuel");
                    InputDevice.Enabled = true;
                    start_record.Enabled = false;
                    stop_record.Enabled = false;
                    RecordingLoopDone.ReleaseMutex();
                    return;
                }

                // wait for the API buffer to fill
                while (((WaveHdr.dwFlags & WinMM.WHDR_DONE) != WinMM.WHDR_DONE) && (hWaveIn.Equals(IntPtr.Zero) == false)) ;

                // copy the data from the API buffer to WaveArrayBuffer, when available
                WaveArrayBufferAvailable.WaitOne();
                Marshal.Copy(DataPtr, WaveArrayBuffer, 0, (int)NumberOfSamples);
                WaveArrayBufferAvailable.ReleaseMutex();

                RecordingLoopDone.ReleaseMutex();
            }
        }

        private void drawing_loop()
        {
            while (is_recording)
            {
                DrawingLoopDone.WaitOne();

                // create a copy for WaveArrayBuffer, to use whithin this function
                WaveArrayBufferAvailable.WaitOne();
                WaveArrayBuffer.CopyTo(WaveArray, 0);
                WaveArrayBufferAvailable.ReleaseMutex();
                
                //calculate FFT
                for (int ip = 0; ip < NumberOfSamples; ip++)
                {
                    REX[ip] = WaveArray[ip];
                    IMX[ip] = 0;
                }

                int maxpitch = 0;
                double fit = 0;
                if (useFFT.Checked)
                {
                    maxpitch = DFT.FFT(REX, IMX, 512);
                    fit = REX[maxpitch] / frequencyDomainHeight;
                }
                else
                {
                    maxpitch = DFT.furier(WaveArray, REX, IMX, output, 512);
                    fit = output[maxpitch] / frequencyDomainHeight;
                }

                // TODO: add the invoke stuff, else - make the program crash sometimes.
                pitch.Text = maxpitch.ToString();
                
                // print time domain - using double buffering
                timeDomainBufferGraphics.Clear(Color.LightGray);
                timeDomainBufferGraphics.DrawLine(RedPen, 0, timeDomainHalfHeight, timeDomainWidth, timeDomainHalfHeight);
                for (int j = 0; j < timeDomainWidth; j++)
                {
                    WaveArrayNormalized[j] = timeDomainHeight - (int)(timeDomainHeight * (double)WaveArray[j] / byte.MaxValue);

                }
                for (int j = 0; j < timeDomainWidth - 1; j++)
                {
                    timeDomainBufferGraphics.DrawLine(BlackPen, j, WaveArrayNormalized[j], j + 1, WaveArrayNormalized[j + 1]);
                }
                timeDomainGraphics.DrawImage(timeDomainBuffer, 0, 0);
               
                
                //print frequency domain
                frequencyDomainBufferGraphics.Clear(Color.LightGray);
                if (fit != 0)
                {
                    for (int j = 0; j < frequencyDomainWidth / 2; j++)
                    {
                        if (useFFT.Checked)
                        {
                            frequencyDomainBufferGraphics.DrawLine(BlackPen, j * 2, frequencyDomainHeight - (int)(REX[j] / fit), (j + 1) * 2, frequencyDomainHeight - (int)(REX[j + 1] / fit));
                        }
                        else
                        {
                            frequencyDomainBufferGraphics.DrawLine(BlackPen, j * 2, frequencyDomainHeight - (int)(output[j] / fit), (j + 1) * 2, frequencyDomainHeight - (int)(output[j + 1] / fit));
                        }
                    }
                    frequencyDomainGraphics.DrawImage(frequencyDomainBuffer, 0, 0);
                }
                
                
                // determine to add new note to sheet
                if (true == abc_handler.addNoteToMusicSheet(maxpitch))
                {
                    //abc_handler.ConvertToPDF();
                    // load pdf to screen
                    //axAcroPDF1.LoadFile("c:\\file.pdf");
                }

                DrawingLoopDone.ReleaseMutex();
            }
        }

        private void stop_record_Click(object sender, EventArgs e)
        {
            // timer1.Enabled = false;
            InputDevice.Enabled = true;
            start_record.Enabled = true;
            stop_record.Enabled = false;

            is_recording = false;

            RecordingLoopDone.WaitOne();
            DrawingLoopDone.WaitOne();

            // STEP (end): stop the device, and close it.
            WinMM.waveInStop(hWaveIn);
            WinMM.waveInReset(hWaveIn);
            result = WinMM.waveInClose(hWaveIn);
            WinMM.waveInUnprepareHeader(hWaveIn, ref WaveHdr, Marshal.SizeOf(WaveHdr));
            if (result != WinMM.MMSYSERR_NOERROR)
            {
                MessageBox.Show("Failed to close the device!", "Recorder By Hila Shmuel");
                InputDevice.Enabled = true;
                start_record.Enabled = false;
                stop_record.Enabled = false;
                //timer enables == false
                return;
            }

            // can record again only if stop ended successfully
            RecordingLoopDone.ReleaseMutex();
            DrawingLoopDone.ReleaseMutex();

            // TODO: close & delete temporery file for ABC shit
            //abc_handler.deleteABCfile();
        }

        /*
        private void  timer1_Tick(object sender, EventArgs e)
        {
            // timer1.Enabled = false;

            //Copy the data from the buffer to WaveArray 
            while (((WaveHdr.dwFlags & WinMM.WHDR_DONE) != WinMM.WHDR_DONE) && (hWaveIn.Equals(IntPtr.Zero)==false)) ;
            //while ((WaveHdr.dwFlags & WinMM.WHDR_DONE) != WinMM.WHDR_DONE) ;
            Marshal.Copy(DataPtr, WaveArray, 0, (int)NumberOfSamples);

            //\ see big endian char copy
            WaveHdr.lpData = DataPtr;
            WaveHdr.dwBufferLength = (int)NumberOfSamples * WaveFormat.nBlockAlign; // length in bytes
            WaveHdr.dwFlags = 0;

            //calculate FFT
            for (int ip = 0; ip < NumberOfSamples; ip++)
            {
                REX[ip] = WaveArray[ip];
                IMX[ip] = 0;
            }

            // for FFT (much better!!!)use the first line. for DFT(called DFT.furier), use the second
            maxvalue = DFT.FFT(REX, IMX, 512);
            //maxvalue = DFT.furier(WaveArray, REX, IMX, output, 512);

            pitch.Text = maxvalue.ToString();

   
            // print time domain - using double buffering
            timeDomainBufferGraphics.Clear(Color.LightGray);
            timeDomainBufferGraphics.DrawLine(RedPen, 0, timeDomainHalfHeight, timeDomainWidth, timeDomainHalfHeight);
            for (int j = 0; j < timeDomainWidth; j++)
            {
                WaveArrayNormalized[j] = timeDomainHeight - (int)(timeDomainHeight * (double)WaveArray[j] / byte.MaxValue);
                
            }
            for (int j = 0; j < timeDomainWidth - 1; j++)
            {
                timeDomainBufferGraphics.DrawLine(black_pen, j, WaveArrayNormalized[j],
                                                          j + 1, WaveArrayNormalized[j + 1]);
            }
            timeDomainGraphics.DrawImage(timeDomainBuffer, 0, 0);
            

            //print frequency domain    
            
            timeDomainWidth = frequencyDomain.Size.Width;
            timeDomainHeight = frequencyDomain.Size.Height;
            halfWidth = fquencyDomain.Size.Width / 2;
            timeDomainHalfHeight = frequencyDomain.Size.Height / 2;

            objGraphic2 = frequencyDomain.CreateGraphics();

            objGraphic2.Clear(Color.LightGray);
            pen.Color = Color.Black;
                fit = maxvalue / timeDomainHeight;
            for (int j = 0; j < 511; j++)
            {
                // forre FFT use the first line. for DFT, use the second
                objGraphic2.DrawLine(pen, j, timeDomainHeight - (int)(REX[j] / fit), j + 1, timeDomainHeight - (int)(REX[j+1] / fit));
                //objGraphic2.DrawLine(pen, j, timeDomainHeight - (int)(output[j] / fit), j + 1, timeDomainHeight - (int)(output[j+1] / fit));
            }
            

            // STEP 5 again: Call wave InPrepareHeader and  waveInAddBuffer to allow Windows to stream the sound bytes to your own buffer
            int i = WinMM.waveInPrepareHeader(hWaveIn, ref WaveHdr, Marshal.SizeOf(WaveHdr));
            if (i != WinMM.MMSYSERR_NOERROR)
            {
                MessageBox.Show("Failed to prepare buffer!", "Recorder By Hila Shmuel");
                InputDevice.Enabled = true;
                start_record.Enabled = false;
                stop_record.Enabled = false;
                return;
            }

            i = WinMM.waveInAddBuffer(hWaveIn, ref WaveHdr, Marshal.SizeOf(WaveHdr));
            if (i != WinMM.MMSYSERR_NOERROR)
            {
                MessageBox.Show("Failed to add buffer!", "Recorder By Hila Shmuel");
                InputDevice.Enabled = true;
                start_record.Enabled = false;
                stop_record.Enabled = false;
                return;
            }

            // determine to add new note to sheet
            if (true == abc_handler.addNoteToMusicSheet((int)maxvalue))
            {
                //abc_handler.ConvertToPDF();
                // load pdf to screen
                //axAcroPDF1.LoadFile("c:\\file.pdf");
            }

            // timer1.Enabled = true;
        }
         */

    }
}
