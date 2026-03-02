using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace Recorder
{
    public partial class ConfigurationForm : Form
    {

        /// <summary>
        /// Let the user changes the programm configurations
        /// </summary>
        public ConfigurationForm()
        {
            InitializeComponent();
            textBoxGhostScriptPath.Text = Properties.Settings.Default.GhostScriptPath;
            textBoxAbc2PsPath.Text = Properties.Settings.Default.abcm2psPath;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.GhostScriptPath = textBoxGhostScriptPath.Text;
            Properties.Settings.Default.abcm2psPath =  textBoxAbc2PsPath.Text;
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void SelectFile(TextBox textBox)
        {
            if (textBox == textBoxGhostScriptPath)
            {
                openFileDialogGhostScript.FileName = Properties.Settings.Default.GhostScriptPath;
            }
            else if (textBox == textBoxAbc2PsPath)
            {
                openFileDialogGhostScript.FileName = Properties.Settings.Default.abcm2psPath;
            }
            else
            {
                openFileDialogGhostScript.FileName = "";
            }
            
            DialogResult openFileDialogResult  = openFileDialogGhostScript.ShowDialog();
            textBox.Text = openFileDialogGhostScript.FileName;

            if (!openFileDialogGhostScript.CheckFileExists)
            {
                errorProvider1.SetError(textBox, "Invalid File");
            }
            else
            {
                errorProvider1.SetError(textBox, "");
            }
        }

        private void buttonGhostScriptDirectory_Click(object sender, EventArgs e)
        {
            SelectFile(textBoxGhostScriptPath);
        }

        private void textBoxGhostScriptPath_Click(object sender, EventArgs e)
        {
            SelectFile(textBoxGhostScriptPath);
        }

        private void buttonAbc2PsDirectory_Click(object sender, EventArgs e)
        {
            SelectFile(textBoxAbc2PsPath);
        }

        private void textBoxAbc2PsPath_Click(object sender, EventArgs e)
        {
            SelectFile(textBoxAbc2PsPath);
        }
    }
}