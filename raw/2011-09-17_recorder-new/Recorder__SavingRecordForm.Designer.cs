namespace Recorder
{
    partial class SavingRecordForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.checkedListBoxFileFormats = new System.Windows.Forms.CheckedListBox();
            this.groupBoxFileFormats = new System.Windows.Forms.GroupBox();
            this.groupBoxRecordDetails = new System.Windows.Forms.GroupBox();
            this.labelComposer = new System.Windows.Forms.Label();
            this.textBoxComposer = new System.Windows.Forms.TextBox();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.groupBoxFileDetails = new System.Windows.Forms.GroupBox();
            this.buttonDirectory = new System.Windows.Forms.Button();
            this.labelDirectoryPath = new System.Windows.Forms.Label();
            this.textBoxDirectoryPath = new System.Windows.Forms.TextBox();
            this.labelFileName = new System.Windows.Forms.Label();
            this.textBoxFileName = new System.Windows.Forms.TextBox();
            this.buttonSaveFile = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxFileFormats.SuspendLayout();
            this.groupBoxRecordDetails.SuspendLayout();
            this.groupBoxFileDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // checkedListBoxFileFormats
            // 
            this.checkedListBoxFileFormats.BackColor = System.Drawing.SystemColors.Control;
            this.checkedListBoxFileFormats.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBoxFileFormats.FormattingEnabled = true;
            this.checkedListBoxFileFormats.Items.AddRange(new object[] {
            "MIDI",
            "WAVE",
            "PostScript",
            "ABC",
            "PDF"});
            this.checkedListBoxFileFormats.Location = new System.Drawing.Point(6, 23);
            this.checkedListBoxFileFormats.Name = "checkedListBoxFileFormats";
            this.checkedListBoxFileFormats.Size = new System.Drawing.Size(74, 75);
            this.checkedListBoxFileFormats.TabIndex = 0;
            this.checkedListBoxFileFormats.ThreeDCheckBoxes = true;
            this.checkedListBoxFileFormats.Validating += new System.ComponentModel.CancelEventHandler(this.checkedListBoxFileFormats_Validating);
            // 
            // groupBoxFileFormats
            // 
            this.groupBoxFileFormats.Controls.Add(this.checkedListBoxFileFormats);
            this.groupBoxFileFormats.Location = new System.Drawing.Point(12, 12);
            this.groupBoxFileFormats.Name = "groupBoxFileFormats";
            this.groupBoxFileFormats.Size = new System.Drawing.Size(111, 102);
            this.groupBoxFileFormats.TabIndex = 1;
            this.groupBoxFileFormats.TabStop = false;
            this.groupBoxFileFormats.Text = "File Formats";
            // 
            // groupBoxRecordDetails
            // 
            this.groupBoxRecordDetails.Controls.Add(this.labelComposer);
            this.groupBoxRecordDetails.Controls.Add(this.textBoxComposer);
            this.groupBoxRecordDetails.Controls.Add(this.textBoxTitle);
            this.groupBoxRecordDetails.Controls.Add(this.labelTitle);
            this.groupBoxRecordDetails.Location = new System.Drawing.Point(129, 12);
            this.groupBoxRecordDetails.Name = "groupBoxRecordDetails";
            this.groupBoxRecordDetails.Size = new System.Drawing.Size(316, 103);
            this.groupBoxRecordDetails.TabIndex = 2;
            this.groupBoxRecordDetails.TabStop = false;
            this.groupBoxRecordDetails.Text = "Record Details";
            // 
            // labelComposer
            // 
            this.labelComposer.AutoSize = true;
            this.labelComposer.Location = new System.Drawing.Point(7, 48);
            this.labelComposer.Name = "labelComposer";
            this.labelComposer.Size = new System.Drawing.Size(54, 13);
            this.labelComposer.TabIndex = 3;
            this.labelComposer.Text = "Composer";
            // 
            // textBoxComposer
            // 
            this.textBoxComposer.Location = new System.Drawing.Point(111, 45);
            this.textBoxComposer.Name = "textBoxComposer";
            this.textBoxComposer.Size = new System.Drawing.Size(174, 20);
            this.textBoxComposer.TabIndex = 2;
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Location = new System.Drawing.Point(111, 19);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(174, 20);
            this.textBoxTitle.TabIndex = 1;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(7, 22);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(27, 13);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Title";
            // 
            // groupBoxFileDetails
            // 
            this.groupBoxFileDetails.Controls.Add(this.buttonDirectory);
            this.groupBoxFileDetails.Controls.Add(this.labelDirectoryPath);
            this.groupBoxFileDetails.Controls.Add(this.textBoxDirectoryPath);
            this.groupBoxFileDetails.Controls.Add(this.labelFileName);
            this.groupBoxFileDetails.Controls.Add(this.textBoxFileName);
            this.groupBoxFileDetails.Location = new System.Drawing.Point(12, 123);
            this.groupBoxFileDetails.Name = "groupBoxFileDetails";
            this.groupBoxFileDetails.Size = new System.Drawing.Size(433, 81);
            this.groupBoxFileDetails.TabIndex = 3;
            this.groupBoxFileDetails.TabStop = false;
            this.groupBoxFileDetails.Text = "File Details";
            // 
            // buttonDirectory
            // 
            this.buttonDirectory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDirectory.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonDirectory.Image = global::Recorder.Properties.Resources.directoryIcon;
            this.buttonDirectory.Location = new System.Drawing.Point(83, 17);
            this.buttonDirectory.Name = "buttonDirectory";
            this.buttonDirectory.Size = new System.Drawing.Size(28, 23);
            this.buttonDirectory.TabIndex = 4;
            this.buttonDirectory.UseVisualStyleBackColor = true;
            this.buttonDirectory.Click += new System.EventHandler(this.buttonDirectory_Click);
            // 
            // labelDirectoryPath
            // 
            this.labelDirectoryPath.AutoSize = true;
            this.labelDirectoryPath.Location = new System.Drawing.Point(6, 22);
            this.labelDirectoryPath.Name = "labelDirectoryPath";
            this.labelDirectoryPath.Size = new System.Drawing.Size(74, 13);
            this.labelDirectoryPath.TabIndex = 3;
            this.labelDirectoryPath.Text = "Directory Path";
            // 
            // textBoxDirectoryPath
            // 
            this.textBoxDirectoryPath.Location = new System.Drawing.Point(108, 19);
            this.textBoxDirectoryPath.Name = "textBoxDirectoryPath";
            this.textBoxDirectoryPath.ReadOnly = true;
            this.textBoxDirectoryPath.Size = new System.Drawing.Size(294, 20);
            this.textBoxDirectoryPath.TabIndex = 2;
            this.textBoxDirectoryPath.Click += new System.EventHandler(this.textBoxDirectoryPath_Click);
            this.textBoxDirectoryPath.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxDirectoryPath_Validating);
            this.textBoxDirectoryPath.Validated += new System.EventHandler(this.textBoxDirectoryPath_Validated);
            // 
            // labelFileName
            // 
            this.labelFileName.AutoSize = true;
            this.labelFileName.Location = new System.Drawing.Point(6, 48);
            this.labelFileName.Name = "labelFileName";
            this.labelFileName.Size = new System.Drawing.Size(54, 13);
            this.labelFileName.TabIndex = 1;
            this.labelFileName.Text = "File Name";
            // 
            // textBoxFileName
            // 
            this.textBoxFileName.Location = new System.Drawing.Point(108, 45);
            this.textBoxFileName.Name = "textBoxFileName";
            this.textBoxFileName.Size = new System.Drawing.Size(294, 20);
            this.textBoxFileName.TabIndex = 0;
            this.textBoxFileName.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxFileName_Validating);
            this.textBoxFileName.Validated += new System.EventHandler(this.textBoxFileName_Validated);
            // 
            // buttonSaveFile
            // 
            this.buttonSaveFile.Location = new System.Drawing.Point(370, 210);
            this.buttonSaveFile.Name = "buttonSaveFile";
            this.buttonSaveFile.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveFile.TabIndex = 4;
            this.buttonSaveFile.Text = "Save Files";
            this.buttonSaveFile.UseVisualStyleBackColor = true;
            this.buttonSaveFile.Click += new System.EventHandler(this.buttonSaveFile_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(289, 210);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // SavingRecordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(457, 245);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSaveFile);
            this.Controls.Add(this.groupBoxFileDetails);
            this.Controls.Add(this.groupBoxRecordDetails);
            this.Controls.Add(this.groupBoxFileFormats);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SavingRecordForm";
            this.ShowIcon = false;
            this.Text = "Saving Record As";
            this.Validating += new System.ComponentModel.CancelEventHandler(this.SavingRecordForm_Validating);
            this.Validated += new System.EventHandler(this.SavingRecordForm_Validated);
            this.groupBoxFileFormats.ResumeLayout(false);
            this.groupBoxRecordDetails.ResumeLayout(false);
            this.groupBoxRecordDetails.PerformLayout();
            this.groupBoxFileDetails.ResumeLayout(false);
            this.groupBoxFileDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.CheckedListBox checkedListBoxFileFormats;
        private System.Windows.Forms.GroupBox groupBoxFileFormats;
        private System.Windows.Forms.GroupBox groupBoxRecordDetails;
        private System.Windows.Forms.GroupBox groupBoxFileDetails;
        private System.Windows.Forms.Label labelFileName;
        private System.Windows.Forms.TextBox textBoxFileName;
        private System.Windows.Forms.Label labelComposer;
        private System.Windows.Forms.TextBox textBoxComposer;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button buttonSaveFile;
        private System.Windows.Forms.TextBox textBoxDirectoryPath;
        private System.Windows.Forms.Label labelDirectoryPath;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button buttonDirectory;
        private System.Windows.Forms.Button buttonCancel;

    }
}