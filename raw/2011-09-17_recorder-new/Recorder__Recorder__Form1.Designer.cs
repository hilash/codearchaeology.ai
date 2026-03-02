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
            this.components = new System.ComponentModel.Container();
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
            this.labelFrequencyDomain = new System.Windows.Forms.Label();
            this.tabPageNotes = new System.Windows.Forms.TabPage();
            this.panelMusicSheet = new System.Windows.Forms.Panel();
            this.textBoxMIDI = new System.Windows.Forms.TextBox();
            this.labelMIDI = new System.Windows.Forms.Label();
            this.buttonStartPlayingRecord = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.buttonPausePlayingRecord = new System.Windows.Forms.Button();
            this.buttonStopPlayingRecord = new System.Windows.Forms.Button();
            this.comboBoxPlayingRecordFormats = new System.Windows.Forms.ComboBox();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.groupBoxRecord = new System.Windows.Forms.GroupBox();
            this.groupBoxPlay = new System.Windows.Forms.GroupBox();
            this.groupBoxSave = new System.Windows.Forms.GroupBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBoxRecorder = new System.Windows.Forms.PictureBox();
            this.pictureBoxTimeDomain = new System.Windows.Forms.PictureBox();
            this.pictureBoxFrequencyDomain = new System.Windows.Forms.PictureBox();
            this.groupBoxTransform.SuspendLayout();
            this.tabPDF.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axAcroPDF1)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabFFT.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabPageNotes.SuspendLayout();
            this.groupBoxRecord.SuspendLayout();
            this.groupBoxPlay.SuspendLayout();
            this.groupBoxSave.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRecorder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTimeDomain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFrequencyDomain)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonStartRecord
            // 
            this.buttonStartRecord.Location = new System.Drawing.Point(6, 19);
            this.buttonStartRecord.Name = "buttonStartRecord";
            this.buttonStartRecord.Size = new System.Drawing.Size(75, 23);
            this.buttonStartRecord.TabIndex = 2;
            this.buttonStartRecord.Text = "start";
            this.toolTip1.SetToolTip(this.buttonStartRecord, "start recording the recorder");
            this.buttonStartRecord.UseVisualStyleBackColor = true;
            this.buttonStartRecord.Click += new System.EventHandler(this.buttonStartRecord_Click);
            // 
            // buttonStopRecord
            // 
            this.buttonStopRecord.Location = new System.Drawing.Point(87, 19);
            this.buttonStopRecord.Name = "buttonStopRecord";
            this.buttonStopRecord.Size = new System.Drawing.Size(75, 23);
            this.buttonStopRecord.TabIndex = 3;
            this.buttonStopRecord.Text = "stop";
            this.toolTip1.SetToolTip(this.buttonStopRecord, "stop the recording");
            this.buttonStopRecord.UseVisualStyleBackColor = true;
            this.buttonStopRecord.Click += new System.EventHandler(this.buttonStopRecord_Click);
            // 
            // textBoxPitch
            // 
            this.textBoxPitch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPitch.Enabled = false;
            this.textBoxPitch.Location = new System.Drawing.Point(802, 13);
            this.textBoxPitch.Name = "textBoxPitch";
            this.textBoxPitch.Size = new System.Drawing.Size(92, 20);
            this.textBoxPitch.TabIndex = 4;
            this.textBoxPitch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelPitch
            // 
            this.labelPitch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPitch.AutoSize = true;
            this.labelPitch.Location = new System.Drawing.Point(766, 16);
            this.labelPitch.Name = "labelPitch";
            this.labelPitch.Size = new System.Drawing.Size(30, 13);
            this.labelPitch.TabIndex = 5;
            this.labelPitch.Text = "pitch";
            // 
            // textBoxNote
            // 
            this.textBoxNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNote.Enabled = false;
            this.textBoxNote.Location = new System.Drawing.Point(802, 40);
            this.textBoxNote.Name = "textBoxNote";
            this.textBoxNote.Size = new System.Drawing.Size(91, 20);
            this.textBoxNote.TabIndex = 6;
            this.textBoxNote.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelNote
            // 
            this.labelNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelNote.AutoSize = true;
            this.labelNote.Location = new System.Drawing.Point(768, 43);
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
            this.comboBoxInputDevices.Location = new System.Drawing.Point(168, 21);
            this.comboBoxInputDevices.MaxDropDownItems = 10;
            this.comboBoxInputDevices.Name = "comboBoxInputDevices";
            this.comboBoxInputDevices.Size = new System.Drawing.Size(173, 21);
            this.comboBoxInputDevices.TabIndex = 8;
            this.comboBoxInputDevices.SelectedIndexChanged += new System.EventHandler(this.comboBoxInputDevices_SelectedIndexChanged);
            // 
            // groupBoxTransform
            // 
            this.groupBoxTransform.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxTransform.Controls.Add(this.radioButtonDFT);
            this.groupBoxTransform.Controls.Add(this.radioButtonFFT);
            this.groupBoxTransform.Location = new System.Drawing.Point(12, 576);
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
            this.tabPDF.Size = new System.Drawing.Size(764, 467);
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
            this.axAcroPDF1.Size = new System.Drawing.Size(758, 461);
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
            this.tabControl.Size = new System.Drawing.Size(772, 493);
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
            this.tabFFT.Size = new System.Drawing.Size(764, 467);
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
            this.splitContainer1.Size = new System.Drawing.Size(758, 461);
            this.splitContainer1.SplitterDistance = 237;
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
            // tabPageNotes
            // 
            this.tabPageNotes.AccessibleName = "tabPageNotes";
            this.tabPageNotes.BackColor = System.Drawing.Color.White;
            this.tabPageNotes.Controls.Add(this.panelMusicSheet);
            this.tabPageNotes.Location = new System.Drawing.Point(4, 22);
            this.tabPageNotes.Name = "tabPageNotes";
            this.tabPageNotes.Size = new System.Drawing.Size(764, 467);
            this.tabPageNotes.TabIndex = 2;
            this.tabPageNotes.Text = "Notes";
            this.tabPageNotes.ToolTipText = "an interactive music sheet, present the notes currently played by the recorder";
            // 
            // panelMusicSheet
            // 
            this.panelMusicSheet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMusicSheet.Location = new System.Drawing.Point(0, 0);
            this.panelMusicSheet.Name = "panelMusicSheet";
            this.panelMusicSheet.Size = new System.Drawing.Size(764, 467);
            this.panelMusicSheet.TabIndex = 0;
            this.panelMusicSheet.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMusicSheet_Paint);
            // 
            // textBoxMIDI
            // 
            this.textBoxMIDI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMIDI.Enabled = false;
            this.textBoxMIDI.Location = new System.Drawing.Point(802, 66);
            this.textBoxMIDI.Name = "textBoxMIDI";
            this.textBoxMIDI.Size = new System.Drawing.Size(93, 20);
            this.textBoxMIDI.TabIndex = 11;
            this.textBoxMIDI.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelMIDI
            // 
            this.labelMIDI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMIDI.AutoSize = true;
            this.labelMIDI.Location = new System.Drawing.Point(768, 69);
            this.labelMIDI.Name = "labelMIDI";
            this.labelMIDI.Size = new System.Drawing.Size(30, 13);
            this.labelMIDI.TabIndex = 12;
            this.labelMIDI.Text = "MIDI";
            // 
            // buttonStartPlayingRecord
            // 
            this.buttonStartPlayingRecord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonStartPlayingRecord.Enabled = false;
            this.buttonStartPlayingRecord.FlatAppearance.BorderColor = System.Drawing.Color.Yellow;
            this.buttonStartPlayingRecord.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Teal;
            this.buttonStartPlayingRecord.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Purple;
            this.buttonStartPlayingRecord.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonStartPlayingRecord.Font = new System.Drawing.Font("Webdings", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.buttonStartPlayingRecord.Location = new System.Drawing.Point(6, 19);
            this.buttonStartPlayingRecord.Name = "buttonStartPlayingRecord";
            this.buttonStartPlayingRecord.Size = new System.Drawing.Size(41, 23);
            this.buttonStartPlayingRecord.TabIndex = 13;
            this.buttonStartPlayingRecord.Text = "4";
            this.toolTip1.SetToolTip(this.buttonStartPlayingRecord, "play the last record");
            this.buttonStartPlayingRecord.UseVisualStyleBackColor = false;
            this.buttonStartPlayingRecord.Click += new System.EventHandler(this.buttonPlayRecord_Click);
            // 
            // buttonPausePlayingRecord
            // 
            this.buttonPausePlayingRecord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonPausePlayingRecord.Enabled = false;
            this.buttonPausePlayingRecord.FlatAppearance.BorderColor = System.Drawing.Color.Yellow;
            this.buttonPausePlayingRecord.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Teal;
            this.buttonPausePlayingRecord.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Purple;
            this.buttonPausePlayingRecord.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonPausePlayingRecord.Font = new System.Drawing.Font("Webdings", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.buttonPausePlayingRecord.Location = new System.Drawing.Point(53, 19);
            this.buttonPausePlayingRecord.Name = "buttonPausePlayingRecord";
            this.buttonPausePlayingRecord.Size = new System.Drawing.Size(41, 23);
            this.buttonPausePlayingRecord.TabIndex = 13;
            this.buttonPausePlayingRecord.Text = ";";
            this.buttonPausePlayingRecord.UseVisualStyleBackColor = false;
            this.buttonPausePlayingRecord.Click += new System.EventHandler(this.buttonPlayRecord_Click);
            // 
            // buttonStopPlayingRecord
            // 
            this.buttonStopPlayingRecord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonStopPlayingRecord.Enabled = false;
            this.buttonStopPlayingRecord.FlatAppearance.BorderColor = System.Drawing.Color.Yellow;
            this.buttonStopPlayingRecord.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Teal;
            this.buttonStopPlayingRecord.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Purple;
            this.buttonStopPlayingRecord.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonStopPlayingRecord.Font = new System.Drawing.Font("Webdings", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.buttonStopPlayingRecord.Location = new System.Drawing.Point(100, 19);
            this.buttonStopPlayingRecord.Name = "buttonStopPlayingRecord";
            this.buttonStopPlayingRecord.Size = new System.Drawing.Size(41, 23);
            this.buttonStopPlayingRecord.TabIndex = 13;
            this.buttonStopPlayingRecord.Text = "<";
            this.buttonStopPlayingRecord.UseVisualStyleBackColor = false;
            this.buttonStopPlayingRecord.Click += new System.EventHandler(this.buttonPlayRecord_Click);
            // 
            // comboBoxPlayingRecordFormats
            // 
            this.comboBoxPlayingRecordFormats.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPlayingRecordFormats.FormattingEnabled = true;
            this.comboBoxPlayingRecordFormats.Location = new System.Drawing.Point(147, 19);
            this.comboBoxPlayingRecordFormats.Name = "comboBoxPlayingRecordFormats";
            this.comboBoxPlayingRecordFormats.Size = new System.Drawing.Size(141, 21);
            this.comboBoxPlayingRecordFormats.TabIndex = 14;
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem4});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2,
            this.menuItem3});
            this.menuItem1.Text = "File";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.Text = "Open";
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 1;
            this.menuItem3.Text = "Save";
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 1;
            this.menuItem4.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem5});
            this.menuItem4.Text = "Help";
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 0;
            this.menuItem5.Text = "About";
            // 
            // groupBoxRecord
            // 
            this.groupBoxRecord.Controls.Add(this.buttonStartRecord);
            this.groupBoxRecord.Controls.Add(this.buttonStopRecord);
            this.groupBoxRecord.Controls.Add(this.comboBoxInputDevices);
            this.groupBoxRecord.Location = new System.Drawing.Point(12, 6);
            this.groupBoxRecord.Name = "groupBoxRecord";
            this.groupBoxRecord.Size = new System.Drawing.Size(347, 47);
            this.groupBoxRecord.TabIndex = 15;
            this.groupBoxRecord.TabStop = false;
            this.groupBoxRecord.Text = "Record";
            // 
            // groupBoxPlay
            // 
            this.groupBoxPlay.Controls.Add(this.buttonStartPlayingRecord);
            this.groupBoxPlay.Controls.Add(this.comboBoxPlayingRecordFormats);
            this.groupBoxPlay.Controls.Add(this.buttonPausePlayingRecord);
            this.groupBoxPlay.Controls.Add(this.buttonStopPlayingRecord);
            this.groupBoxPlay.Location = new System.Drawing.Point(365, 6);
            this.groupBoxPlay.Name = "groupBoxPlay";
            this.groupBoxPlay.Size = new System.Drawing.Size(294, 47);
            this.groupBoxPlay.TabIndex = 16;
            this.groupBoxPlay.TabStop = false;
            this.groupBoxPlay.Text = "Play";
            // 
            // groupBoxSave
            // 
            this.groupBoxSave.Controls.Add(this.button1);
            this.groupBoxSave.Location = new System.Drawing.Point(665, 6);
            this.groupBoxSave.Name = "groupBoxSave";
            this.groupBoxSave.Size = new System.Drawing.Size(46, 47);
            this.groupBoxSave.TabIndex = 2;
            this.groupBoxSave.TabStop = false;
            this.groupBoxSave.Text = "Save";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "WAV";
            this.saveFileDialog1.Title = "Saving Last Record";
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.Transparent;
            this.button1.Image = global::Recorder.Properties.Resources.Icon_Save_Small;
            this.button1.Location = new System.Drawing.Point(6, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(34, 34);
            this.button1.TabIndex = 17;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBoxRecorder
            // 
            this.pictureBoxRecorder.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pictureBoxRecorder.BackgroundImage = global::Recorder.Properties.Resources.Recorder_picture;
            this.pictureBoxRecorder.Location = new System.Drawing.Point(802, 81);
            this.pictureBoxRecorder.Name = "pictureBoxRecorder";
            this.pictureBoxRecorder.Size = new System.Drawing.Size(93, 467);
            this.pictureBoxRecorder.TabIndex = 9;
            this.pictureBoxRecorder.TabStop = false;
            // 
            // pictureBoxTimeDomain
            // 
            this.pictureBoxTimeDomain.AccessibleDescription = "pictureBoxTimeDomain";
            this.pictureBoxTimeDomain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxTimeDomain.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxTimeDomain.Name = "pictureBoxTimeDomain";
            this.pictureBoxTimeDomain.Size = new System.Drawing.Size(758, 237);
            this.pictureBoxTimeDomain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxTimeDomain.TabIndex = 0;
            this.pictureBoxTimeDomain.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBoxTimeDomain, "displays the sound wave, on real-time");
            this.pictureBoxTimeDomain.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxTimeDomain_Paint);
            // 
            // pictureBoxFrequencyDomain
            // 
            this.pictureBoxFrequencyDomain.AccessibleDescription = "pictureBoxFrequencyDomain";
            this.pictureBoxFrequencyDomain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxFrequencyDomain.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxFrequencyDomain.Name = "pictureBoxFrequencyDomain";
            this.pictureBoxFrequencyDomain.Size = new System.Drawing.Size(758, 223);
            this.pictureBoxFrequencyDomain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxFrequencyDomain.TabIndex = 0;
            this.pictureBoxFrequencyDomain.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBoxFrequencyDomain, "the sound wave frequency analysis");
            this.pictureBoxFrequencyDomain.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxFrequencyDomain_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 662);
            this.Controls.Add(this.groupBoxSave);
            this.Controls.Add(this.groupBoxPlay);
            this.Controls.Add(this.labelMIDI);
            this.Controls.Add(this.groupBoxRecord);
            this.Controls.Add(this.textBoxMIDI);
            this.Controls.Add(this.groupBoxTransform);
            this.Controls.Add(this.pictureBoxRecorder);
            this.Controls.Add(this.labelNote);
            this.Controls.Add(this.labelPitch);
            this.Controls.Add(this.textBoxNote);
            this.Controls.Add(this.textBoxPitch);
            this.Controls.Add(this.tabControl);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
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
            this.tabPageNotes.ResumeLayout(false);
            this.groupBoxRecord.ResumeLayout(false);
            this.groupBoxPlay.ResumeLayout(false);
            this.groupBoxSave.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRecorder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTimeDomain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFrequencyDomain)).EndInit();
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
        private System.Windows.Forms.Button buttonStartPlayingRecord;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button buttonPausePlayingRecord;
        private System.Windows.Forms.Button buttonStopPlayingRecord;
        private System.Windows.Forms.ComboBox comboBoxPlayingRecordFormats;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.GroupBox groupBoxRecord;
        private System.Windows.Forms.GroupBox groupBoxPlay;
        private System.Windows.Forms.GroupBox groupBoxSave;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button button1;
    }
}

