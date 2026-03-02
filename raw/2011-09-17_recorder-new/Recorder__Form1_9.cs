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
using Wave;
using Analysis;

//a simple recorder, 8-bit per sample, with one channel.
//by Hila Shmuel
// 
namespace Recorder
{
    public partial class Form1 : Form
    {
        const uint NumberOfSamples = 512;
        const uint SamplesPerSec = 4000;

        // wave
        WaveIn wave;
        byte[] waveArray;
        double[] waveArrayFrequency;

        // graphics
        Graphics timeDomainGraphics; 
        Graphics frequencyDomainGraphics;

        // notes
        ABCHandler abc_handler;
        NotesDetector noteDetector;

        // threads synchronization
        ThreadStart recordingLoopThreadStart;
        Thread recordingLoopThread;
        bool is_recording;
        
        // delegates
        SetTextDelegate SetTextCallback;
        GetTrackBarValueDelegate GetTrackBarValueCallback;
        SetEnabledDelegate SetEnabledCallback;
        
        public Form1()
        {
            try
            {
                InitializeComponent();

                // UI elements
                start_record.Enabled = false;
                stop_record.Enabled = false;
                useFFT.Checked = true;
            
                // wave
                wave = new WaveIn(NumberOfSamples, SamplesPerSec);
                waveArray = new byte[NumberOfSamples];
                waveArrayFrequency = new double[NumberOfSamples / 2 + 1];

                // graphics
                timeDomainGraphics = timeDomain.CreateGraphics();
                frequencyDomainGraphics = frequencyDomain.CreateGraphics();

                // notes
                abc_handler = new ABCHandler();
                noteDetector = new NotesDetector("C:\\Users\\Purple Fire\\Documents\\Visual Studio 2010\\Projects\\Recorder\\Recorder\\Recorder\\Resources\\notes_table.txt");

                // threads synchronization
                is_recording = false;

                // delegates
                SetTextCallback = this.SetText;
                GetTrackBarValueCallback = this.GetTrackBarValue;
                SetEnabledCallback = this.SetEnabled;

                // update microphone devices list
                InputDevice.Items.Add("Choose Input Device");
                InputDevice.SelectedIndex = 0;

                foreach (string deviceName in wave.getDevices())
                {
                    InputDevice.Items.Add(deviceName);
                }
            }
            catch (WaveInException ex)
            {
                string error_message = "Window initialization: " + ex.Message + "Please connect a microphone and run the program again.";
                MessageBox.Show(error_message, "Recorder By Hila Shmuel");
                InputDevice.Enabled = false;
                stop_record.Enabled = false;
                start_record.Enabled = false;
            }
            catch (Exception ex)
            {
                string error_message = "Unknown exception: Window initialization: " + ex.Message;
                MessageBox.Show(error_message, "Recorder By Hila Shmuel");
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
            try
            {
                InputDevice.Enabled = false;
                start_record.Enabled = false;
                stop_record.Enabled = false;

                // open the waveform-audio input device, and start recording
                wave.startDevice((uint)(InputDevice.SelectedIndex - 1));

                // create & open for writing a temporery ABC music sheet file
                abc_handler.init();
                is_recording = true;

                recordingLoopThreadStart = new ThreadStart(recording_loop);
                recordingLoopThread = new Thread(recordingLoopThreadStart);
                recordingLoopThread.Start();

                stop_record.Enabled = true;
            }
            catch (WaveInException ex)
            {
                string error_message = "start record: " + ex.Message + "Please re-connect the microphone and run the program again.";
                MessageBox.Show(error_message, "Recorder By Hila Shmuel");
                InputDevice.Enabled = true;
                start_record.Enabled = false;
                stop_record.Enabled = false;
            }
            catch (Exception ex)
            {
                string error_message = "Unknown exception: start record: " + ex.Message;
                MessageBox.Show(error_message , "Recorder By Hila Shmuel");
            }
        }

        private void stop_record_Click(object sender, EventArgs e)
        {
            try
            {
                ThreadStart stopRecordThreadStart = new ThreadStart(stopRecord);
                Thread stopRecordThread = new Thread(stopRecordThreadStart);
                stopRecordThread.Start();
            }
            catch (Exception ex)
            {
                string error_message = "Unknown exception: stop record: " + ex.Message;
                MessageBox.Show(error_message, "Recorder By Hila Shmuel");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        { 
            try
            {
                ThreadStart stopRecordThreadStart = new ThreadStart(stopRecord);
                Thread stopRecordThread = new Thread(stopRecordThreadStart);
                stopRecordThread.Start();
            }
            catch (Exception ex)
            {
                string error_message = "Unknown exception: closing window: " + ex.Message;
                MessageBox.Show(error_message, "Recorder By Hila Shmuel");
            }
        }

        private void printGraph(Graphics graph, int[] wave, int maxAmplitude, Color background, Pen foreground, float stretch)
        {
            int width = (int)graph.VisibleClipBounds.Width;
            int height = (int)graph.VisibleClipBounds.Height;

            Image bufferImg = new Bitmap(width, height);
            Graphics bufferImgGraphics = Graphics.FromImage(bufferImg);
            bufferImgGraphics.Clear(background);

            int a = 0;
            int b = height - (int)(height * wave[0] / maxAmplitude);
            int numberOfSamplesToDraw = Math.Min(width, wave.Length);

            for (int i = 0; i < numberOfSamplesToDraw - 1; i++)
            {
                a = b;
                b = height - (int)(height * wave[i + 1] / maxAmplitude);
                bufferImgGraphics.DrawLine(foreground, i * stretch, a, (i + 1) * stretch , b);
            }
            graph.DrawImage(bufferImg, 0, 0);
        }

        private void recording_loop()
        {
            try
            {
                Pen pen = new Pen(Color.Orange);

                while (is_recording)
                {
                    // open the waveform-audio input device, and start recording
                    wave.recordBuffer(waveArray, 60);
                    if (!is_recording) { return; }

                    // preform a frequney analysis on the sound wave
                    int maxPitch = FurierTransform.preformTranform(FurierTransform.TRANSFORM.FFT, waveArray, waveArrayFrequency, (int)NumberOfSamples);
                    if (!is_recording) { return; }

                    // print time domain
                    int[] tmpArray = new int[waveArray.Length];
                    for (int i = 0; i < waveArray.Length; i++) { tmpArray[i] = (int)waveArray[i]; }
                    printGraph(timeDomainGraphics, tmpArray, byte.MaxValue, Color.FromArgb(39, 40, 34), pen, 1);
                    if (!is_recording) { return; }

                    // print time domain
                    for (int i = 0; i < waveArrayFrequency.Length; i++) { tmpArray[i] = (int)waveArrayFrequency[i]; }
                    printGraph(frequencyDomainGraphics, tmpArray, tmpArray.Max(), Color.FromArgb(39, 40, 34), pen, 2);
                    if (!is_recording) { return; }

                    // get the current note based on the frequency analysis
                    Note currentNote = noteDetector.FindNote(maxPitch);
                    if (!is_recording) { return; }

                    // determine weather to add new note to sheet
                    if (true == abc_handler.addNoteToMusicSheet(maxPitch))
                    {
                        //chord.Text = maxpitch.ToString();
                        //abc_handler.ConvertToPDF();
                        // load pdf to screen
                        //axAcroPDF1.LoadFile("c:\\file.pdf");
                    }

                    if (!is_recording) { return; }
                    SetControlText(pitch, maxPitch.ToString());
                    SetControlText(chord, currentNote.note);

                    // FFT

                    /*
                    if (pitchTrackBar.InvokeRequired)
                    {
                        IAsyncResult s = this.BeginInvoke(GetTrackBarValueCallback, new object[] { pitchTrackBar });
                        // return lock - when program ends, still waits for invoke
                        if (is_recording)
                        {
                            object a = this.EndInvoke(s);
                            maxpitch += (int)a;
                        }
                    }
                    else
                    {
                        maxpitch += pitchTrackBar.Value;
                    }
                    */
                }
            }
            catch (Exception ex)
            {
                string error_message;

                if (!(ex is WaveInException || ex is FurierTransformException))
                {
                    error_message = "Unknown exception: recording loop: " + ex.Message;
                    MessageBox.Show(error_message, "Recorder By Hila Shmuel");
                }
                else
                {
                    error_message = "recording loop: " + ex.Message + "Please re-connect the microphone and run the program again.";
                    MessageBox.Show(error_message, "Recorder By Hila Shmuel");
                }

                InputDevice.Enabled = true;
                start_record.Enabled = false;
                stop_record.Enabled = false;

                ThreadStart stopRecordThreadStart = new ThreadStart(stopRecord);
                Thread stopRecordThread = new Thread(stopRecordThreadStart);
                stopRecordThread.Start();

                return;
            }
        }

        private void stopRecord()
        {
            try
            {
                SetControlEnabled(InputDevice, false);
                SetControlEnabled(start_record, false);
                SetControlEnabled(stop_record, false);

                if (is_recording)
                {
                    is_recording = false;
                    recordingLoopThread.Join();

                    // stop the waveform-audio input device recording
                    wave.stopDevice();                
                }
                is_recording = false;
  
                // TODO: close & delete temporery file for ABC shit
                //abc_handler.deleteABCfile();

                SetControlEnabled(InputDevice, true);
                SetControlEnabled(start_record, true);
            }
            catch (WaveInException ex)
            {
                string error_message = "stop record thread: " + ex.Message + "Please re-connect the microphone and run the program again.";
                MessageBox.Show(error_message, "Recorder By Hila Shmuel");
                InputDevice.Enabled = true;
                start_record.Enabled = false;
                stop_record.Enabled = false;
            }
            catch (Exception ex)
            {
                string error_message;

                if (ex is WaveInException )
                {
                    error_message = error_message = "stop record thread: " + ex.Message + "Please re-connect the microphone and run the program again.";
                }
                else
                {
                    error_message = "Unknown exception: stop record thread: " + ex.Message;
                }

                MessageBox.Show(error_message, "Recorder By Hila Shmuel");
                InputDevice.Enabled = true;
                start_record.Enabled = false;
                stop_record.Enabled = false;
            }
        }


        /***************************************************************************************
         * SET & GET User-Interface controls methods - DONT USE THEM!
         ***************************************************************************************/
        private void SetText(TextBox text_box, string text)
        {
            text_box.Text = text;
        }

        private int GetTrackBarValue(TrackBar trackBar)
        {
            return trackBar.Value;
        }

        private void SetEnabled(Control controller, bool enabled)
        {
            controller.Enabled = enabled;
        }

        /***************************************************************************************
         * SET & GET User-Interface controls methods - USE THEM!
         ***************************************************************************************/
        private void SetControlText(Control control, string text)
        {
            if (control == null)
            {
                return;
            }
            else if (control.InvokeRequired)
            {
                this.BeginInvoke(SetTextCallback, new object[] { control, text });
            }
            else
            {
                control.Text = text;
            }
        }

        private void SetControlEnabled(Control controller, bool enabled)
        {
            if (controller == null)
            {
                return;
            }
            if (controller.InvokeRequired)
            {
                this.BeginInvoke(SetEnabledCallback, new object[] { controller, enabled });
            }
            else
            {
                controller.Enabled = enabled;
            }
        }
    }

    public delegate void SetTextDelegate(TextBox text_box, string text);
    public delegate int GetTrackBarValueDelegate(TrackBar trackBar);
    public delegate void SetEnabledDelegate(Control controller, bool enabled);
}
