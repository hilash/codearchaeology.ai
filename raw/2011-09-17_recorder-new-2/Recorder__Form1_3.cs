/*******************************************************************
 * Class Name: Form1
 * Purpose: a simple recorder, 8-bit per sample, with one channel.
 * Author: Hila Shmuel, 
 * Date: TODO
 *******************************************************************/

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
using Notes;
using MIDI;
using Delegates;

namespace Recorder
{
    public partial class Form1 : Form
    {
        /***** Form's Variables ******************************************************************/
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
        NotesDB notesDB;
        NotesDetector notesDetector;

        // Fingering Chart
        FingeringChart fingeringChart;
        bool doesFingeringDisplayed;

        // threads synchronization
        ThreadStart drawingLoopThreadStart;
        Thread drawingLoopThread;    
        //ThreadStart recordingLoopThreadStart;  
        //Thread recordingLoopThread;
        bool isRecording;

        // threads synchronization
        ThreadStart StartPlayingRecordThreadStart;
        Thread StartPlayingRecordThread;

        // delegates
        Delegates.Delegates DelegatesUI;

        /***** Forms Methods *********************************************************************/

        public Form1()
        {
            try
            {
                InitializeComponent();

                // UI elements
                comboBoxInputDevices.Enabled = true;
                buttonStartRecord.Enabled = false;
                buttonStopRecord.Enabled = false;
                buttonStartPlayingRecord.Enabled = false;
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
                notesDetector = new NotesDetector();

                // notes
                NotesPlayed = new List<Note>();
                notesDB = new NotesDB();

                // threads synchronization
                isRecording = false;

                // delegates 
                DelegatesUI = new Delegates.Delegates();

                // Fingering Chart
                fingeringChart = new FingeringChart();
                doesFingeringDisplayed = false;


                // Load microphone devices list
                comboBoxInputDevices.Items.Add("Choose Input Device");
                comboBoxInputDevices.SelectedIndex = 0;
                foreach (string deviceName in Wave.getDevices())
                {
                    comboBoxInputDevices.Items.Add(deviceName);
                }

                // Load Playing Formats
                comboBoxPlayingRecordFormats.Items.Add("WAVE");
                comboBoxPlayingRecordFormats.Items.Add("MIDI");
                comboBoxPlayingRecordFormats.Items.Add("Beep");

                // Enable Disable Controllers Groups
                groupBoxRecord.Enabled = true;
                groupBoxPlayingRecord.Enabled = false;
                groupBoxSavingRecord.Enabled = false;
                groupBoxTransform.Enabled = true;
            }
            catch (Exception ex)
            {
                string error_message;

                if (ex is WaveInException)
                {
                    error_message = "Recording Error: " + ex.Message + "Please connect a microphone and run the program again.";
                }
                else
                {
                    error_message = "Unknown exception: Window initialization: " + ex.Message;
                }

                MessageBox.Show(error_message, "Recorder By Hila Shmuel");
                groupBoxRecord.Enabled = false;
                groupBoxTransform.Enabled = false;
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

        /***** Controllers Events - Recording ****************************************************/
       
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
                string guid = Guid.NewGuid().ToString();
                Wave.startDevice((uint)(comboBoxInputDevices.SelectedIndex - 1), @"C:\temp\" + guid + ".wav");

                isRecording = true;

                //recordingLoopThreadStart = new ThreadStart(RecordingLoop);
                //recordingLoopThread = new Thread(recordingLoopThreadStart);
                //recordingLoopThread.Start();

                
                drawingLoopThreadStart = new ThreadStart(DrawingLoop);
                drawingLoopThread = new Thread(drawingLoopThreadStart);
                drawingLoopThread.Start();
                
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
                groupBoxRecord.Enabled = false;
                groupBoxPlayingRecord.Enabled = false;
                groupBoxSavingRecord.Enabled = false;
                groupBoxTransform.Enabled = false;
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
                buttonStartPlayingRecord.Enabled = false;
            }
            else
            {
                buttonStartRecord.Enabled = true;
                buttonStopRecord.Enabled = false;
                buttonStartPlayingRecord.Enabled = false;
            }
        }

        /***** Controllers Events - Playing Record ***********************************************/
        
        private void buttonPlayRecord_Click(object sender, EventArgs e)
        {
            try
            {
                // determine which playing method to chose
                switch (comboBoxPlayingRecordFormats.SelectedItem.ToString())
                {
                    case "Beep":
                        //PlayingRecordMethod = new PlayNotesByBeep();
                        break;
                    default:
                        string error_message = "Unknown exception: buttonPlayRecord_Click: unknown playing method";
                        MessageBox.Show(error_message, "Recorder By Hila Shmuel");
                        break;
                }


                StartPlayingRecordThreadStart = new ThreadStart(StartPlayingRecord);
                StartPlayingRecordThread = new Thread(StartPlayingRecordThreadStart);
                StartPlayingRecordThread.Start();
            }
            catch (Exception ex)
            {
                string error_message = "Unknown exception: play record: " + ex.Message;
                MessageBox.Show(error_message, "Recorder By Hila Shmuel");
            }

        }

        /***** Controllers Events - Saving Record ************************************************/

        private void buttonSaveRecord_Click(object sender, EventArgs e)
        {
            SavingRecordForm savingRecordForm = new SavingRecordForm(NotesPlayed);
            savingRecordForm.ShowDialog();
        }  

        /***** Controllers Events - Tranform *****************************************************/
        
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

        /***** Controllers Events - Graphics *****************************************************/
        
        private void pictureBoxTimeDomain_Paint(object sender, PaintEventArgs e)
        {
            timeDomainGraphics = pictureBoxTimeDomain.CreateGraphics();
        }

        private void pictureBoxFrequencyDomain_Paint(object sender, PaintEventArgs e)
        {
            frequencyDomainGraphics = pictureBoxFrequencyDomain.CreateGraphics();
        }

        private void panelMusicSheet_Paint(object sender, PaintEventArgs e)
        {
            // paint only when not recording, since when recording, the drawingLoop
            // paints the music sheet, and "owns" the controller

            MusicSheetGraphics = panelMusicSheet.CreateGraphics();
            //if (!isRecording)
            //{
            MusicSheetMutex.WaitOne();
            SolidBrush brush = new SolidBrush(Color.Black);
            noteDrawer.Draw(MusicSheetGraphics, Color.White, brush, brush);
            MusicSheetMutex.ReleaseMutex();
            //}
        }

        /***** Controllers Events - User Interface ***********************************************/
        
        private void tabControl_Selected(object sender, TabControlEventArgs e)
        {
            if (tabControl.SelectedTab.AccessibleName == "tabPageFFT")
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

        /***** Controllers Events - Instrument Fingering *****************************************/

        private void splitContainerFingering_Panel2_Paint(object sender, PaintEventArgs e)
        {
            fingeringChart.Draw(splitContainerFingering.Panel2.CreateGraphics(), new SolidBrush(Color.Black));
        }

        private void splitContainerFingering_Panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isRecording)
            {
                Note currentNote = fingeringChart.GetNoteOnLocation(e.Location);
                if (currentNote != null)
                {
                    fingeringsPicture.createPicture(pictureRecorderGraphics, currentNote);
                    DelegatesUI.SetControlText(this, textBoxPitch, currentNote.Frequency.ToString());
                    DelegatesUI.SetControlText(this, textBoxNote, currentNote.NoteDescription);
                    DelegatesUI.SetControlText(this, textBoxMIDI, currentNote.MIDI.ToString());
                    doesFingeringDisplayed = true;
                }
                else
                {
                    if (doesFingeringDisplayed)
                    {
                        fingeringsPicture.createPicture(pictureRecorderGraphics, null);
                        DelegatesUI.SetControlText(this, textBoxPitch, "");
                        DelegatesUI.SetControlText(this, textBoxNote, "");
                        DelegatesUI.SetControlText(this, textBoxMIDI, "");
                        doesFingeringDisplayed = false;
                    }
                }
            }
        }

        /***** Form's Threads ********************************************************************/
        
        private void DrawingLoop()
        {
            try
            {
                Note prevNote = null;
                Pen pen = new Pen(Color.Orange);
                Brush brush = new SolidBrush(Color.Black);

                while (isRecording)
                {
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
                        WaveGraphics.DrawGraph(timeDomainGraphics, tmpArray, byte.MaxValue, Color.FromArgb(39, 40, 34), pen, tmpArray.Length / 4);
                        if (!isRecording) { return; }

                        // print fequency domain
                        Array.Resize(ref tmpArray, WaveArrayFrequency.Length);
                        for (int i = 0; i < WaveArrayFrequency.Length; i++) { tmpArray[i] = (int)WaveArrayFrequency[i]; }
                        WaveGraphics.DrawGraph(frequencyDomainGraphics, tmpArray, 1800, Color.FromArgb(39, 40, 34), pen,
                            (int)((double)RecorderMaxFrequency * (double)NumberOfSamples / (double)SamplesPerSec));
                        if (!isRecording) { return; }
                    }

                    // get the current note based on the frequency analysis
                    Note currentNote = notesDB.FindNote(maxPitch);
                    if (!isRecording) { return; } 

                    // add to ABC file
                    //if (true == abcHandler.addNoteToMusicSheet(currentNote.NoteDescription))
                    {
                        //chord.Text = currentNote.note;
                        //abcHandler.ConvertToPDF();
                        // load pdf to screen
                        //axAcroPDF1.LoadFile("c:\\file.pdf");
                    }

                    if (!isRecording) { return; }
                    DelegatesUI.SetControlText(this, textBoxPitch, maxPitch.ToString());
                    DelegatesUI.SetControlText(this, textBoxNote, currentNote.NoteDescription);
                    DelegatesUI.SetControlText(this, textBoxMIDI, currentNote.MIDI.ToString());

                    //draw recorder fingering
                    fingeringsPicture.createPicture(pictureRecorderGraphics, currentNote);

                    // Detect the note based on the last and current samples
                    Note detectedNote = notesDetector.DetectNote(currentNote, Environment.TickCount);
                    if (null != detectedNote)
                    {
                        if (prevNote != detectedNote)
                        {
                            NotesPlayed.Add(detectedNote);

                            // Draw note in interactive music sheet
                            if (72 <= detectedNote.MIDI && detectedNote.MIDI <= 99)
                            {
                                MusicSheetMutex.WaitOne();
                                noteDrawer.Draw(MusicSheetGraphics, Color.White, brush, brush, detectedNote);
                                MusicSheetMutex.ReleaseMutex();
                            }
                        }
                        else
                        {
                            // redraw note
                        }
                    }
                    prevNote = detectedNote;

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

                DelegatesUI.SetControlEnabled(this, comboBoxInputDevices, false);
                DelegatesUI.SetControlEnabled(this, buttonStartRecord, false);
                DelegatesUI.SetControlEnabled(this, buttonStopRecord, false);
                DelegatesUI.SetControlEnabled(this, buttonStartPlayingRecord, false);

                ThreadStart StopRecordThreadStart = new ThreadStart(StopRecord);
                Thread StopRecordThread = new Thread(StopRecordThreadStart);
                StopRecordThread.Start();

                return;
            }
        }

        private void RecordingLoop()
        {
            try
            {
                while (isRecording)
                {
                    //Wave.recordBuffer();
                }
            }
            catch (Exception ex)
            {
                string error_message;

                if (!(ex is WaveInException))
                {
                    error_message = "WaveIn Exception: recording loop: " + ex.Message;
                    MessageBox.Show(error_message, "Recorder By Hila Shmuel");
                }
                else
                {
                    error_message = "recording loop: " + ex.Message + "Please re-connect the microphone and run the program again.";
                    MessageBox.Show(error_message, "Recorder By Hila Shmuel");
                }

                DelegatesUI.SetControlEnabled(this, comboBoxInputDevices, false);
                DelegatesUI.SetControlEnabled(this, buttonStartRecord, false);
                DelegatesUI.SetControlEnabled(this, buttonStopRecord, false);
                DelegatesUI.SetControlEnabled(this, buttonStartPlayingRecord, false);

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
                DelegatesUI.SetControlEnabled(this, groupBoxRecord, false);
                DelegatesUI.SetControlEnabled(this, groupBoxSavingRecord, false);
                DelegatesUI.SetControlEnabled(this, groupBoxPlayingRecord, false);

                if (isRecording)
                {
                    isRecording = false;
                    drawingLoopThread.Join();
                    //recordingLoopThread.Join();

                    // stop the waveform-audio input device recording
                    Wave.stopDevice();
                }
                isRecording = false;

                // TODO: close & delete temporery file for ABC shit
                //abcHandler.deleteABCfile();

                DelegatesUI.SetControlEnabled(this, groupBoxRecord, true);
                DelegatesUI.SetControlEnabled(this, groupBoxSavingRecord, true);
                DelegatesUI.SetControlEnabled(this, groupBoxPlayingRecord, true);
                DelegatesUI.SetControlEnabled(this, buttonStartPlayingRecord, true);

                DelegatesUI.SetControlEnabled(this, comboBoxInputDevices, true);
                DelegatesUI.SetControlEnabled(this, buttonStartRecord, true);
                DelegatesUI.SetControlEnabled(this, buttonStopRecord, false);
            }
            catch (Exception ex)
            {
                string error_message;

                if (ex is WaveInException)
                {
                    error_message = error_message = "stop record thread: " + ex.Message + "Please re-connect the microphone and run the program again.";
                }
                else
                {
                    error_message = "Unknown exception: stop record thread: " + ex.Message;
                }

                MessageBox.Show(error_message, "Recorder By Hila Shmuel");
                DelegatesUI.SetControlEnabled(this, comboBoxInputDevices, true);
                DelegatesUI.SetControlEnabled(this, buttonStartRecord, false);
                DelegatesUI.SetControlEnabled(this, buttonStopRecord, false);
                DelegatesUI.SetControlEnabled(this, buttonStartPlayingRecord, false);
            }
        }

        private void StartPlayingRecord()
        {
            //MIDI.MIDI.Play(NotesPlayed);

           // PlayingRecordMethod.Play();

            //PlayingRecordMethod.AddNotes(NotesPlayed);
            //PlayingRecordMethod.Play();
            /*
            try
            {
                DelegatesUI.SetControlEnabled(this, comboBoxInputDevices, false);
                DelegatesUI.SetControlEnabled(this, buttonStartRecord, false);
                DelegatesUI.SetControlEnabled(this, buttonStopRecord, false);
                DelegatesUI.SetControlEnabled(this, buttonPlayRecord, false);

                if (isRecording)
                {
                    isRecording = false;
                    drawingLoopThread.Join();

                    // stop the waveform-audio input device recording
                    Wave.stopDevice();
                }
                isRecording = false;

                // TODO: close & delete temporery file for ABC shit
                //abcHandler.deleteABCfile();

                DelegatesUI.SetControlEnabled(this, comboBoxInputDevices, true);
                DelegatesUI.SetControlEnabled(this, buttonStartRecord, true);
                DelegatesUI.SetControlEnabled(this, buttonPlayRecord, true);
            }
            catch (Exception ex)
            {
                string error_message;

                if (ex is WaveInException)
                {
                    error_message = error_message = "stop record thread: " + ex.Message + "Please re-connect the microphone and run the program again.";
                }
                else
                {
                    error_message = "Unknown exception: stop record thread: " + ex.Message;
                }

                MessageBox.Show(error_message, "Recorder By Hila Shmuel");
                DelegatesUI.SetControlEnabled(this, comboBoxInputDevices, true);
                DelegatesUI.SetControlEnabled(this, buttonStartRecord, false);
                DelegatesUI.SetControlEnabled(this, buttonStopRecord, false);
                DelegatesUI.SetControlEnabled(this, buttonPlayRecord, false);
            }*/
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        
    } 
}
