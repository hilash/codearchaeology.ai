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
            this.start_record = new System.Windows.Forms.Button();
            this.stop_record = new System.Windows.Forms.Button();
            this.pitch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chord = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.InputDevice = new System.Windows.Forms.ComboBox();
            this.pictureRecorder = new System.Windows.Forms.PictureBox();
            this.transformGroup = new System.Windows.Forms.GroupBox();
            this.useDFT = new System.Windows.Forms.RadioButton();
            this.useFFT = new System.Windows.Forms.RadioButton();
            this.pitchTrackBar = new System.Windows.Forms.TrackBar();
            this.pitchDeltaGroup = new System.Windows.Forms.GroupBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.axAcroPDF1 = new AxAcroPDFLib.AxAcroPDF();
            this.timeDomain = new System.Windows.Forms.PictureBox();
            this.frequencyDomain = new System.Windows.Forms.PictureBox();
            this.tabPDF = new System.Windows.Forms.TabControl();
            this.tabTransform = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.labelTimeDomain = new System.Windows.Forms.Label();
            this.labelFrequencyDomain = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureRecorder)).BeginInit();
            this.transformGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pitchTrackBar)).BeginInit();
            this.pitchDeltaGroup.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axAcroPDF1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeDomain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.frequencyDomain)).BeginInit();
            this.tabPDF.SuspendLayout();
            this.tabTransform.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // start_record
            // 
            this.start_record.Location = new System.Drawing.Point(16, 13);
            this.start_record.Name = "start_record";
            this.start_record.Size = new System.Drawing.Size(75, 23);
            this.start_record.TabIndex = 2;
            this.start_record.Text = "start record";
            this.start_record.UseVisualStyleBackColor = true;
            this.start_record.Click += new System.EventHandler(this.start_record_Click);
            // 
            // stop_record
            // 
            this.stop_record.Location = new System.Drawing.Point(98, 12);
            this.stop_record.Name = "stop_record";
            this.stop_record.Size = new System.Drawing.Size(75, 23);
            this.stop_record.TabIndex = 3;
            this.stop_record.Text = "stop record";
            this.stop_record.UseVisualStyleBackColor = true;
            this.stop_record.Click += new System.EventHandler(this.stop_record_Click);
            // 
            // pitch
            // 
            this.pitch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pitch.Enabled = false;
            this.pitch.Location = new System.Drawing.Point(764, 13);
            this.pitch.Name = "pitch";
            this.pitch.Size = new System.Drawing.Size(92, 20);
            this.pitch.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(723, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "pitch";
            // 
            // chord
            // 
            this.chord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chord.Enabled = false;
            this.chord.Location = new System.Drawing.Point(764, 40);
            this.chord.Name = "chord";
            this.chord.Size = new System.Drawing.Size(91, 20);
            this.chord.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(723, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "chord";
            // 
            // InputDevice
            // 
            this.InputDevice.AccessibleName = "";
            this.InputDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.InputDevice.FormattingEnabled = true;
            this.InputDevice.Location = new System.Drawing.Point(180, 13);
            this.InputDevice.MaxDropDownItems = 10;
            this.InputDevice.Name = "InputDevice";
            this.InputDevice.Size = new System.Drawing.Size(166, 21);
            this.InputDevice.TabIndex = 8;
            this.InputDevice.SelectedIndexChanged += new System.EventHandler(this.InputDevice_SelectedIndexChanged);
            // 
            // pictureRecorder
            // 
            this.pictureRecorder.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pictureRecorder.BackgroundImage = global::Recorder.Properties.Resources.Recorder_picture;
            this.pictureRecorder.Location = new System.Drawing.Point(764, 68);
            this.pictureRecorder.Name = "pictureRecorder";
            this.pictureRecorder.Size = new System.Drawing.Size(93, 467);
            this.pictureRecorder.TabIndex = 9;
            this.pictureRecorder.TabStop = false;
            // 
            // transformGroup
            // 
            this.transformGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.transformGroup.Controls.Add(this.useDFT);
            this.transformGroup.Controls.Add(this.useFFT);
            this.transformGroup.Location = new System.Drawing.Point(12, 550);
            this.transformGroup.Name = "transformGroup";
            this.transformGroup.Size = new System.Drawing.Size(114, 64);
            this.transformGroup.TabIndex = 10;
            this.transformGroup.TabStop = false;
            this.transformGroup.Text = "transform";
            // 
            // useDFT
            // 
            this.useDFT.AutoSize = true;
            this.useDFT.Location = new System.Drawing.Point(57, 30);
            this.useDFT.Name = "useDFT";
            this.useDFT.Size = new System.Drawing.Size(46, 17);
            this.useDFT.TabIndex = 1;
            this.useDFT.TabStop = true;
            this.useDFT.Text = "DFT";
            this.useDFT.UseVisualStyleBackColor = true;
            this.useDFT.CheckedChanged += new System.EventHandler(this.useDFT_CheckedChanged);
            // 
            // useFFT
            // 
            this.useFFT.AutoSize = true;
            this.useFFT.Checked = true;
            this.useFFT.Location = new System.Drawing.Point(7, 30);
            this.useFFT.Name = "useFFT";
            this.useFFT.Size = new System.Drawing.Size(44, 17);
            this.useFFT.TabIndex = 0;
            this.useFFT.TabStop = true;
            this.useFFT.Text = "FFT";
            this.useFFT.UseVisualStyleBackColor = true;
            this.useFFT.CheckedChanged += new System.EventHandler(this.useFFT_CheckedChanged);
            // 
            // pitchTrackBar
            // 
            this.pitchTrackBar.Location = new System.Drawing.Point(6, 17);
            this.pitchTrackBar.Maximum = 50;
            this.pitchTrackBar.Minimum = -50;
            this.pitchTrackBar.Name = "pitchTrackBar";
            this.pitchTrackBar.Size = new System.Drawing.Size(268, 45);
            this.pitchTrackBar.TabIndex = 11;
            this.pitchTrackBar.TickFrequency = 0;
            this.pitchTrackBar.Scroll += new System.EventHandler(this.pitchTrackBar_Scroll);
            // 
            // pitchDeltaGroup
            // 
            this.pitchDeltaGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pitchDeltaGroup.Controls.Add(this.pitchTrackBar);
            this.pitchDeltaGroup.Location = new System.Drawing.Point(146, 550);
            this.pitchDeltaGroup.Name = "pitchDeltaGroup";
            this.pitchDeltaGroup.Size = new System.Drawing.Size(278, 64);
            this.pitchDeltaGroup.TabIndex = 12;
            this.pitchDeltaGroup.TabStop = false;
            this.pitchDeltaGroup.Text = "adjust pitch";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.axAcroPDF1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(726, 441);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Notes";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // axAcroPDF1
            // 
            this.axAcroPDF1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axAcroPDF1.Enabled = true;
            this.axAcroPDF1.Location = new System.Drawing.Point(3, 3);
            this.axAcroPDF1.Name = "axAcroPDF1";
            this.axAcroPDF1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axAcroPDF1.OcxState")));
            this.axAcroPDF1.Size = new System.Drawing.Size(720, 435);
            this.axAcroPDF1.TabIndex = 0;
            // 
            // timeDomain
            // 
            this.timeDomain.AccessibleDescription = "Time Domain";
            this.timeDomain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeDomain.Location = new System.Drawing.Point(0, 0);
            this.timeDomain.Name = "timeDomain";
            this.timeDomain.Size = new System.Drawing.Size(720, 227);
            this.timeDomain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.timeDomain.TabIndex = 0;
            this.timeDomain.TabStop = false;
            // 
            // frequencyDomain
            // 
            this.frequencyDomain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frequencyDomain.Location = new System.Drawing.Point(0, 0);
            this.frequencyDomain.Name = "frequencyDomain";
            this.frequencyDomain.Size = new System.Drawing.Size(720, 207);
            this.frequencyDomain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.frequencyDomain.TabIndex = 0;
            this.frequencyDomain.TabStop = false;
            // 
            // tabPDF
            // 
            this.tabPDF.AccessibleName = "tabPDF";
            this.tabPDF.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabPDF.Controls.Add(this.tabTransform);
            this.tabPDF.Controls.Add(this.tabPage2);
            this.tabPDF.Location = new System.Drawing.Point(12, 68);
            this.tabPDF.Name = "tabPDF";
            this.tabPDF.SelectedIndex = 0;
            this.tabPDF.Size = new System.Drawing.Size(734, 467);
            this.tabPDF.TabIndex = 0;
            this.tabPDF.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabTransform
            // 
            this.tabTransform.AccessibleName = "tabTransform";
            this.tabTransform.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(40)))), ((int)(((byte)(34)))));
            this.tabTransform.Controls.Add(this.splitContainer1);
            this.tabTransform.Location = new System.Drawing.Point(4, 22);
            this.tabTransform.Name = "tabTransform";
            this.tabTransform.Padding = new System.Windows.Forms.Padding(3);
            this.tabTransform.Size = new System.Drawing.Size(726, 441);
            this.tabTransform.TabIndex = 0;
            this.tabTransform.Text = "FFT";
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
            this.splitContainer1.Panel1.Controls.Add(this.timeDomain);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.labelFrequencyDomain);
            this.splitContainer1.Panel2.Controls.Add(this.frequencyDomain);
            this.splitContainer1.Size = new System.Drawing.Size(720, 435);
            this.splitContainer1.SplitterDistance = 227;
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 636);
            this.Controls.Add(this.pitchDeltaGroup);
            this.Controls.Add(this.transformGroup);
            this.Controls.Add(this.pictureRecorder);
            this.Controls.Add(this.InputDevice);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chord);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pitch);
            this.Controls.Add(this.stop_record);
            this.Controls.Add(this.start_record);
            this.Controls.Add(this.tabPDF);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recorder By Hila Shmuel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureRecorder)).EndInit();
            this.transformGroup.ResumeLayout(false);
            this.transformGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pitchTrackBar)).EndInit();
            this.pitchDeltaGroup.ResumeLayout(false);
            this.pitchDeltaGroup.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axAcroPDF1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeDomain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.frequencyDomain)).EndInit();
            this.tabPDF.ResumeLayout(false);
            this.tabTransform.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button start_record;
        private System.Windows.Forms.Button stop_record;
        private System.Windows.Forms.TextBox pitch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox chord;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox InputDevice;
        private System.Windows.Forms.PictureBox pictureRecorder;
        private System.Windows.Forms.GroupBox transformGroup;
        private System.Windows.Forms.RadioButton useDFT;
        private System.Windows.Forms.RadioButton useFFT;
        private System.Windows.Forms.TrackBar pitchTrackBar;
        private System.Windows.Forms.GroupBox pitchDeltaGroup;
        private System.Windows.Forms.TabPage tabPage2;
        private AxAcroPDFLib.AxAcroPDF axAcroPDF1;
        private System.Windows.Forms.PictureBox timeDomain;
        private System.Windows.Forms.PictureBox frequencyDomain;
        private System.Windows.Forms.TabControl tabPDF;
        private System.Windows.Forms.TabPage tabTransform;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label labelTimeDomain;
        private System.Windows.Forms.Label labelFrequencyDomain;
    }
}

