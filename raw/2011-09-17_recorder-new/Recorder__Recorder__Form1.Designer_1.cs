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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label3 = new System.Windows.Forms.Label();
            this.timeDomain = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.frequencyDomain = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.axAcroPDF1 = new AxAcroPDFLib.AxAcroPDF();
            this.start_record = new System.Windows.Forms.Button();
            this.stop_record = new System.Windows.Forms.Button();
            this.pitch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chord = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.InputDevice = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.transformGroup = new System.Windows.Forms.GroupBox();
            this.useDFT = new System.Windows.Forms.RadioButton();
            this.useFFT = new System.Windows.Forms.RadioButton();
            this.pitchTrackBar = new System.Windows.Forms.TrackBar();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timeDomain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.frequencyDomain)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axAcroPDF1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.transformGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pitchTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 68);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(734, 467);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(40)))), ((int)(((byte)(34)))));
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(726, 441);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "FFT";
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
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.timeDomain);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.frequencyDomain);
            this.splitContainer1.Size = new System.Drawing.Size(720, 435);
            this.splitContainer1.SplitterDistance = 224;
            this.splitContainer1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label3.ForeColor = System.Drawing.Color.Aquamarine;
            this.label3.Location = new System.Drawing.Point(561, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 24);
            this.label3.TabIndex = 1;
            this.label3.Text = "Time Domain";
            // 
            // timeDomain
            // 
            this.timeDomain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(40)))), ((int)(((byte)(34)))));
            this.timeDomain.Location = new System.Drawing.Point(16, 12);
            this.timeDomain.Name = "timeDomain";
            this.timeDomain.Size = new System.Drawing.Size(512, 200);
            this.timeDomain.TabIndex = 0;
            this.timeDomain.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.ForeColor = System.Drawing.SystemColors.MenuBar;
            this.label4.Location = new System.Drawing.Point(534, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(172, 24);
            this.label4.TabIndex = 1;
            this.label4.Text = "Frequency Domain";
            // 
            // frequencyDomain
            // 
            this.frequencyDomain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(40)))), ((int)(((byte)(34)))));
            this.frequencyDomain.Location = new System.Drawing.Point(16, 7);
            this.frequencyDomain.Name = "frequencyDomain";
            this.frequencyDomain.Size = new System.Drawing.Size(512, 200);
            this.frequencyDomain.TabIndex = 0;
            this.frequencyDomain.TabStop = false;
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
            this.axAcroPDF1.Enabled = true;
            this.axAcroPDF1.Location = new System.Drawing.Point(0, 3);
            this.axAcroPDF1.Name = "axAcroPDF1";
            this.axAcroPDF1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axAcroPDF1.OcxState")));
            this.axAcroPDF1.Size = new System.Drawing.Size(720, 432);
            this.axAcroPDF1.TabIndex = 0;
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
            this.pitch.Enabled = false;
            this.pitch.Location = new System.Drawing.Point(764, 13);
            this.pitch.Name = "pitch";
            this.pitch.Size = new System.Drawing.Size(92, 20);
            this.pitch.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(723, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "pitch";
            // 
            // chord
            // 
            this.chord.Enabled = false;
            this.chord.Location = new System.Drawing.Point(764, 40);
            this.chord.Name = "chord";
            this.chord.Size = new System.Drawing.Size(91, 20);
            this.chord.TabIndex = 6;
            // 
            // label2
            // 
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
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Recorder.Properties.Resources.Recorder_picture;
            this.pictureBox1.Location = new System.Drawing.Point(764, 68);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(93, 467);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // transformGroup
            // 
            this.transformGroup.Controls.Add(this.useDFT);
            this.transformGroup.Controls.Add(this.useFFT);
            this.transformGroup.Location = new System.Drawing.Point(366, 12);
            this.transformGroup.Name = "transformGroup";
            this.transformGroup.Size = new System.Drawing.Size(114, 50);
            this.transformGroup.TabIndex = 10;
            this.transformGroup.TabStop = false;
            this.transformGroup.Text = "transform";
            // 
            // useDFT
            // 
            this.useDFT.AutoSize = true;
            this.useDFT.Location = new System.Drawing.Point(58, 20);
            this.useDFT.Name = "useDFT";
            this.useDFT.Size = new System.Drawing.Size(46, 17);
            this.useDFT.TabIndex = 1;
            this.useDFT.TabStop = true;
            this.useDFT.Text = "DFT";
            this.useDFT.UseVisualStyleBackColor = true;
            // 
            // useFFT
            // 
            this.useFFT.AutoSize = true;
            this.useFFT.Checked = true;
            this.useFFT.Location = new System.Drawing.Point(7, 20);
            this.useFFT.Name = "useFFT";
            this.useFFT.Size = new System.Drawing.Size(44, 17);
            this.useFFT.TabIndex = 0;
            this.useFFT.TabStop = true;
            this.useFFT.Text = "FFT";
            this.useFFT.UseVisualStyleBackColor = true;
            // 
            // pitchTrackBar
            // 
            this.pitchTrackBar.Location = new System.Drawing.Point(486, 17);
            this.pitchTrackBar.Maximum = 5;
            this.pitchTrackBar.Minimum = -5;
            this.pitchTrackBar.Name = "pitchTrackBar";
            this.pitchTrackBar.Size = new System.Drawing.Size(128, 45);
            this.pitchTrackBar.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 545);
            this.Controls.Add(this.pitchTrackBar);
            this.Controls.Add(this.transformGroup);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.InputDevice);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chord);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pitch);
            this.Controls.Add(this.stop_record);
            this.Controls.Add(this.start_record);
            this.Controls.Add(this.tabControl1);
            this.MaximumSize = new System.Drawing.Size(885, 583);
            this.MinimumSize = new System.Drawing.Size(885, 583);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recorder By Hila Shmuel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.timeDomain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.frequencyDomain)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axAcroPDF1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.transformGroup.ResumeLayout(false);
            this.transformGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pitchTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button start_record;
        private System.Windows.Forms.Button stop_record;
        private System.Windows.Forms.TextBox pitch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox chord;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox timeDomain;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox frequencyDomain;
        private System.Windows.Forms.ComboBox InputDevice;
        private System.Windows.Forms.PictureBox pictureBox1;
        private AxAcroPDFLib.AxAcroPDF axAcroPDF1;
        private System.Windows.Forms.GroupBox transformGroup;
        private System.Windows.Forms.RadioButton useDFT;
        private System.Windows.Forms.RadioButton useFFT;
        private System.Windows.Forms.TrackBar pitchTrackBar;
    }
}

