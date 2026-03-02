namespace Recorder
{
    partial class ConfigurationForm
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
            this.labelConfigurationExplenation = new System.Windows.Forms.Label();
            this.groupBoxGhostScript = new System.Windows.Forms.GroupBox();
            this.pictureBoxGhostScriptLogo = new System.Windows.Forms.PictureBox();
            this.buttonGhostScriptDirectory = new System.Windows.Forms.Button();
            this.textBoxGhostScriptDescription = new System.Windows.Forms.TextBox();
            this.textBoxGhostScriptPath = new System.Windows.Forms.TextBox();
            this.labelGhostScriptPath = new System.Windows.Forms.Label();
            this.textBoxConfigurationDescription = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.openFileDialogGhostScript = new System.Windows.Forms.OpenFileDialog();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBoxAbc2Ps = new System.Windows.Forms.GroupBox();
            this.textBoxAbc2PsDescription = new System.Windows.Forms.TextBox();
            this.buttonAbc2PsDirectory = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxAbc2PsPath = new System.Windows.Forms.TextBox();
            this.labelAbc2PsPath = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBoxGhostScript.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGhostScriptLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBoxAbc2Ps.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelConfigurationExplenation
            // 
            this.labelConfigurationExplenation.AutoSize = true;
            this.labelConfigurationExplenation.Location = new System.Drawing.Point(12, 9);
            this.labelConfigurationExplenation.Name = "labelConfigurationExplenation";
            this.labelConfigurationExplenation.Size = new System.Drawing.Size(0, 13);
            this.labelConfigurationExplenation.TabIndex = 0;
            this.labelConfigurationExplenation.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // groupBoxGhostScript
            // 
            this.groupBoxGhostScript.Controls.Add(this.pictureBoxGhostScriptLogo);
            this.groupBoxGhostScript.Controls.Add(this.buttonGhostScriptDirectory);
            this.groupBoxGhostScript.Controls.Add(this.textBoxGhostScriptDescription);
            this.groupBoxGhostScript.Controls.Add(this.textBoxGhostScriptPath);
            this.groupBoxGhostScript.Controls.Add(this.labelGhostScriptPath);
            this.groupBoxGhostScript.Location = new System.Drawing.Point(6, 6);
            this.groupBoxGhostScript.Name = "groupBoxGhostScript";
            this.groupBoxGhostScript.Size = new System.Drawing.Size(452, 120);
            this.groupBoxGhostScript.TabIndex = 1;
            this.groupBoxGhostScript.TabStop = false;
            this.groupBoxGhostScript.Text = "GhostScript";
            // 
            // pictureBoxGhostScriptLogo
            // 
            this.pictureBoxGhostScriptLogo.Image = global::Recorder.Properties.Resources.ghostscriptPicture;
            this.pictureBoxGhostScriptLogo.Location = new System.Drawing.Point(376, 17);
            this.pictureBoxGhostScriptLogo.Name = "pictureBoxGhostScriptLogo";
            this.pictureBoxGhostScriptLogo.Size = new System.Drawing.Size(70, 64);
            this.pictureBoxGhostScriptLogo.TabIndex = 4;
            this.pictureBoxGhostScriptLogo.TabStop = false;
            // 
            // buttonGhostScriptDirectory
            // 
            this.buttonGhostScriptDirectory.FlatAppearance.BorderSize = 0;
            this.buttonGhostScriptDirectory.Image = global::Recorder.Properties.Resources.directoryPicture;
            this.buttonGhostScriptDirectory.Location = new System.Drawing.Point(411, 87);
            this.buttonGhostScriptDirectory.Name = "buttonGhostScriptDirectory";
            this.buttonGhostScriptDirectory.Size = new System.Drawing.Size(35, 23);
            this.buttonGhostScriptDirectory.TabIndex = 3;
            this.buttonGhostScriptDirectory.UseVisualStyleBackColor = true;
            this.buttonGhostScriptDirectory.Click += new System.EventHandler(this.buttonGhostScriptDirectory_Click);
            // 
            // textBoxGhostScriptDescription
            // 
            this.textBoxGhostScriptDescription.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxGhostScriptDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxGhostScriptDescription.Location = new System.Drawing.Point(9, 19);
            this.textBoxGhostScriptDescription.Multiline = true;
            this.textBoxGhostScriptDescription.Name = "textBoxGhostScriptDescription";
            this.textBoxGhostScriptDescription.ReadOnly = true;
            this.textBoxGhostScriptDescription.Size = new System.Drawing.Size(328, 49);
            this.textBoxGhostScriptDescription.TabIndex = 2;
            this.textBoxGhostScriptDescription.Text = "Ghostscript is a suite of software for converting PostScript files (.ps) to PDF f" +
                "iles (.pdf)";
            // 
            // textBoxGhostScriptPath
            // 
            this.textBoxGhostScriptPath.Location = new System.Drawing.Point(153, 89);
            this.textBoxGhostScriptPath.Name = "textBoxGhostScriptPath";
            this.textBoxGhostScriptPath.Size = new System.Drawing.Size(252, 20);
            this.textBoxGhostScriptPath.TabIndex = 1;
            this.textBoxGhostScriptPath.Click += new System.EventHandler(this.textBoxGhostScriptPath_Click);
            // 
            // labelGhostScriptPath
            // 
            this.labelGhostScriptPath.AutoSize = true;
            this.labelGhostScriptPath.Location = new System.Drawing.Point(6, 92);
            this.labelGhostScriptPath.Name = "labelGhostScriptPath";
            this.labelGhostScriptPath.Size = new System.Drawing.Size(141, 13);
            this.labelGhostScriptPath.TabIndex = 0;
            this.labelGhostScriptPath.Text = "GhostScript executable path";
            // 
            // textBoxConfigurationDescription
            // 
            this.textBoxConfigurationDescription.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBoxConfigurationDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxConfigurationDescription.Location = new System.Drawing.Point(18, 9);
            this.textBoxConfigurationDescription.Name = "textBoxConfigurationDescription";
            this.textBoxConfigurationDescription.ReadOnly = true;
            this.textBoxConfigurationDescription.Size = new System.Drawing.Size(456, 13);
            this.textBoxConfigurationDescription.TabIndex = 2;
            this.textBoxConfigurationDescription.Text = "Inorder for the program to work well, you must set some configurations.";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(409, 464);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(328, 464);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // openFileDialogGhostScript
            // 
            this.openFileDialogGhostScript.Filter = "Executable fils|*.exe";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(472, 430);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.groupBoxAbc2Ps);
            this.tabPage1.Controls.Add(this.groupBoxGhostScript);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(464, 404);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Software";
            // 
            // groupBoxAbc2Ps
            // 
            this.groupBoxAbc2Ps.Controls.Add(this.textBoxAbc2PsDescription);
            this.groupBoxAbc2Ps.Controls.Add(this.buttonAbc2PsDirectory);
            this.groupBoxAbc2Ps.Controls.Add(this.label1);
            this.groupBoxAbc2Ps.Controls.Add(this.textBoxAbc2PsPath);
            this.groupBoxAbc2Ps.Controls.Add(this.labelAbc2PsPath);
            this.groupBoxAbc2Ps.Location = new System.Drawing.Point(7, 133);
            this.groupBoxAbc2Ps.Name = "groupBoxAbc2Ps";
            this.groupBoxAbc2Ps.Size = new System.Drawing.Size(451, 107);
            this.groupBoxAbc2Ps.TabIndex = 2;
            this.groupBoxAbc2Ps.TabStop = false;
            this.groupBoxAbc2Ps.Text = "abcm2ps";
            // 
            // textBoxAbc2PsDescription
            // 
            this.textBoxAbc2PsDescription.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxAbc2PsDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxAbc2PsDescription.Location = new System.Drawing.Point(8, 19);
            this.textBoxAbc2PsDescription.Multiline = true;
            this.textBoxAbc2PsDescription.Name = "textBoxAbc2PsDescription";
            this.textBoxAbc2PsDescription.ReadOnly = true;
            this.textBoxAbc2PsDescription.Size = new System.Drawing.Size(328, 49);
            this.textBoxAbc2PsDescription.TabIndex = 2;
            this.textBoxAbc2PsDescription.Text = "Abcm2ps is a software for converting music notation \r\nABC files (.abc) to PostScr" +
                "ipt files (.ps)";
            // 
            // buttonAbc2PsDirectory
            // 
            this.buttonAbc2PsDirectory.FlatAppearance.BorderSize = 0;
            this.buttonAbc2PsDirectory.Image = global::Recorder.Properties.Resources.directoryPicture;
            this.buttonAbc2PsDirectory.Location = new System.Drawing.Point(410, 73);
            this.buttonAbc2PsDirectory.Name = "buttonAbc2PsDirectory";
            this.buttonAbc2PsDirectory.Size = new System.Drawing.Size(35, 23);
            this.buttonAbc2PsDirectory.TabIndex = 3;
            this.buttonAbc2PsDirectory.UseVisualStyleBackColor = true;
            this.buttonAbc2PsDirectory.Click += new System.EventHandler(this.buttonAbc2PsDirectory_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "GhostScript executable path";
            // 
            // textBoxAbc2PsPath
            // 
            this.textBoxAbc2PsPath.Location = new System.Drawing.Point(152, 75);
            this.textBoxAbc2PsPath.Name = "textBoxAbc2PsPath";
            this.textBoxAbc2PsPath.Size = new System.Drawing.Size(252, 20);
            this.textBoxAbc2PsPath.TabIndex = 1;
            this.textBoxAbc2PsPath.Click += new System.EventHandler(this.textBoxAbc2PsPath_Click);
            // 
            // labelAbc2PsPath
            // 
            this.labelAbc2PsPath.AutoSize = true;
            this.labelAbc2PsPath.Location = new System.Drawing.Point(6, 78);
            this.labelAbc2PsPath.Name = "labelAbc2PsPath";
            this.labelAbc2PsPath.Size = new System.Drawing.Size(129, 13);
            this.labelAbc2PsPath.TabIndex = 0;
            this.labelAbc2PsPath.Text = "abcm2ps executable path";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(464, 404);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Music";
            // 
            // ConfigurationForm
            // 
            this.AcceptButton = this.buttonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(496, 499);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxConfigurationDescription);
            this.Controls.Add(this.labelConfigurationExplenation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ConfigurationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "RecorderRecorder Configuration";
            this.groupBoxGhostScript.ResumeLayout(false);
            this.groupBoxGhostScript.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGhostScriptLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBoxAbc2Ps.ResumeLayout(false);
            this.groupBoxAbc2Ps.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelConfigurationExplenation;
        private System.Windows.Forms.GroupBox groupBoxGhostScript;
        private System.Windows.Forms.TextBox textBoxGhostScriptPath;
        private System.Windows.Forms.Label labelGhostScriptPath;
        private System.Windows.Forms.TextBox textBoxGhostScriptDescription;
        private System.Windows.Forms.TextBox textBoxConfigurationDescription;
        private System.Windows.Forms.Button buttonGhostScriptDirectory;
        private System.Windows.Forms.PictureBox pictureBoxGhostScriptLogo;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.OpenFileDialog openFileDialogGhostScript;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBoxAbc2Ps;
        private System.Windows.Forms.TextBox textBoxAbc2PsDescription;
        private System.Windows.Forms.Button buttonAbc2PsDirectory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxAbc2PsPath;
        private System.Windows.Forms.Label labelAbc2PsPath;
    }
}