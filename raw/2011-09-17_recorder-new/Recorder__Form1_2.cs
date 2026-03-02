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
        /***************************************************************************************
         * Forms Varibles
         ***************************************************************************************/
        const uint NumberOfSamples = 4096;
        const uint SamplesPerSec = 88200;
        const uint RecorderMaxFrequency = 3000;

        // wave
        WaveIn Wave;
        byte[] WaveArray;
        double[] WaveArrayFrequency;
        FurierTransform furierTransform;
        FurierTransform.TRANSFORM transformType;

        // domains graphics
        Graphics timeDomainGraphics; 
        Graphics frequencyDomainGraphics;
        bool printGraphsEnabled;

        // fingering 
        Graphics pictureRecorderGraphics;
        FingeringsPicture fingeringsPicture;

        // notes drawing
        Graphics MusicSheetGraphics;
        NoteDrawer noteDrawer;
        Mutex MusicSheetMutex;

        // notes
        List<Note> NotesPlayed;
        ABCHandler abcHandler;
        NotesDetector noteDetector;

        // threads synchronization
        ThreadStart recordingLoopThreadStart;
        Thread recordingLoopThread;
        bool isRecording;
        
        // delegates
        SetTextDelegate SetTextCallback;
        GetTrackBarValueDelegate GetTrackBarValueCallback;
        SetEnabledDelegate SetEnabledCallback;

        /***************************************************************************************
         * Forms Methods
         ***************************************************************************************/

        public Form1()
        {
            try
            {
                InitializeComponent();

                // UI elements
                comboBoxInputDevices.Enabled = true;
                buttonStartRecord.Enabled = false;
                buttonStopRecord.Enabled = false;
                radioButtonFFT.Checked = true;
            
                // wave
                Wave = new WaveIn(NumberOfSamples, SamplesPerSec);
                WaveArray = new byte[NumberOfSamples];
                WaveArrayFrequency = new double[NumberOfSamples / 2 + 1];
                furierTransform = new FurierTransform(NumberOfSamples);
                transformType = FurierTransform.TRANSFORM.FFT;

                // graphics
                timeDomainGraphics = pictureBoxTimeDomain.CreateGraphics();
                frequencyDomainGraphics = pictureBoxFrequencyDomain.CreateGraphics();
                pictureRecorderGraphics = pictureBoxRecorder.CreateGraphics();
                MusicSheetGraphics = panelMusicSheet.CreateGraphics();

                printGraphsEnabled = true;
                fingeringsPicture = new FingeringsPicture("C:\\Users\\Purple Fire\\Documents\\Visual Studio 2010\\Projects\\Recorder2\\Recorder\\Recorder\\Resources\\Recorder picture.jpg",
                    "C:\\Users\\Purple Fire\\Documents\\Visual Studio 2010\\Projects\\Recorder2\\Recorder\\Recorder\\Resources\\fingerings_table.txt",
                    "C:\\Users\\Purple Fire\\Documents\\Visual Studio 2010\\Projects\\Recorder2\\Recorder\\Recorder\\Resources\\recoders_holes.txt");


                // notes drawing
                noteDrawer = new NoteDrawer();
                MusicSheetMutex = new Mutex();

                // notes
                NotesPlayed = new List<Note>();
                abcHandler = new ABCHandler();
                noteDetector = new NotesDetector("C:\\Users\\Purple Fire\\Documents\\Visual Studio 2010\\Projects\\Recorder2\\Recorder\\Recorder\\Resources\\notes_table.txt");

                // threads synchronization
                isRecording = false;

                // delegates
                SetTextCallback = this.SetText;
                GetTrackBarValueCallback = this.GetTrackBarValue;
                SetEnabledCallback = this.SetEnabled;

                // update microphone devices list
                comboBoxInputDevices.Items.Add("Choose Input Device");
                comboBoxInputDevices.SelectedIndex = 0;

                foreach (string deviceName in Wave.getDevices())
                {
                    comboBoxInputDevices.Items.Add(deviceName);
                }
            }
            catch (Exception ex)
            {
                string error_message;

                if (ex is WaveInException)
                {
                    error_message = "Window initialization: " + ex.Message + "Please connect a microphone and run the program again.";
                }
                else
                {
                    error_message = "Unknown exception: Window initialization: " + ex.Message;
                }

                MessageBox.Show(error_message, "Recorder By Hila Shmuel");
                SetControlEnabled(comboBoxInputDevices, false);
                SetControlEnabled(buttonStartRecord, false);
                SetControlEnabled(buttonStopRecord, false);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        { 
            try
            {
                ThreadStart StopRecordThreadStart = new ThreadStart(StopRecord);
                Thread StopRecordThread = new Thread(StopRecordThreadStart);
                StopRecordThread.Start();
            }
            catch (Exception ex)
            {
                string error_message = "Unknown exception: closing window: " + ex.Message;
                MessageBox.Show(error_message, "Recorder By Hila Shmuel");
            }
        }

        private void printGraph(Graphics graph, int[] wave, int maxAmplitude, Color background, Pen foreground, int maxSample)
        {
            int width = (int)graph.VisibleClipBounds.Width;
            int height = (int)graph.VisibleClipBounds.Height;
            float stretch = 0;
            int numberOfSamplesToDraw = 0;

            Image bufferImg = new Bitmap(width, height);
            Graphics bufferImgGraphics = Graphics.FromImage(bufferImg);
            bufferImgGraphics.Clear(background);

            numberOfSamplesToDraw = Math.Min(maxSample, wave.Length);
            stretch = (float)width / (float)numberOfSamplesToDraw;

            if (maxAmplitude < wave.Max())
            {
                maxAmplitude = wave.Max();
            }

            int a = 0;
            int b = height - (int)(height * wave[0] / maxAmplitude);

            for (int i = 0; i < numberOfSamplesToDraw - 1; i++)
            {
                a = b;
                b = height - (int)(height * wave[i + 1] / maxAmplitude);
                bufferImgGraphics.DrawLine(foreground, i * stretch, a - 1, (i + 1) * stretch , b - 1 );
            }
            graph.DrawImage(bufferImg, 0, 0);
        }

        private void RecordingLoop()
        {
            try
            {
                long cur_time = ttime();

                Pen pen = new Pen(Color.Orange);
                Brush brush = new SolidBrush(Color.Black);

                while (isRecording)
                {
                    // TODO: add tab control

                    // open the waveform-audio input device, and start recording
                    Wave.recordBuffer(WaveArray, 60);
                    if (!isRecording) { return; }

                    // preform a frequney analysis on the sound wave
                    float maxPitch = (float)furierTransform.preformTranform(transformType, WaveArray, WaveArrayFrequency, NumberOfSamples, SamplesPerSec);
                    if (!isRecording) { return; }

                    if (printGraphsEnabled)
                    {
                        // print time domain
                        int[] tmpArray = new int[WaveArray.Length];
                        for (int i = 0; i < WaveArray.Length; i++) { tmpArray[i] = (int)WaveArray[i]; }
                        printGraph(timeDomainGraphics, tmpArray, byte.MaxValue, Color.FromArgb(39, 40, 34), pen, tmpArray.Length / 4);
                        if (!isRecording) { return; }

                        // print fequency domain
                        Array.Resize(ref tmpArray, WaveArrayFrequency.Length);
                        for (int i = 0; i < WaveArrayFrequency.Length; i++) { tmpArray[i] = (int)WaveArrayFrequency[i]; }
                        //printGraph(frequencyDomainGraphics, tmpArray, tmpArray.Max(), Color.FromArgb(39, 40, 34), pen,
                            //(int) ((double)RecorderMaxFrequency * (double)NumberOfSamples / (double) SamplesPerSec));
                        printGraph(frequencyDomainGraphics, tmpArray, 1800, Color.FromArgb(39, 40, 34), pen,
                            (int)((double)RecorderMaxFrequency * (double)NumberOfSamples / (double)SamplesPerSec));

                       
                        if (!isRecording) { return; }
                    }

                    // get the current note based on the frequency analysis
                    Note currentNote = noteDetector.FindNote(maxPitch);
                    if (!isRecording) { return; }

                    // determine if is a recorder note (in the specific range)
                    /*if (!(60 <= currentNote.MIDI && currentNote.MIDI <= 84))
                    {
                        SetControlText(textBoxPitch, "");
                        SetControlText(textBoxNote, "");
                        SetControlText(textBoxMIDI, "");
                        continue;
                    }*/

                    // add to ABC file
                    if (true == abcHandler.addNoteToMusicSheet(currentNote.note))
                    {
                        //chord.Text = currentNote.note;
                        //abcHandler.ConvertToPDF();
                        // load pdf to screen
                        //axAcroPDF1.LoadFile("c:\\file.pdf");
                    }

                    if (!isRecording) { return; }
                    SetControlText(textBoxPitch, maxPitch.ToString());
                    SetControlText(textBoxNote, currentNote.note);
                    SetControlText(textBoxMIDI, currentNote.MIDI.ToString());

                    //draw recorder fingering
                    fingeringsPicture.createPicture(pictureRecorderGraphics, currentNote);

                    //daw note in interactive music sheet
                    NotesPlayed.Add(currentNote);

                    if (72 <= currentNote.MIDI && currentNote.MIDI <= 99)
                    {
                        MusicSheetMutex.WaitOne();
                        noteDrawer.Draw(MusicSheetGraphics, new SolidBrush(Color.Black), currentNote);
                        MusicSheetMutex.ReleaseMutex();
                    }

                    System.GC.Collect();
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

                SetControlEnabled(comboBoxInputDevices, false);
                SetControlEnabled(buttonStartRecord, false);
                SetControlEnabled(buttonStopRecord, false);

                ThreadStart StopRecordThreadStart = new ThreadStart(StopRecord);
                Thread StopRecordThread = new Thread(StopRecordThreadStart);
                StopRecordThread.Start();

                return;
            }
        }

        private void StopRecord()
        {
            try
            {
                SetControlEnabled(comboBoxInputDevices, false);
                SetControlEnabled(buttonStartRecord, false);
                SetControlEnabled(buttonStopRecord, false);

                if (isRecording)
                {
                    isRecording = false;
                    recordingLoopThread.Join();

                    // stop the waveform-audio input device recording
                    Wave.stopDevice();                
                }
                isRecording = false;
  
                // TODO: close & delete temporery file for ABC shit
                //abcHandler.deleteABCfile();

                SetControlEnabled(comboBoxInputDevices, true);
                SetControlEnabled(buttonStartRecord, true);
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
                SetControlEnabled(comboBoxInputDevices, true);
                SetControlEnabled(buttonStartRecord, false);
                SetControlEnabled(buttonStopRecord, false);
            }
        }

        private long ttime()
        {

            System.TimeSpan timeDifference = DateTime.UtcNow -
                new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return System.Convert.ToInt64(timeDifference.TotalSeconds);
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

        /***************************************************************************************
         * Controllers Events 
         ***************************************************************************************/

        private void buttonStartRecord_Click(object sender, EventArgs e)
        {
            try
            {
                comboBoxInputDevices.Enabled = false;
                buttonStartRecord.Enabled = false;
                buttonStopRecord.Enabled = false;

                // clear notes buffer
                NotesPlayed.Clear();

                // open the waveform-audio input device, and start recording
                Wave.startDevice((uint)(comboBoxInputDevices.SelectedIndex - 1));

                // create & open for writing a temporery ABC music sheet file
                abcHandler.init();
                isRecording = true;

                recordingLoopThreadStart = new ThreadStart(RecordingLoop);
                recordingLoopThread = new Thread(recordingLoopThreadStart);
                recordingLoopThread.Start();

                buttonStopRecord.Enabled = true;
            }
            catch (Exception ex)
            {
                string error_message;

                if (ex is WaveInException)
                {
                    error_message = "start record: " + ex.Message + "Please re-connect the microphone and run the program again.";
                }
                else
                {
                    error_message = "Unknown exception: start record: " + ex.Message;
                }

                MessageBox.Show(error_message, "Recorder By Hila Shmuel");
                SetControlEnabled(comboBoxInputDevices, true);
                SetControlEnabled(buttonStartRecord, false);
                SetControlEnabled(buttonStopRecord, false);
            }
        }

        private void buttonStopRecord_Click(object sender, EventArgs e)
        {
            try
            {
                ThreadStart StopRecordThreadStart = new ThreadStart(StopRecord);
                Thread StopRecordThread = new Thread(StopRecordThreadStart);
                StopRecordThread.Start();
            }
            catch (Exception ex)
            {
                string error_message = "Unknown exception: stop record: " + ex.Message;
                MessageBox.Show(error_message, "Recorder By Hila Shmuel");
            }
        }

        private void comboBoxInputDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxInputDevices.SelectedIndex == 0)
            {
                buttonStartRecord.Enabled = false;
                buttonStopRecord.Enabled = false;
            }
            else
            {
                buttonStartRecord.Enabled = true;
                buttonStopRecord.Enabled = false;
            }
        }

        private void tabControl_Selected(object sender, TabControlEventArgs e)
        {
            if (tabControl.SelectedTab.AccessibleName == "tabFFT")
            {
                printGraphsEnabled = true;
                // graphics
                timeDomainGraphics = pictureBoxTimeDomain.CreateGraphics();
                frequencyDomainGraphics = pictureBoxFrequencyDomain.CreateGraphics();
            }
            else
            {
                printGraphsEnabled = false;
            }
        }

        private void pictureBoxTimeDomain_Paint(object sender, PaintEventArgs e)
        {
            timeDomainGraphics = pictureBoxTimeDomain.CreateGraphics();
        }

        private void pictureBoxFrequencyDomain_Paint(object sender, PaintEventArgs e)
        {
            frequencyDomainGraphics = pictureBoxFrequencyDomain.CreateGraphics();
        }

        private void radioButtonFFT_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonFFT.Checked)
            {
                transformType = FurierTransform.TRANSFORM.FFT;
            }
        }

        private void radioButtonDFT_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonDFT.Checked)
            {
                transformType = FurierTransform.TRANSFORM.DFT;
            }
        }

        private void panelMusicSheet_Paint(object sender, PaintEventArgs e)
        {
            // paint only when not recording, since when recording, the RecordingLoop
            // paints the music sheet, and "owns" the controller

            MusicSheetGraphics = panelMusicSheet.CreateGraphics();
            //if (!isRecording)
            //{
            MusicSheetMutex.WaitOne();
                noteDrawer.Draw(MusicSheetGraphics, new SolidBrush(Color.Black));
                MusicSheetMutex.ReleaseMutex();
            //}
        }
    }

    /***************************************************************************************
     * Delegates for UI controls
     ***************************************************************************************/
    public delegate void SetTextDelegate(TextBox text_box, string text);
    public delegate int GetTrackBarValueDelegate(TrackBar trackBar);
    public delegate void SetEnabledDelegate(Control controller, bool enabled);
}
