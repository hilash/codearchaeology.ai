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
using System.Configuration;
using Win32;
using System.Threading;
using System.Reflection;
using Wave;
using Analysis;
using Notes;
using Delegates;

namespace Recorder
{
    /// <summary>
    /// The program main form, includes all the user interface controls and functionality
    /// </summary>
    public partial class MainForm : Form
    {

        /***** Form's Variables ******************************************************************/

        /// <summary>
        /// Number of samples, given each time from the recording device (microphone).
        /// Must be a power of 2 (for the furier transfrom analysis).
        /// The bigger that number, the more accureate the notes detection result,
        /// but the running time is affected.
        /// </summary>
        const uint NumberOfSamples = 4096;

        /// <summary>
        /// The recording device sampling rate. usually, one of the following numbers:
        /// 11025, 22050, 44100, ... 
        /// </summary>
        const uint SamplesPerSec = 88200;

        /// <summary>
        /// The max frequency (note) of the recorder instrument.
        /// Used for displaying the frequency spectrum.
        /// </summary>
        const uint RecorderMaxFrequency = 3000;

        // wave
        WaveIn Wave;
        byte[] WaveArray;
        double[] WaveArrayFrequency;
        FurierTransform furierTransform;

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

        // notes
        List<Note> NotesPlayed;
        NotesDB notesDB;
        NotesDetector notesDetector;

        //Player
        IPlayNotes musicPlayer;

        // Fingering Chart
        FingeringChart fingeringChart;
        bool doesFingeringDisplayed;

        // threads synchronization
        Thread StartPlayingRecordThread;
        Thread recordingLoopThread;    
        bool isRecording;

        // recorder Hero
        RecorderHero recorderHero;

        // files
        string tempWaveFilePath;
        string tempPDFFilePath;

        // threads synchronization
        

        // delegates
        Delegates.Delegates DelegatesUI;

        /***** Forms Methods *********************************************************************/

        public MainForm()
        {
            try
            {
                InitializeComponent();

                // UI elements
                comboBoxInputDevices.Enabled = true;
                buttonStartRecord.Enabled = false;
                buttonStopRecord.Enabled = false;
                buttonStartPlayingRecord.Enabled = false;
            
                // wave
                Wave = new WaveIn(NumberOfSamples, SamplesPerSec);
                WaveArray = new byte[NumberOfSamples];
                WaveArrayFrequency = new double[NumberOfSamples / 2 + 1];
                furierTransform = new FurierTransform(NumberOfSamples);

                // graphics
                timeDomainGraphics = pictureBoxTimeDomain.CreateGraphics();
                frequencyDomainGraphics = pictureBoxFrequencyDomain.CreateGraphics();
                pictureRecorderGraphics = pictureBoxRecorder.CreateGraphics();
                MusicSheetGraphics = panelNotes.CreateGraphics();

                printGraphsEnabled = true;
                fingeringsPicture = new FingeringsPicture();

                // notes drawing
                noteDrawer = new NoteDrawer();
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

                // recorder Hero
                recorderHero = null;


                // Load microphone devices list
                comboBoxInputDevices.Items.Add("Choose Input Device");
                comboBoxInputDevices.SelectedIndex = 0;
                foreach (string deviceName in Wave.getDevices())
                {
                    comboBoxInputDevices.Items.Add(deviceName);
                }

                tempWaveFilePath = null;
                tempPDFFilePath = Path.GetTempFileName() + ".pdf";

                // Load Playing Formats
                comboBoxPlayingRecordFormats.Items.Add("WAVE (as recorded)");
                comboBoxPlayingRecordFormats.Items.Add("WAVE (synthesized)");
                comboBoxPlayingRecordFormats.Items.Add("Beep");
                comboBoxPlayingRecordFormats.SelectedIndex = 0;

                // Enable Disable Controllers Groups
                groupBoxRecord.Enabled = true;
                groupBoxPlayingRecord.Enabled = false;
                groupBoxFileRecord.Enabled = true;

                if (Properties.Settings.Default.NeedToConfig)
                {

                    // need to configure the program for the first time
                    ConfigurationForm configurationForm = new ConfigurationForm();
                    configurationForm.ShowDialog();
                    Properties.Settings.Default.NeedToConfig = false;
                    Properties.Settings.Default.Save();
                }
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

        /***** Menu ******************************************************************************/

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            openABCfile();
        }

        private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            openABCfile();
        }

        private void saveToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SavingRecordForm savingRecordForm = new SavingRecordForm(NotesPlayed, tempWaveFilePath);
            savingRecordForm.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }


        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                ThreadStart StopRecordThreadStart = new ThreadStart(StopRecord);
                Thread StopRecordThread = new Thread(StopRecordThreadStart);
                StopRecordThread.Start();
                this.Close();
            }
            catch (Exception ex)
            {
                string error_message = "Unknown exception: closing window: " + ex.Message;
                MessageBox.Show(error_message, "Recorder By Hila Shmuel");
            }
        }

        private void ConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurationForm configurationForm = new ConfigurationForm();
            configurationForm.ShowDialog();
        }
        
        private void buttonSaveRecord_EnabledChanged(object sender, EventArgs e)
        {
           // saveToolStripMenuItem.Enabled = buttonSaveRecord.Enabled;
        }

        private void newToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // when pressing "New", delete current notes buffer,
            // and clear the music sheet
            lock (noteDrawer)
            {
                lock (NotesPlayed)
                {
                    NotesPlayed.Clear();
                    noteDrawer = new NoteDrawer();
                    MusicSheetGraphics = panelNotes.CreateGraphics();
                    SolidBrush brush = new SolidBrush(Color.Black);
                    noteDrawer.Draw(MusicSheetGraphics, Color.White, brush, brush);
                }
            }
        }

        private void buttonNewRecord_Click(object sender, EventArgs e)
        {
            lock (noteDrawer)
            {
                lock (NotesPlayed)
                {
                    NotesPlayed.Clear();
                    noteDrawer = new NoteDrawer();
                    MusicSheetGraphics = panelNotes.CreateGraphics();
                    SolidBrush brush = new SolidBrush(Color.Black);
                    noteDrawer.Draw(MusicSheetGraphics, Color.White, brush, brush);
                }
            }
        }

        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                RecordingData recordingData = new RecordingData();
                recordingData.Notes = NotesPlayed;
                recordingData.Title = toolStripTextBoxTitle.Text;
                recordingData.Composer = toolStripTextBoxComposer.Text;
                string path = tempPDFFilePath + Guid.NewGuid() + ".pdf";
                FilesHandler.CreatePDFfile(recordingData, path);

                webBrowser1.Navigate(path);
                webBrowser1.Refresh(WebBrowserRefreshOption.Completely);
            }
            catch (Exception ex)
            {
                DelegatesUI.SetControlEnabled(this, groupBoxRecord, false);
                DelegatesUI.SetControlEnabled(this, groupBoxPlayingRecord, false);
                DelegatesUI.SetControlEnabled(this, groupBoxFileRecord, false);

                MessageBox.Show(ex.Message + "\n\n Please check the software configuration", "Recorder By Hila Shmuel");

                ThreadStart StopRecordThreadStart = new ThreadStart(StopRecord);
                Thread StopRecordThread = new Thread(StopRecordThreadStart);
                StopRecordThread.Start();
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
                groupBoxPlayingRecord.Enabled = false;

                // clear notes buffer
                NotesPlayed.Clear();

                //remove temp file, and create a new one instead
                if (null != tempWaveFilePath)
                {
                    File.Delete(tempWaveFilePath);
                }

                // open the waveform-audio input device, and start recording
                tempWaveFilePath = Path.GetTempFileName() + "Record.wav";
                Wave.startDevice((uint)(comboBoxInputDevices.SelectedIndex - 1), tempWaveFilePath);

                isRecording = true; 

                ThreadStart  recordingLoopThreadStart = new ThreadStart(RecordingLoop);
                recordingLoopThread = new Thread(recordingLoopThreadStart);
                recordingLoopThread.Start();

                if (null != recorderHero)
                {
                    recorderHero.SleepMultiplier = GetTrackBarSleepMultiplier();
                    recorderHero.Start();
                }
                
                buttonStopRecord.Enabled = true;
            }
            catch (Exception ex)
            {
                string error_message = "error: start record: " + ex.Message + "Please re-connect the microphone and run the program again.";
                MessageBox.Show(error_message, "Recorder By Hila Shmuel");

                groupBoxRecord.Enabled = false;
                groupBoxPlayingRecord.Enabled = false;
                groupBoxFileRecord.Enabled = false;
            }
        }

        private void buttonStopRecord_Click(object sender, EventArgs e)
        {
            try
            {
                groupBoxRecord.Enabled = false;
                groupBoxPlayingRecord.Enabled = false;

                ThreadStart StopRecordThreadStart = new ThreadStart(StopRecord);
                Thread StopRecordThread = new Thread(StopRecordThreadStart);
                StopRecordThread.Start();
            }
            catch (Exception ex)
            {
                string error_message = "error: stop record: " + ex.Message;
                MessageBox.Show(error_message, "Recorder By Hila Shmuel");

                groupBoxRecord.Enabled = false;
                groupBoxPlayingRecord.Enabled = false;
                groupBoxFileRecord.Enabled = false;
            }
        }

        private void comboBoxInputDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxInputDevices.SelectedIndex == 0)
            {
                buttonStartRecord.Enabled = false;
                buttonStopRecord.Enabled = false;
                //buttonStartPlayingRecord.Enabled = false;
            }
            else
            {
                buttonStartRecord.Enabled = true;
                buttonStopRecord.Enabled = false;
                //buttonStartPlayingRecord.Enabled = false;
            }
        }

        /***** Controllers Events - Playing Record ***********************************************/
        
        private void buttonPlayRecord_Click(object sender, EventArgs e)
        {
            try
            {
                groupBoxRecord.Enabled = false;
                groupBoxPlayingRecord.Enabled = false;

                // determine which playing method to chose
                switch (comboBoxPlayingRecordFormats.SelectedItem.ToString())
                {
                    case "Beep":
                        musicPlayer = new PlayNotesByBeep();
                        musicPlayer.AddNotes(NotesPlayed);
                        break;
                    case "WAVE (synthesized)":
                        musicPlayer = new PlayNotesByWave();
                        musicPlayer.AddNotes(NotesPlayed);
                        break;
                    case "WAVE (as recorded)":
                        musicPlayer = new PlayNotesByWaveFile(tempWaveFilePath);
                        break;
                    default:
                        string error_message = "Unknown exception: buttonPlayRecord_Click: unknown playing method";
                        MessageBox.Show(error_message, "Recorder By Hila Shmuel");
                        break;
                }

                ThreadStart StartPlayingRecordThreadStart = new ThreadStart(StartPlayingRecord);
                StartPlayingRecordThread = new Thread(StartPlayingRecordThreadStart);
                StartPlayingRecordThread.Start();

                groupBoxPlayingRecord.Enabled =true;
                buttonStopPlayingRecord.Enabled = true;
                buttonPausePlayingRecord.Enabled = true;
                buttonStartPlayingRecord.Enabled = false;
                comboBoxPlayingRecordFormats.Enabled = false;
            }
            catch (Exception ex)
            {
                string error_message = "error: playing record: " + ex.Message;
                MessageBox.Show(error_message, "Recorder By Hila Shmuel");
                
                groupBoxRecord.Enabled = false;
                groupBoxPlayingRecord.Enabled = false;
                groupBoxFileRecord.Enabled = false;
            }
        }

        private void buttonPausePlayingRecord_Click(object sender, EventArgs e)
        {
            try
            {
                groupBoxRecord.Enabled = false;
                groupBoxPlayingRecord.Enabled = false;

                ThreadStart PausePlayingRecordThreadStart = new ThreadStart(PausePlayingRecord);
                Thread PausePlayingRecordThread = new Thread(PausePlayingRecordThreadStart);
                PausePlayingRecordThread.Start();

            }
            catch (Exception ex)
            {
                string error_message = "error: pause playing record: " + ex.Message;
                MessageBox.Show(error_message, "Recorder By Hila Shmuel");

                groupBoxRecord.Enabled = false;
                groupBoxPlayingRecord.Enabled = false;
                groupBoxFileRecord.Enabled = false;
            }
        }  
 
        private void buttonStopPlayingRecord_Click(object sender, EventArgs e)
        {
            try
            {
                groupBoxRecord.Enabled = false;
                groupBoxPlayingRecord.Enabled = false;

                ThreadStart StopPlayingRecordThreadStart = new ThreadStart(StopPlayingRecord);
                Thread StopPlayingRecordThread = new Thread(StopPlayingRecordThreadStart);
                StopPlayingRecordThread.Start();

            }
            catch (Exception ex)
            {
                string error_message = "error: stop playing record: " + ex.Message;
                MessageBox.Show(error_message, "Recorder By Hila Shmuel");

                groupBoxRecord.Enabled = false;
                groupBoxPlayingRecord.Enabled = false;
                groupBoxFileRecord.Enabled = false;
            }
        } 

        /***** Controllers Events - Saving Record ************************************************/

        private void buttonSaveRecord_Click(object sender, EventArgs e)
        {
            SavingRecordForm savingRecordForm = new SavingRecordForm(NotesPlayed, tempWaveFilePath);
            savingRecordForm.ShowDialog();
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
            MusicSheetGraphics = panelNotes.CreateGraphics();
            SolidBrush brush = new SolidBrush(Color.Black);
            noteDrawer.Draw(MusicSheetGraphics, Color.White, brush, brush);
        }

        /***** Controllers Events - User Interface ***********************************************/
        
        private void tabControl_Selected(object sender, TabControlEventArgs e)
        {
            if (tabControl.SelectedTab.Name == "tabPageWaveform")
            {
                printGraphsEnabled = true;
                // graphics
                timeDomainGraphics = pictureBoxTimeDomain.CreateGraphics();
                frequencyDomainGraphics = pictureBoxFrequencyDomain.CreateGraphics();
                return;
            }

            printGraphsEnabled = false;
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

        /***** Form's Threads - Recording ********************************************************/
        
        private void RecordingLoop()
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
                    float maxPitch = (float)furierTransform.preformTranform(FurierTransform.TRANSFORM.FFT, WaveArray, WaveArrayFrequency, NumberOfSamples, SamplesPerSec);
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

                    if (!isRecording) { return; }
                    DelegatesUI.SetControlText(this, textBoxPitch, maxPitch.ToString());
                    DelegatesUI.SetControlText(this, textBoxNote, currentNote.NoteDescription);
                    DelegatesUI.SetControlText(this, textBoxMIDI, currentNote.MIDI.ToString());

                    //draw recorder fingering
                    //fingeringsPicture.createPicture(pictureRecorderGraphics, currentNote);

                    // If playing RecorderHero, send the note to the RecorderHero
                    if (null != recorderHero && recorderHero.isPlaying == true)
                    {
                        recorderHero.UpdateCurrentNote(currentNote);
                        fingeringsPicture.createPicture(pictureRecorderGraphics, recorderHero.GetCurrentNote());
                    }
                    else 
                    {
                        //draw recorder fingering
                        fingeringsPicture.createPicture(pictureRecorderGraphics, currentNote);
                    }
                    
                    // Detect the note based on the last and current samples
                    Note detectedNote = notesDetector.DetectNote(currentNote, Environment.TickCount);

                    if (null != detectedNote)
                    {
                        if (prevNote == null || prevNote.MIDI != detectedNote.MIDI)
                        {
                            // Draw note in interactive music sheet
                            if (72 <= detectedNote.MIDI && detectedNote.MIDI <= 99)
                            {
                                NotesPlayed.Add(detectedNote);

                                noteDrawer.Draw(MusicSheetGraphics, Color.White, brush, brush, detectedNote);
                            }
                        }
                        else
                        {
                            // a new instance of the same note
                            if (detectedNote.Duration < prevNote.Duration)
                            {
                                NotesPlayed.Add(detectedNote);

                                noteDrawer.Draw(MusicSheetGraphics, Color.White, brush, brush, detectedNote);
                            }

                            // redraw note
                            //noteDrawer.RedrawNote(MusicSheetGraphics, brush, detectedNote, -1);
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

                DelegatesUI.SetControlEnabled(this, groupBoxRecord, false);
                DelegatesUI.SetControlEnabled(this, groupBoxPlayingRecord, false);
                DelegatesUI.SetControlEnabled(this, groupBoxFileRecord, false);

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
                if (isRecording)
                {
                    isRecording = false;
                    recordingLoopThread.Join();

                    // stop the waveform-audio input device recording
                    Wave.stopDevice();
                }
                isRecording = false;

                if (null != recorderHero)
                {
                    recorderHero.Stop();
                }

                DelegatesUI.SetControlEnabled(this, groupBoxRecord, true);
                DelegatesUI.SetControlEnabled(this, groupBoxPlayingRecord, true);

                DelegatesUI.SetControlEnabled(this, comboBoxInputDevices, true);
                DelegatesUI.SetControlEnabled(this, buttonStartRecord, true);
                DelegatesUI.SetControlEnabled(this, buttonStopRecord, false);
                DelegatesUI.SetControlEnabled(this, buttonStartPlayingRecord, true);
            }
            catch (Exception ex)
            {
                string error_message;

                if (ex is WaveInException || ex is MCIException)
                {
                    error_message = error_message = "stop record thread: " + ex.Message + "Please re-connect the microphone and run the program again.";
                }
                else
                {
                    error_message = "Unknown exception: stop record thread: " + ex.Message;
                }

                MessageBox.Show(error_message, "Recorder By Hila Shmuel");
                DelegatesUI.SetControlEnabled(this, groupBoxRecord, false);
                DelegatesUI.SetControlEnabled(this, groupBoxPlayingRecord, false);
                DelegatesUI.SetControlEnabled(this, groupBoxFileRecord, false);
            }
        }

        /***** Form's Threads - Playing Record ***************************************************/

        private void StartPlayingRecord()
        {
            try
            {
                musicPlayer.Play();
            }
            catch (Exception ex)
            {
                string error_message = "error playing record: " + ex.Message;
                MessageBox.Show(error_message, "Recorder By Hila Shmuel");

                DelegatesUI.SetControlEnabled(this, groupBoxRecord, false);
                DelegatesUI.SetControlEnabled(this, groupBoxPlayingRecord, false);
                DelegatesUI.SetControlEnabled(this, groupBoxFileRecord, false);
            }
        }

        private void PausePlayingRecord()
        {
            try
            {
                musicPlayer.Pause();
                StartPlayingRecordThread.Join();

                DelegatesUI.SetControlEnabled(this, groupBoxRecord, true);
                DelegatesUI.SetControlEnabled(this, groupBoxPlayingRecord, true);

                DelegatesUI.SetControlEnabled(this, buttonStartPlayingRecord, true);
                DelegatesUI.SetControlEnabled(this, buttonPausePlayingRecord, false);
                DelegatesUI.SetControlEnabled(this, buttonStopPlayingRecord, true);
                DelegatesUI.SetControlEnabled(this, comboBoxPlayingRecordFormats, false);
            }
            catch (Exception ex)
            {
                string error_message = "error playing record: " + ex.Message;
                MessageBox.Show(error_message, "Recorder By Hila Shmuel");

                DelegatesUI.SetControlEnabled(this, groupBoxRecord, false);
                DelegatesUI.SetControlEnabled(this, groupBoxPlayingRecord, false);
                DelegatesUI.SetControlEnabled(this, groupBoxFileRecord, false);
            }
        }

        private void StopPlayingRecord()
        {
            try
            {
                musicPlayer.Stop();
                StartPlayingRecordThread.Join();

                DelegatesUI.SetControlEnabled(this, groupBoxRecord, true);
                DelegatesUI.SetControlEnabled(this, groupBoxPlayingRecord, true);

                DelegatesUI.SetControlEnabled(this, buttonStartPlayingRecord, true);
                DelegatesUI.SetControlEnabled(this, buttonPausePlayingRecord, false);
                DelegatesUI.SetControlEnabled(this, buttonStopPlayingRecord, false);
                DelegatesUI.SetControlEnabled(this, comboBoxPlayingRecordFormats, true);
            }
            catch (Exception ex)
            {
                string error_message = "error playing record: " + ex.Message;
                MessageBox.Show(error_message, "Recorder By Hila Shmuel");

                DelegatesUI.SetControlEnabled(this, groupBoxRecord, false);
                DelegatesUI.SetControlEnabled(this, groupBoxPlayingRecord, false);
                DelegatesUI.SetControlEnabled(this, groupBoxFileRecord, false);
            }
        }

        /***** Form's Functions - New, Open, Save File *******************************************/

        private void openABCfile()
        {
            // open a new ABC file, convert to Note objects, and display it in a new tab

            //try
            {
                // open ABC file
                DialogResult result = openFileDialogABC.ShowDialog();
                if (result != DialogResult.OK)
                {
                    return;
                }

                if (null != recorderHero)
                {
                    recorderHero.Dispose();
                }
                recorderHero = new RecorderHero(splitContainerRecorderHero.Panel1.CreateGraphics(), openFileDialogABC.FileName);
                recorderHero.SleepMultiplier = GetTrackBarSleepMultiplier();
                //recorderHero.SleepMultiplier = track

                if (isRecording)
                {
                    recorderHero.Start();
                }
                tabControl.SelectedIndex = 4;
            }
            //catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }
        }

        private void splitContainerRecorderHero_Panel1_Paint(object sender, PaintEventArgs e)
        {
            if (null != recorderHero)
            {
                recorderHero.OnPaint(splitContainerRecorderHero.Panel1.CreateGraphics());
            }
            else
            {
                splitContainerRecorderHero.Panel1.BackColor = Color.WhiteSmoke;
                Graphics graphics = splitContainerRecorderHero.Panel1.CreateGraphics();
                RectangleF graphicsSize = graphics.VisibleClipBounds;
                Size imageSize = Recorder.Properties.Resources.BalanceTheStraw.Size;

                //graphics.DrawImage(Recorder.Properties.Resources.BalanceTheStraw,
                //    (graphicsSize.Width - imageSize.Width) / 2,
                //    (graphicsSize.Height - imageSize.Height) / 2);

                string recorderHeroUsage = "Open an ABC music notation file,\n and start playing the recorder!";
                Font font = new Font("Gisha", 24);
                graphics.DrawString(recorderHeroUsage,
                    font,
                    new SolidBrush(Color.Black),
                    new PointF(graphicsSize.X + graphicsSize.Width / 4,
                        graphicsSize.Y + graphicsSize.Height / 4));
            }
        }

        private float GetTrackBarSleepMultiplier()
        {
            float precent = trackBarRecorderHero.Value;

            if (precent <= 50)
            {
                return (-9F / 50F) * precent + 10F;
            }
            else
            {
                return (-9F / 500F) * precent + 1.9F;
            }
        }

        private void trackBarRecorderHero_ValueChanged(object sender, EventArgs e)
        {
            if (null != recorderHero)
            {
                recorderHero.SleepMultiplier = GetTrackBarSleepMultiplier();
            }
        }

    }
}
