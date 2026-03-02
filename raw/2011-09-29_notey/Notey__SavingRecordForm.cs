using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Notes;
using System.IO;

namespace Recorder
{
    public partial class SavingRecordForm : Form
    {
        RecordingData recordingData;
        string WaveRecordingfilePath;

        public SavingRecordForm(List<Note> notes, string waveRecordingfilePath)
        {
            InitializeComponent();
            recordingData = new RecordingData();
            recordingData.Notes = notes;
            WaveRecordingfilePath = waveRecordingfilePath;
            TryToEnableSave();
        }

        private void textBoxFileName_Validated(object sender, EventArgs e)
        {
            ValidateFileName();
            TryToEnableSave();
        }

        private void textBoxDirectoryPath_Click(object sender, EventArgs e)
        {
            SelectDirectory();
            TryToEnableSave();
        }

        private void buttonDirectory_Click(object sender, EventArgs e)
        {
            SelectDirectory();
            TryToEnableSave();
        }

        private void textBoxDirectoryPath_Validated(object sender, EventArgs e)
        {
            ValidateDirectoyPath();
            TryToEnableSave();
        }    

        private void SelectDirectory()
        {
            DialogResult folderBrowserDialogResult = folderBrowserDialog1.ShowDialog();
            if (DialogResult.OK == folderBrowserDialogResult)
            {
                textBoxDirectoryPath.Text = folderBrowserDialog1.SelectedPath;
                errorProvider1.SetError(textBoxDirectoryPath, "");
            }
            else if (DialogResult.Cancel == folderBrowserDialogResult)
            {
                errorProvider1.SetError(textBoxDirectoryPath, "");
            }
            else
            {
                errorProvider1.SetError(textBoxDirectoryPath, "Invalid Directory Path");
            }
        }

        private bool ValidateFileFormats()
        {
            if (0 == checkedListBoxFileFormats.CheckedItems.Count)
            {
                errorProvider1.SetError(checkedListBoxFileFormats, "You must chose at least one file format");
                return false;
            }

            errorProvider1.SetError(checkedListBoxFileFormats, "");
            return true;
        }

        private bool ValidateFileName()
        {
            if (textBoxFileName.Text.Length == 0)
            {
                errorProvider1.SetError(textBoxFileName, "File Name must contain at least one character");
                return false;
            }

            char[] invalidChars = Path.GetInvalidFileNameChars();
            foreach (char c in textBoxFileName.Text)
            {
                if (invalidChars.Contains(c))
                {
                    string invalidString = "A filename cannot contain any of the following characters:\n\\ / : * ? \" < > | ";
                    errorProvider1.SetError(textBoxFileName,
                        "File name must not contains any of this charcters:" + invalidString);
                    return false;
                }
            }

            errorProvider1.SetError(textBoxFileName, "");
            return true;
        }

        private bool ValidateDirectoyPath()
        {
            if (textBoxDirectoryPath.Text.Length == 0)
            {
                errorProvider1.SetError(textBoxDirectoryPath, "Invalid Directory Path");
                return false;
            }

            errorProvider1.SetError(textBoxDirectoryPath, "");
            return true;
        }

        private void TryToEnableSave()
        {
            bool ValidateFileFormatsResult = ValidateFileFormats();
            bool ValidateFileNameResult = ValidateFileName();
            bool ValidateDirectoyPathResult = ValidateDirectoyPath();

            if (ValidateFileFormatsResult &&
                ValidateFileNameResult &&
                ValidateDirectoyPathResult)
            {
                buttonSaveFile.Enabled = true;
            }
            else
            {
                buttonSaveFile.Enabled = false;
            }
        }

        private void SavingRecordForm_Validated(object sender, EventArgs e)
        {
            TryToEnableSave();
        }

        private void SavingRecordForm_Validating(object sender, CancelEventArgs e)
        {
            TryToEnableSave();
        }

        private void checkedListBoxFileFormats_Validating(object sender, CancelEventArgs e)
        {
            TryToEnableSave();
        }

        private void textBoxDirectoryPath_Validating(object sender, CancelEventArgs e)
        {
            TryToEnableSave();
        }

        private void textBoxFileName_Validating(object sender, CancelEventArgs e)
        {
            TryToEnableSave();
        }

        private void buttonSaveFile_Click(object sender, EventArgs e)
        {
            try
            {
                string ABCfilePath = Path.Combine(textBoxDirectoryPath.Text, textBoxFileName.Text + ".ABC");
                string postScriptFilePath = Path.Combine(textBoxDirectoryPath.Text, textBoxFileName.Text + ".PS");
                string PDFfilePath = Path.Combine(textBoxDirectoryPath.Text, textBoxFileName.Text + ".PDF");
                string WAVEfilePath = Path.Combine(textBoxDirectoryPath.Text, textBoxFileName.Text + ".WAV");

                recordingData.Title = textBoxTitle.Text;
                recordingData.Composer = textBoxComposer.Text;

                // TODO: raise an exception and catch it down
                if (recordingData.Title.Length < 3)
                {
                    MessageBox.Show("MORE THAN 3");
                    return;
                }

                // Create ABC file
                if (checkedListBoxFileFormats.CheckedItems.Contains("ABC"))
                {
                    FilesHandler.CreateABCfile(recordingData, ABCfilePath);
                }

                // Create PostScript file
                if (checkedListBoxFileFormats.CheckedItems.Contains("PostScript"))
                {
                    // if already created ABC
                    if (checkedListBoxFileFormats.CheckedItems.Contains("ABC"))
                    {
                        FilesHandler.CreatePostScriptFileFromABC(ABCfilePath, postScriptFilePath);
                    }
                    else
                    {
                        FilesHandler.CreatePostScriptfile(recordingData, postScriptFilePath);
                    }
                }

                // Create PDF file
                if (checkedListBoxFileFormats.CheckedItems.Contains("PDF"))
                {
                    // if already created ABC
                    if (checkedListBoxFileFormats.CheckedItems.Contains("PostScript"))
                    {
                        FilesHandler.CreatePDFfileFromPostScript(postScriptFilePath, PDFfilePath);
                    }
                    else if (checkedListBoxFileFormats.CheckedItems.Contains("ABC"))
                    {
                        FilesHandler.CreatePDFfileFromABC(ABCfilePath, PDFfilePath);
                    }
                    else
                    {
                        FilesHandler.CreatePDFfile(recordingData, PDFfilePath);
                    }
                }

                // Create WAVE file
                /*if (checkedListBoxFileFormats.CheckedItems.Contains("WAVE"))
                {
                    File.Copy(WaveRecordingfilePath, WAVEfilePath);            
                }*/
                
                buttonSaveFile.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch
            {
            }
        }
    }

    public class RecordingData
    {
        public List<Note> Notes;
        public string Title;
        public string Composer;
        public float DefaultNoteLength = 0.25F;
    }
}
