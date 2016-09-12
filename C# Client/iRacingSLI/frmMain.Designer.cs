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
            this.tmr = new System.Windows.Forms.Timer(this.components);
            this.cmbSerial = new System.Windows.Forms.Button();
            this.cboPorts = new System.Windows.Forms.ComboBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.lblConn = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.carTopSpeed = new System.Windows.Forms.NumericUpDown();
            this.radioButtonKph = new System.Windows.Forms.RadioButton();
            this.radioButtonMph = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonConst = new System.Windows.Forms.RadioButton();
            this.chkAutoTopSpeed = new System.Windows.Forms.CheckBox();
            this.lblFanSpeed = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.carTopSpeed)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmr
            // 
            this.tmr.Enabled = true;
            this.tmr.Interval = 50;
            this.tmr.Tick += new System.EventHandler(this.tmr_Tick);
            // 
            // cmbSerial
            // 
            this.cmbSerial.Location = new System.Drawing.Point(9, 180);
            this.cmbSerial.Name = "cmbSerial";
            this.cmbSerial.Size = new System.Drawing.Size(192, 43);
            this.cmbSerial.TabIndex = 0;
            this.cmbSerial.Text = "Start serial port";
            this.cmbSerial.UseVisualStyleBackColor = true;
            this.cmbSerial.Click += new System.EventHandler(this.cmbSerial_Click);
            // 
            // cboPorts
            // 
            this.cboPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPorts.FormattingEnabled = true;
            this.cboPorts.Location = new System.Drawing.Point(9, 140);
            this.cboPorts.Name = "cboPorts";
            this.cboPorts.Size = new System.Drawing.Size(192, 21);
            this.cboPorts.TabIndex = 5;
            this.cboPorts.SelectedIndexChanged += new System.EventHandler(this.cboPorts_SelectedIndexChanged);
            this.cboPorts.SelectedValueChanged += new System.EventHandler(this.cboPorts_SelectedValueChanged);
            this.cboPorts.TextChanged += new System.EventHandler(this.cboPorts_TextChanged);
            // 
            // lblColor
            // 
            this.lblColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblColor.Location = new System.Drawing.Point(9, 226);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(18, 19);
            this.lblColor.TabIndex = 6;
            // 
            // lblConn
            // 
            this.lblConn.AutoSize = true;
            this.lblConn.Location = new System.Drawing.Point(6, 164);
            this.lblConn.Name = "lblConn";
            this.lblConn.Size = new System.Drawing.Size(0, 13);
            this.lblConn.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Select your Arduino COM port:";
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Location = new System.Drawing.Point(14, 9);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(87, 13);
            this.lblSpeed.TabIndex = 13;
            this.lblSpeed.Text = "Current Speed: 0";
            this.lblSpeed.Click += new System.EventHandler(this.lblSpeed_Click);
            // 
            // carTopSpeed
            // 
            this.carTopSpeed.Location = new System.Drawing.Point(6, 19);
            this.carTopSpeed.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.carTopSpeed.Name = "carTopSpeed";
            this.carTopSpeed.Size = new System.Drawing.Size(47, 20);
            this.carTopSpeed.TabIndex = 20;
            this.carTopSpeed.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            // 
            // radioButtonKph
            // 
            this.radioButtonKph.AutoSize = true;
            this.radioButtonKph.Checked = true;
            this.radioButtonKph.Location = new System.Drawing.Point(59, 19);
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
            this.radioButtonMph.Location = new System.Drawing.Point(100, 19);
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
            this.groupBox1.Controls.Add(this.radioButtonConst);
            this.groupBox1.Controls.Add(this.chkAutoTopSpeed);
            this.groupBox1.Controls.Add(this.carTopSpeed);
            this.groupBox1.Controls.Add(this.radioButtonMph);
            this.groupBox1.Controls.Add(this.radioButtonKph);
            this.groupBox1.Location = new System.Drawing.Point(9, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(261, 70);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Top Speed";
            // 
            // radioButtonConst
            // 
            this.radioButtonConst.AutoSize = true;
            this.radioButtonConst.Location = new System.Drawing.Point(158, 19);
            this.radioButtonConst.Name = "radioButtonConst";
            this.radioButtonConst.Size = new System.Drawing.Size(96, 17);
            this.radioButtonConst.TabIndex = 24;
            this.radioButtonConst.TabStop = true;
            this.radioButtonConst.Text = "Set Fan Speed";
            this.radioButtonConst.UseVisualStyleBackColor = true;
            // 
            // chkAutoTopSpeed
            // 
            this.chkAutoTopSpeed.AutoSize = true;
            this.chkAutoTopSpeed.Checked = true;
            this.chkAutoTopSpeed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoTopSpeed.Location = new System.Drawing.Point(6, 45);
            this.chkAutoTopSpeed.Name = "chkAutoTopSpeed";
            this.chkAutoTopSpeed.Size = new System.Drawing.Size(125, 17);
            this.chkAutoTopSpeed.TabIndex = 23;
            this.chkAutoTopSpeed.Text = "Allow auto top speed";
            this.chkAutoTopSpeed.UseVisualStyleBackColor = true;
            // 
            // lblFanSpeed
            // 
            this.lblFanSpeed.AutoSize = true;
            this.lblFanSpeed.Location = new System.Drawing.Point(17, 26);
            this.lblFanSpeed.Name = "lblFanSpeed";
            this.lblFanSpeed.Size = new System.Drawing.Size(74, 13);
            this.lblFanSpeed.TabIndex = 24;
            this.lblFanSpeed.Text = "Fan Speed : 0";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 250);
            this.Controls.Add(this.lblFanSpeed);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblSpeed);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblConn);
            this.Controls.Add(this.lblColor);
            this.Controls.Add(this.cboPorts);
            this.Controls.Add(this.cmbSerial);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "iRacing DX SLI";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.carTopSpeed)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmr;
        private System.Windows.Forms.Button cmbSerial;
        private System.Windows.Forms.ComboBox cboPorts;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.Label lblConn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.NumericUpDown carTopSpeed;
        private System.Windows.Forms.RadioButton radioButtonKph;
        private System.Windows.Forms.RadioButton radioButtonMph;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblFanSpeed;
        private System.Windows.Forms.CheckBox chkAutoTopSpeed;
        private System.Windows.Forms.RadioButton radioButtonConst;
    }
}

