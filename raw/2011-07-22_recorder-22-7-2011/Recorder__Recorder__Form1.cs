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

//a simple recorder, 8-bit per sample, with one channel.
//by Hila Shmuel
// 
namespace Recorder
{
    enum errors_e :int
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
        
        WAVEINCAPS      WaveInCaps;
        WAVEFORMATEX    WaveFormat;
        WAVEHDR         WaveHdr;

        byte[] WaveArray = new byte[NumberOfSamples];// byte = for 8-bit samle
        byte[] buffer = new byte[NumberOfSamples];

        // varibles for the frequency analysis
        double[] REX    = new double[NumberOfSamples + 1];
        double[] IMX    = new double[NumberOfSamples + 1 ];
        double[] output = new double[NumberOfSamples + 1];
        double   maxvalue = 0;
        //double fit = 0;

        // graphics
        Graphics objGraphic1;
        Graphics objGraphic2;
        Pen pen;

        ABCHandler abc_handler;

        public Form1()
        {
            InitializeComponent();
            timeDomain.BackColor = System.Drawing.Color.LightGray;
            frequencyDomain.BackColor = System.Drawing.Color.LightGray;
            start_record.Enabled = false;
            stop_record.Enabled = false;
             objGraphic1 = timeDomain.CreateGraphics();
             objGraphic2 = frequencyDomain.CreateGraphics();
             pen = new Pen(Color.Red);
             abc_handler = new ABCHandler();

            // STEP 1: get the number of waveform-audio input devices present in the system.
            int NumOfDevs = WinMM.waveInGetNumDevs();

            if (NumOfDevs <= 0)
            {
                MessageBox.Show("No microphone detected!\nPlease connect a microphone and run the program again.","Recorder By Hila Shmuel");
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

        private void start_record_Click(object sender, EventArgs e)
        {
            InputDevice.Enabled = false;
            start_record.Enabled = false;
            stop_record.Enabled = true;

            // init the WAVEFORMAT structure
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
            if (result!=WinMM.MMSYSERR_NOERROR)
            {
                MessageBox.Show("Failed to open sound input device!", "Recorder By Hila Shmuel");
                InputDevice.Enabled = true;
                start_record.Enabled = false;
                stop_record.Enabled = false;
                return;
            }

            // STEP 4: start the device. At this point, the sound bytes will not reach your program
            result = WinMM.waveInStart(hWaveIn);
            if (result!=WinMM.MMSYSERR_NOERROR)
            {
                MessageBox.Show("Failed to start recording!", "Recorder By Hila Shmuel");
                InputDevice.Enabled = true;
                start_record.Enabled = false;
                stop_record.Enabled = false;
                return;
            }

            // make DataPtr a pointer to the buffer array, where the device will put the samples
            unsafe {
                fixed (byte* p = buffer)
                {
                    DataPtr = (IntPtr)p;
                }
            }

            WaveHdr.lpData = DataPtr;
            WaveHdr.dwBufferLength = (int)NumberOfSamples * WaveFormat.nBlockAlign; // length in bytes
            WaveHdr.dwFlags = 0;

            // STEP 5: waveInPrepareHeader and  waveInAddBuffer to allow Windows to stream the sound bytes to your own buffer
            unsafe
            {
                result = WinMM.waveInPrepareHeader(hWaveIn, ref WaveHdr, sizeof(WAVEHDR));
            }
            if (result != WinMM.MMSYSERR_NOERROR)
            {
                MessageBox.Show("Failed to prepare buffer!", "Recorder By Hila Shmuel");
                InputDevice.Enabled = true;
                start_record.Enabled = false;
                stop_record.Enabled = false;
                return;
            }

            result = WinMM.waveInAddBuffer(hWaveIn, ref WaveHdr, Marshal.SizeOf(WaveHdr));
            if (result != WinMM.MMSYSERR_NOERROR)
            {
                MessageBox.Show("Failed to add buffer!", "Recorder By Hila Shmuel");
                InputDevice.Enabled = true;
                start_record.Enabled = false;
                stop_record.Enabled = false;
                return;
            }


            // create & open for writing a temporery ABC music sheet file
            abc_handler.init();

            timer1.Enabled = true;
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

        private void stop_record_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            InputDevice.Enabled = true;
            start_record.Enabled = true;
            stop_record.Enabled = false;

            // STEP (end): stop the device, and close it.
            WinMM.waveInStop(hWaveIn);
            result = WinMM.waveInClose(hWaveIn);
            WinMM.waveInUnprepareHeader(hWaveIn, ref WaveHdr, Marshal.SizeOf(WaveHdr));
            if (result != WinMM.MMSYSERR_NOERROR)
            {
                MessageBox.Show("Failed to close the device!", "Recorder By Hila Shmuel");
                InputDevice.Enabled = true;
                start_record.Enabled = false;
                stop_record.Enabled = false;
                return;
            }

            // TODO: close & delete temporery file for ABC shit
            abc_handler.deleteABCfile();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            //Copy the data from the buffer to WaveArray 
            //while (((WaveHdr.dwFlags & WinMM.WHDR_DONE) != WinMM.WHDR_DONE) && (hWaveIn.Equals(IntPtr.Zero)==false)) ;
            while ((WaveHdr.dwFlags & WinMM.WHDR_DONE) != WinMM.WHDR_DONE);
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

             //print time domain
            int picBoxWidth = timeDomain.Size.Width;
            int picBoxHeight = timeDomain.Size.Height;
            int halfWidth = timeDomain.Size.Width / 2;
            int halfHeight = timeDomain.Size.Height / 2;

            pen.Color = Color.Red;
            objGraphic1.Clear(Color.LightGray);
            objGraphic1.DrawLine(pen, 0, halfHeight, picBoxWidth, halfHeight);
            pen.Color = Color.Black;
            for (int j = 0; j < 511; j++) // 512 = picture box width
            {
                objGraphic1.DrawLine(pen, j, picBoxHeight - (WaveArray[j] / (256/ picBoxHeight)), j + 1, picBoxHeight - (WaveArray[j + 1] / (256 / picBoxHeight)));
            }

            //print frequency domain    
            /*
            picBoxWidth = frequencyDomain.Size.Width;
            picBoxHeight = frequencyDomain.Size.Height;
            halfWidth = frequencyDomain.Size.Width / 2;
            halfHeight = frequencyDomain.Size.Height / 2;

            objGraphic2 = frequencyDomain.CreateGraphics();

            objGraphic2.Clear(Color.LightGray);
            pen.Color = Color.Black;
                fit = maxvalue / picBoxHeight;
            for (int j = 0; j < 511; j++)
            {
                // for FFT use the first line. for DFT, use the second
                objGraphic2.DrawLine(pen, j, picBoxHeight - (int)(REX[j] / fit), j + 1, picBoxHeight - (int)(REX[j+1] / fit));
                //objGraphic2.DrawLine(pen, j, picBoxHeight - (int)(output[j] / fit), j + 1, picBoxHeight - (int)(output[j+1] / fit));
            }
            */

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
                abc_handler.ConvertToPDF();
                // load pdf to screen
                axAcroPDF1.LoadFile("c:\\file.pdf");
            }

            timer1.Enabled = true;
        }
    }
}
