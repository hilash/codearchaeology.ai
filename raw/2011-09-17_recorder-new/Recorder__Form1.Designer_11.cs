namespace Recorder
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.buttonStartRecord = new System.Windows.Forms.Button();
            this.buttonStopRecord = new System.Windows.Forms.Button();
            this.textBoxPitch = new System.Windows.Forms.TextBox();
            this.labelPitch = new System.Windows.Forms.Label();
            this.textBoxNote = new System.Windows.Forms.TextBox();
            this.labelNote = new System.Windows.Forms.Label();
            this.comboBoxInputDevices = new System.Windows.Forms.ComboBox();
            this.groupBoxTransform = new System.Windows.Forms.GroupBox();
            this.radioButtonDFT = new System.Windows.Forms.RadioButton();
            this.radioButtonFFT = new System.Windows.Forms.RadioButton();
            this.tabPDF = new System.Windows.Forms.TabPage();
            this.axAcroPDF1 = new AxAcroPDFLib.AxAcroPDF();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabFFT = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.labelTimeDomain = new System.Windows.Forms.Label();
            this.pictureBoxTimeDomain = new System.Windows.Forms.PictureBox();
            this.labelFrequencyDomain = new System.Windows.Forms.Label();
            this.pictureBoxFrequencyDomain = new System.Windows.Forms.PictureBox();
            this.tabPageNotes = new System.Windows.Forms.TabPage();
            this.panelMusicSheet = new System.Windows.Forms.Panel();
            this.pictureBoxRecorder = new System.Windows.Forms.PictureBox();
            this.textBoxMIDI = new System.Windows.Forms.TextBox();
            this.labelMIDI = new System.Windows.Forms.Label();
            this.groupBoxTransform.SuspendLayout();
            this.tabPDF.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axAcroPDF1)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabFFT.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTimeDomain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFrequencyDomain)).BeginInit();
            this.tabPageNotes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRecorder)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonStartRecord
            // 
            this.buttonStartRecord.Location = new System.Drawing.Point(16, 13);
            this.buttonStartRecord.Name = "buttonStartRecord";
            this.buttonStartRecord.Size = new System.Drawing.Size(75, 23);
            this.buttonStartRecord.TabIndex = 2;
            this.buttonStartRecord.Text = "start";
            this.buttonStartRecord.UseVisualStyleBackColor = true;
            this.buttonStartRecord.Click += new System.EventHandler(this.buttonStartRecord_Click);
            // 
            // buttonStopRecord
            // 
            this.buttonStopRecord.Location = new System.Drawing.Point(98, 12);
            this.buttonStopRecord.Name = "buttonStopRecord";
            this.buttonStopRecord.Size = new System.Drawing.Size(75, 23);
            this.buttonStopRecord.TabIndex = 3;
            this.buttonStopRecord.Text = "stop";
            this.buttonStopRecord.UseVisualStyleBackColor = true;
            this.buttonStopRecord.Click += new System.EventHandler(this.buttonStopRecord_Click);
            // 
            // textBoxPitch
            // 
            this.textBoxPitch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPitch.Enabled = false;
            this.textBoxPitch.Location = new System.Drawing.Point(764, 13);
            this.textBoxPitch.Name = "textBoxPitch";
            this.textBoxPitch.Size = new System.Drawing.Size(92, 20);
            this.textBoxPitch.TabIndex = 4;
            this.textBoxPitch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelPitch
            // 
            this.labelPitch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPitch.AutoSize = true;
            this.labelPitch.Location = new System.Drawing.Point(723, 12);
            this.labelPitch.Name = "labelPitch";
            this.labelPitch.Size = new System.Drawing.Size(30, 13);
            this.labelPitch.TabIndex = 5;
            this.labelPitch.Text = "pitch";
            // 
            // textBoxNote
            // 
            this.textBoxNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNote.Enabled = false;
            this.textBoxNote.Location = new System.Drawing.Point(764, 40);
            this.textBoxNote.Name = "textBoxNote";
            this.textBoxNote.Size = new System.Drawing.Size(91, 20);
            this.textBoxNote.TabIndex = 6;
            this.textBoxNote.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelNote
            // 
            this.labelNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelNote.AutoSize = true;
            this.labelNote.Location = new System.Drawing.Point(723, 40);
            this.labelNote.Name = "labelNote";
            this.labelNote.Size = new System.Drawing.Size(28, 13);
            this.labelNote.TabIndex = 7;
            this.labelNote.Text = "note";
            // 
            // comboBoxInputDevices
            // 
            this.comboBoxInputDevices.AccessibleName = "";
            this.comboBoxInputDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxInputDevices.FormattingEnabled = true;
            this.comboBoxInputDevices.Location = new System.Drawing.Point(180, 13);
            this.comboBoxInputDevices.MaxDropDownItems = 10;
            this.comboBoxInputDevices.Name = "comboBoxInputDevices";
            this.comboBoxInputDevices.Size = new System.Drawing.Size(166, 21);
            this.comboBoxInputDevices.TabIndex = 8;
            this.comboBoxInputDevices.SelectedIndexChanged += new System.EventHandler(this.comboBoxInputDevices_SelectedIndexChanged);
            // 
            // groupBoxTransform
            // 
            this.groupBoxTransform.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxTransform.Controls.Add(this.radioButtonDFT);
            this.groupBoxTransform.Controls.Add(this.radioButtonFFT);
            this.groupBoxTransform.Location = new System.Drawing.Point(12, 593);
            this.groupBoxTransform.Name = "groupBoxTransform";
            this.groupBoxTransform.Size = new System.Drawing.Size(114, 64);
            this.groupBoxTransform.TabIndex = 10;
            this.groupBoxTransform.TabStop = false;
            this.groupBoxTransform.Text = "transform";
            // 
            // radioButtonDFT
            // 
            this.radioButtonDFT.AutoSize = true;
            this.radioButtonDFT.Location = new System.Drawing.Point(57, 30);
            this.radioButtonDFT.Name = "radioButtonDFT";
            this.radioButtonDFT.Size = new System.Drawing.Size(46, 17);
            this.radioButtonDFT.TabIndex = 1;
            this.radioButtonDFT.TabStop = true;
            this.radioButtonDFT.Text = "DFT";
            this.radioButtonDFT.UseVisualStyleBackColor = true;
            this.radioButtonDFT.CheckedChanged += new System.EventHandler(this.radioButtonDFT_CheckedChanged);
            // 
            // radioButtonFFT
            // 
            this.radioButtonFFT.AutoSize = true;
            this.radioButtonFFT.Checked = true;
            this.radioButtonFFT.Location = new System.Drawing.Point(7, 30);
            this.radioButtonFFT.Name = "radioButtonFFT";
            this.radioButtonFFT.Size = new System.Drawing.Size(44, 17);
            this.radioButtonFFT.TabIndex = 0;
            this.radioButtonFFT.TabStop = true;
            this.radioButtonFFT.Text = "FFT";
            this.radioButtonFFT.UseVisualStyleBackColor = true;
            this.radioButtonFFT.CheckedChanged += new System.EventHandler(this.radioButtonFFT_CheckedChanged);
            // 
            // tabPDF
            // 
            this.tabPDF.Controls.Add(this.axAcroPDF1);
            this.tabPDF.Location = new System.Drawing.Point(4, 22);
            this.tabPDF.Name = "tabPDF";
            this.tabPDF.Padding = new System.Windows.Forms.Padding(3);
            this.tabPDF.Size = new System.Drawing.Size(726, 484);
            this.tabPDF.TabIndex = 1;
            this.tabPDF.Text = "PDF";
            this.tabPDF.UseVisualStyleBackColor = true;
            // 
            // axAcroPDF1
            // 
            this.axAcroPDF1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axAcroPDF1.Enabled = true;
            this.axAcroPDF1.Location = new System.Drawing.Point(3, 3);
            this.axAcroPDF1.Name = "axAcroPDF1";
            this.axAcroPDF1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axAcroPDF1.OcxState")));
            this.axAcroPDF1.Size = new System.Drawing.Size(720, 461);
            this.axAcroPDF1.TabIndex = 0;
            // 
            // tabControl
            // 
            this.tabControl.AccessibleName = "tabControl";
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabFFT);
            this.tabControl.Controls.Add(this.tabPDF);
            this.tabControl.Controls.Add(this.tabPageNotes);
            this.tabControl.Location = new System.Drawing.Point(12, 68);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(734, 510);
            this.tabControl.TabIndex = 0;
            this.tabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl_Selected);
            // 
            // tabFFT
            // 
            this.tabFFT.AccessibleName = "tabFFT";
            this.tabFFT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(40)))), ((int)(((byte)(34)))));
            this.tabFFT.Controls.Add(this.splitContainer1);
            this.tabFFT.Location = new System.Drawing.Point(4, 22);
            this.tabFFT.Name = "tabFFT";
            this.tabFFT.Padding = new System.Windows.Forms.Padding(3);
            this.tabFFT.Size = new System.Drawing.Size(726, 484);
            this.tabFFT.TabIndex = 0;
            this.tabFFT.Text = "FFT";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.labelTimeDomain);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBoxTimeDomain);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.labelFrequencyDomain);
            this.splitContainer1.Panel2.Controls.Add(this.pictureBoxFrequencyDomain);
            this.splitContainer1.Size = new System.Drawing.Size(720, 478);
            this.splitContainer1.SplitterDistance = 246;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            // 
            // labelTimeDomain
            // 
            this.labelTimeDomain.AutoSize = true;
            this.labelTimeDomain.BackColor = System.Drawing.Color.Transparent;
            this.labelTimeDomain.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTimeDomain.ForeColor = System.Drawing.Color.LemonChiffon;
            this.labelTimeDomain.Location = new System.Drawing.Point(0, 0);
            this.labelTimeDomain.Name = "labelTimeDomain";
            this.labelTimeDomain.Size = new System.Drawing.Size(84, 14);
            this.labelTimeDomain.TabIndex = 1;
            this.labelTimeDomain.Text = "Time Domain";
            // 
            // pictureBoxTimeDomain
            // 
            this.pictureBoxTimeDomain.AccessibleDescription = "pictureBoxTimeDomain";
            this.pictureBoxTimeDomain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxTimeDomain.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxTimeDomain.Name = "pictureBoxTimeDomain";
            this.pictureBoxTimeDomain.Size = new System.Drawing.Size(720, 246);
            this.pictureBoxTimeDomain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxTimeDomain.TabIndex = 0;
            this.pictureBoxTimeDomain.TabStop = false;
            this.pictureBoxTimeDomain.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxTimeDomain_Paint);
            // 
            // labelFrequencyDomain
            // 
            this.labelFrequencyDomain.AutoSize = true;
            this.labelFrequencyDomain.BackColor = System.Drawing.Color.Transparent;
            this.labelFrequencyDomain.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFrequencyDomain.ForeColor = System.Drawing.Color.LemonChiffon;
            this.labelFrequencyDomain.Location = new System.Drawing.Point(-3, 2);
            this.labelFrequencyDomain.Name = "labelFrequencyDomain";
            this.labelFrequencyDomain.Size = new System.Drawing.Size(119, 14);
            this.labelFrequencyDomain.TabIndex = 1;
            this.labelFrequencyDomain.Text = "Frequency Domain";
            // 
            // pictureBoxFrequencyDomain
            // 
            this.pictureBoxFrequencyDomain.AccessibleDescription = "pictureBoxFrequencyDomain";
            this.pictureBoxFrequencyDomain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxFrequencyDomain.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxFrequencyDomain.Name = "pictureBoxFrequencyDomain";
            this.pictureBoxFrequencyDomain.Size = new System.Drawing.Size(720, 231);
            this.pictureBoxFrequencyDomain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxFrequencyDomain.TabIndex = 0;
            this.pictureBoxFrequencyDomain.TabStop = false;
            this.pictureBoxFrequencyDomain.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxFrequencyDomain_Paint);
            // 
            // tabPageNotes
            // 
            this.tabPageNotes.AccessibleName = "tabPageNotes";
            this.tabPageNotes.BackColor = System.Drawing.Color.White;
            this.tabPageNotes.Controls.Add(this.panelMusicSheet);
            this.tabPageNotes.Location = new System.Drawing.Point(4, 22);
            this.tabPageNotes.Name = "tabPageNotes";
            this.tabPageNotes.Size = new System.Drawing.Size(726, 484);
            this.tabPageNotes.TabIndex = 2;
            this.tabPageNotes.Text = "Notes";
            this.tabPageNotes.ToolTipText = "an interactive music sheet, present the notes currently played by the recorder";
            // 
            // panelMusicSheet
            // 
            this.panelMusicSheet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMusicSheet.Location = new System.Drawing.Point(0, 0);
            this.panelMusicSheet.Name = "panelMusicSheet";
            this.panelMusicSheet.Size = new System.Drawing.Size(726, 467);
            this.panelMusicSheet.TabIndex = 0;
            this.panelMusicSheet.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMusicSheet_Paint);
            // 
            // pictureBoxRecorder
            // 
            this.pictureBoxRecorder.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pictureBoxRecorder.BackgroundImage = global::Recorder.Properties.Resources.Recorder_picture;
            this.pictureBoxRecorder.Location = new System.Drawing.Point(764, 89);
            this.pictureBoxRecorder.Name = "pictureBoxRecorder";
            this.pictureBoxRecorder.Size = new System.Drawing.Size(93, 467);
            this.pictureBoxRecorder.TabIndex = 9;
            this.pictureBoxRecorder.TabStop = false;
            // 
            // textBoxMIDI
            // 
            this.textBoxMIDI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMIDI.Enabled = false;
            this.textBoxMIDI.Location = new System.Drawing.Point(764, 66);
            this.textBoxMIDI.Name = "textBoxMIDI";
            this.textBoxMIDI.Size = new System.Drawing.Size(93, 20);
            this.textBoxMIDI.TabIndex = 11;
            this.textBoxMIDI.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelMIDI
            // 
            this.labelMIDI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMIDI.AutoSize = true;
            this.labelMIDI.Location = new System.Drawing.Point(723, 68);
            this.labelMIDI.Name = "labelMIDI";
            this.labelMIDI.Size = new System.Drawing.Size(30, 13);
            this.labelMIDI.TabIndex = 12;
            this.labelMIDI.Text = "MIDI";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 679);
            this.Controls.Add(this.labelMIDI);
            this.Controls.Add(this.textBoxMIDI);
            this.Controls.Add(this.groupBoxTransform);
            this.Controls.Add(this.pictureBoxRecorder);
            this.Controls.Add(this.comboBoxInputDevices);
            this.Controls.Add(this.labelNote);
            this.Controls.Add(this.labelPitch);
            this.Controls.Add(this.textBoxNote);
            this.Controls.Add(this.textBoxPitch);
            this.Controls.Add(this.buttonStopRecord);
            this.Controls.Add(this.buttonStartRecord);
            this.Controls.Add(this.tabControl);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(600, 700);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recorder By Hila Shmuel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBoxTransform.ResumeLayout(false);
            this.groupBoxTransform.PerformLayout();
            this.tabPDF.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axAcroPDF1)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabFFT.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTimeDomain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFrequencyDomain)).EndInit();
            this.tabPageNotes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRecorder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStartRecord;
        private System.Windows.Forms.Button buttonStopRecord;
        private System.Windows.Forms.TextBox textBoxPitch;
        private System.Windows.Forms.Label labelPitch;
        private System.Windows.Forms.TextBox textBoxNote;
        private System.Windows.Forms.Label labelNote;
        private System.Windows.Forms.ComboBox comboBoxInputDevices;
        private System.Windows.Forms.PictureBox pictureBoxRecorder;
        private System.Windows.Forms.GroupBox groupBoxTransform;
        private System.Windows.Forms.RadioButton radioButtonDFT;
        private System.Windows.Forms.RadioButton radioButtonFFT;
        private System.Windows.Forms.TabPage tabPDF;
        private AxAcroPDFLib.AxAcroPDF axAcroPDF1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabFFT;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label labelTimeDomain;
        private System.Windows.Forms.Label labelFrequencyDomain;
        private System.Windows.Forms.TabPage tabPageNotes;
        private System.Windows.Forms.Panel panelMusicSheet;
        private System.Windows.Forms.PictureBox pictureBoxTimeDomain;
        private System.Windows.Forms.PictureBox pictureBoxFrequencyDomain;
        private System.Windows.Forms.TextBox textBoxMIDI;
        private System.Windows.Forms.Label labelMIDI;
    }
}

