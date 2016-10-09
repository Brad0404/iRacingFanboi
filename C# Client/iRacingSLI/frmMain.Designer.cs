namespace iRacingSLI {
    partial class frmMain {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.tmr = new System.Windows.Forms.Timer(this.components);
            this.cboPorts = new System.Windows.Forms.ComboBox();
            this.lblArduinoStatus = new System.Windows.Forms.Label();
            this.lblConn = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.carTopSpeed = new System.Windows.Forms.NumericUpDown();
            this.radioButtonKph = new System.Windows.Forms.RadioButton();
            this.radioButtonMph = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkAutoTopSpeed = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.maxFanSpeed = new System.Windows.Forms.NumericUpDown();
            this.lblFanSpeed = new System.Windows.Forms.Label();
            this.lbliracingStatus = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.fanProgressBar = new System.Windows.Forms.ProgressBar();
            this.checkBoxManualSpeed = new System.Windows.Forms.CheckBox();
            this.numericUpDownManualSpeed = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownReplayFanSpeed = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.carTopSpeed)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxFanSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownManualSpeed)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownReplayFanSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // tmr
            // 
            this.tmr.Enabled = true;
            this.tmr.Interval = 50;
            this.tmr.Tick += new System.EventHandler(this.tmr_Tick);
            // 
            // cboPorts
            // 
            this.cboPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPorts.FormattingEnabled = true;
            this.cboPorts.Location = new System.Drawing.Point(7, 269);
            this.cboPorts.Name = "cboPorts";
            this.cboPorts.Size = new System.Drawing.Size(163, 21);
            this.cboPorts.TabIndex = 5;
            this.cboPorts.SelectedValueChanged += new System.EventHandler(this.cboPorts_SelectedValueChanged);
            // 
            // lblArduinoStatus
            // 
            this.lblArduinoStatus.BackColor = System.Drawing.Color.Red;
            this.lblArduinoStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblArduinoStatus.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblArduinoStatus.Location = new System.Drawing.Point(8, 293);
            this.lblArduinoStatus.Name = "lblArduinoStatus";
            this.lblArduinoStatus.Size = new System.Drawing.Size(72, 19);
            this.lblArduinoStatus.TabIndex = 6;
            this.lblArduinoStatus.Text = "Arduino";
            this.lblArduinoStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblConn
            // 
            this.lblConn.AutoSize = true;
            this.lblConn.Location = new System.Drawing.Point(14, 225);
            this.lblConn.Name = "lblConn";
            this.lblConn.Size = new System.Drawing.Size(0, 13);
            this.lblConn.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 253);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Select your Arduino COM port:";
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Location = new System.Drawing.Point(150, 184);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(69, 13);
            this.lblSpeed.TabIndex = 13;
            this.lblSpeed.Text = "Car Speed: 0";
            this.lblSpeed.Click += new System.EventHandler(this.lblSpeed_Click);
            // 
            // carTopSpeed
            // 
            this.carTopSpeed.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.carTopSpeed.Location = new System.Drawing.Point(98, 21);
            this.carTopSpeed.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.carTopSpeed.Name = "carTopSpeed";
            this.carTopSpeed.Size = new System.Drawing.Size(43, 20);
            this.carTopSpeed.TabIndex = 20;
            this.carTopSpeed.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.carTopSpeed.ValueChanged += new System.EventHandler(this.carTopSpeed_ValueChanged);
            // 
            // radioButtonKph
            // 
            this.radioButtonKph.AutoSize = true;
            this.radioButtonKph.Checked = true;
            this.radioButtonKph.Location = new System.Drawing.Point(153, 21);
            this.radioButtonKph.Name = "radioButtonKph";
            this.radioButtonKph.Size = new System.Drawing.Size(47, 17);
            this.radioButtonKph.TabIndex = 21;
            this.radioButtonKph.TabStop = true;
            this.radioButtonKph.Text = "KPH";
            this.radioButtonKph.UseVisualStyleBackColor = true;
            // 
            // radioButtonMph
            // 
            this.radioButtonMph.AutoSize = true;
            this.radioButtonMph.Location = new System.Drawing.Point(198, 21);
            this.radioButtonMph.Name = "radioButtonMph";
            this.radioButtonMph.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.radioButtonMph.Size = new System.Drawing.Size(51, 17);
            this.radioButtonMph.TabIndex = 22;
            this.radioButtonMph.TabStop = true;
            this.radioButtonMph.Text = "MPH";
            this.radioButtonMph.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDownReplayFanSpeed);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.chkAutoTopSpeed);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.carTopSpeed);
            this.groupBox1.Controls.Add(this.maxFanSpeed);
            this.groupBox1.Controls.Add(this.radioButtonMph);
            this.groupBox1.Controls.Add(this.radioButtonKph);
            this.groupBox1.Location = new System.Drawing.Point(7, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(261, 121);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "iRacing Fan Control";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(233, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "%";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 27;
            this.label3.Text = "Car top speed:";
            // 
            // chkAutoTopSpeed
            // 
            this.chkAutoTopSpeed.AutoSize = true;
            this.chkAutoTopSpeed.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAutoTopSpeed.Checked = true;
            this.chkAutoTopSpeed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoTopSpeed.Location = new System.Drawing.Point(9, 47);
            this.chkAutoTopSpeed.Name = "chkAutoTopSpeed";
            this.chkAutoTopSpeed.Size = new System.Drawing.Size(168, 17);
            this.chkAutoTopSpeed.TabIndex = 23;
            this.chkAutoTopSpeed.Text = "Update top speed dynamically";
            this.chkAutoTopSpeed.UseVisualStyleBackColor = true;
            this.chkAutoTopSpeed.CheckedChanged += new System.EventHandler(this.chkAutoTopSpeed_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "Maximum In-game Fan Speed:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // maxFanSpeed
            // 
            this.maxFanSpeed.Location = new System.Drawing.Point(190, 69);
            this.maxFanSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.maxFanSpeed.Name = "maxFanSpeed";
            this.maxFanSpeed.Size = new System.Drawing.Size(38, 20);
            this.maxFanSpeed.TabIndex = 25;
            this.maxFanSpeed.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.maxFanSpeed.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // lblFanSpeed
            // 
            this.lblFanSpeed.AutoSize = true;
            this.lblFanSpeed.Location = new System.Drawing.Point(14, 184);
            this.lblFanSpeed.Name = "lblFanSpeed";
            this.lblFanSpeed.Size = new System.Drawing.Size(110, 13);
            this.lblFanSpeed.TabIndex = 24;
            this.lblFanSpeed.Text = "Fan Speed (PWM) : 0";
            this.lblFanSpeed.Click += new System.EventHandler(this.lblFanSpeed_Click);
            // 
            // lbliracingStatus
            // 
            this.lbliracingStatus.BackColor = System.Drawing.Color.Red;
            this.lbliracingStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbliracingStatus.Location = new System.Drawing.Point(193, 293);
            this.lbliracingStatus.Name = "lbliracingStatus";
            this.lbliracingStatus.Size = new System.Drawing.Size(76, 19);
            this.lbliracingStatus.TabIndex = 25;
            this.lbliracingStatus.Text = "iRacing SDK";
            this.lbliracingStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(177, 267);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(92, 23);
            this.btnSave.TabIndex = 25;
            this.btnSave.Text = "Save Settings";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // fanProgressBar
            // 
            this.fanProgressBar.BackColor = System.Drawing.SystemColors.HighlightText;
            this.fanProgressBar.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.fanProgressBar.Location = new System.Drawing.Point(16, 209);
            this.fanProgressBar.Maximum = 255;
            this.fanProgressBar.Name = "fanProgressBar";
            this.fanProgressBar.Size = new System.Drawing.Size(240, 13);
            this.fanProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.fanProgressBar.TabIndex = 27;
            this.fanProgressBar.Visible = false;
            // 
            // checkBoxManualSpeed
            // 
            this.checkBoxManualSpeed.AutoSize = true;
            this.checkBoxManualSpeed.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxManualSpeed.Location = new System.Drawing.Point(17, 152);
            this.checkBoxManualSpeed.Name = "checkBoxManualSpeed";
            this.checkBoxManualSpeed.Size = new System.Drawing.Size(92, 17);
            this.checkBoxManualSpeed.TabIndex = 28;
            this.checkBoxManualSpeed.Text = "Set fan speed";
            this.checkBoxManualSpeed.UseVisualStyleBackColor = true;
            this.checkBoxManualSpeed.CheckedChanged += new System.EventHandler(this.checkBoxManualSpeed_CheckedChanged);
            // 
            // numericUpDownManualSpeed
            // 
            this.numericUpDownManualSpeed.Location = new System.Drawing.Point(190, 15);
            this.numericUpDownManualSpeed.Name = "numericUpDownManualSpeed";
            this.numericUpDownManualSpeed.Size = new System.Drawing.Size(38, 20);
            this.numericUpDownManualSpeed.TabIndex = 29;
            this.numericUpDownManualSpeed.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDownManualSpeed.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged_1);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.numericUpDownManualSpeed);
            this.groupBox2.Location = new System.Drawing.Point(7, 134);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(261, 41);
            this.groupBox2.TabIndex = 30;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Manual Fan Control";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(233, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "%";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(123, 13);
            this.label6.TabIndex = 33;
            this.label6.Text = "Fan speed during replay:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(233, 95);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 13);
            this.label7.TabIndex = 34;
            this.label7.Text = "%";
            // 
            // numericUpDownReplayFanSpeed
            // 
            this.numericUpDownReplayFanSpeed.Location = new System.Drawing.Point(190, 92);
            this.numericUpDownReplayFanSpeed.Name = "numericUpDownReplayFanSpeed";
            this.numericUpDownReplayFanSpeed.Size = new System.Drawing.Size(38, 20);
            this.numericUpDownReplayFanSpeed.TabIndex = 35;
            this.numericUpDownReplayFanSpeed.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged_2);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 316);
            this.Controls.Add(this.checkBoxManualSpeed);
            this.Controls.Add(this.fanProgressBar);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lbliracingStatus);
            this.Controls.Add(this.lblFanSpeed);
            this.Controls.Add(this.lblSpeed);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblConn);
            this.Controls.Add(this.lblArduinoStatus);
            this.Controls.Add(this.cboPorts);
            this.Controls.Add(this.groupBox2);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "iRacingFanBoi";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.carTopSpeed)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxFanSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownManualSpeed)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownReplayFanSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmr;
        private System.Windows.Forms.ComboBox cboPorts;
        private System.Windows.Forms.Label lblArduinoStatus;
        private System.Windows.Forms.Label lblConn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.NumericUpDown carTopSpeed;
        private System.Windows.Forms.RadioButton radioButtonKph;
        private System.Windows.Forms.RadioButton radioButtonMph;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblFanSpeed;
        private System.Windows.Forms.CheckBox chkAutoTopSpeed;
        private System.Windows.Forms.Label lbliracingStatus;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown maxFanSpeed;
        private System.Windows.Forms.ProgressBar fanProgressBar;
        private System.Windows.Forms.CheckBox checkBoxManualSpeed;
        private System.Windows.Forms.NumericUpDown numericUpDownManualSpeed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownReplayFanSpeed;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
    }
}

