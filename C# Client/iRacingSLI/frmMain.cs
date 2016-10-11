using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using iRSDKSharp;
using System.Diagnostics;
using System.Management;
using System.Runtime.CompilerServices;

namespace iRacingSLI {
    public partial class frmMain : Form {
        private string CONFIG_FILE_NAME = AppDomain.CurrentDomain.BaseDirectory + "iRacingFanboi.txt";
        private string preferredPort = "";

        iRacingSDK sdk = new iRacingSDK();
        
        SerialPort SP;

        //int Gear, IsOnTrack;
        double Speed, carTopSpeeda, carTopSpeedb;
        
        //double RPM, Fuel, Shift, Speed_north, Speed_west, Speed_east, Meters;
        //short iRPM, iFuel, iShift, iSpeed_north, iSpeed_west, iSpeed_east, iMeters;
        //byte Engine;
        //byte[] serialdata = new byte[19];
        byte[] serialdata = new byte[5];

        System.Random randnum = new System.Random();

        public frmMain() {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            String[] ports = SerialPort.GetPortNames();

            carTopSpeed.Value = Convert.ToDecimal(Properties.Settings.Default.TopSpeed);
            chkAutoTopSpeed.Checked = Properties.Settings.Default.AutoTopSpeed;
            maxFanSpeed.Value = Convert.ToDecimal(Properties.Settings.Default.MaxFanSpeed);
            numericUpDownReplayFanSpeed.Value = Convert.ToDecimal(Properties.Settings.Default.FanSpeedDuringReplay);
            checkBoxManualSpeed.Checked = Properties.Settings.Default.EnableManualSpeed;
            numericUpDownManualSpeed.Value = Convert.ToDecimal(Properties.Settings.Default.ManualFanSpeed);
  
            if (ports.Length > 0)
            {
                cboPorts.Items.AddRange(ports);
                preferredPort = Properties.Settings.Default.Port.ToString();
            }
            else
            {
                label1.Text = "No Arduino's found";
                btnSave.Enabled = false;
            }
            
            lblConn.Text = "No connection with iRacing API";
            sdk.Startup(); 
            tmr.Enabled = true;
        }

        private void tmr_Tick(object sender, EventArgs e) {

            double fanSpeedPercent = Convert.ToDouble(maxFanSpeed.Value);
            double manualSpeed = Convert.ToDouble(numericUpDownManualSpeed.Value) * 2.55; // remap manual speed percentage to 0-255
            double replayFanSpeed = Convert.ToDouble(numericUpDownReplayFanSpeed.Value) * 2.55; // remap replayfanspeed to 0-255
            if ((fanSpeedPercent < 0) || (fanSpeedPercent > 100))
                fanSpeedPercent = 100;
            if ((manualSpeed < 0) || (manualSpeed > 255))
                manualSpeed = 255;
            if ((replayFanSpeed < 0) || (replayFanSpeed > 255))
                replayFanSpeed = 0;

            // show progress bar if manual speed adjustment is selected
            if (checkBoxManualSpeed.Checked)
                fanProgressBar.Visible = true;
            else
                fanProgressBar.Visible = false;

            if (Process.GetProcesses().Any(p => p.ProcessName.Contains("iRacingService")))
            {
                lbliracingStatus.BackColor = Color.Yellow;
                if (Process.GetProcesses().Any(p => p.ProcessName.Contains("iRacingSim")))
                {
                    sdk.Startup();
                }
            }
            else
            {
                lblConn.Text = "iRacing NOT running";
                lbliracingStatus.BackColor = Color.Red;
                sdk.Shutdown();
            }

            if (sdk.IsConnected())
            {
                lblConn.Text = "iRacing SDK is connected";
                lbliracingStatus.BackColor = Color.LimeGreen;
            }
            else if (sdk.IsInitialized)
            {
                lblConn.Text = "iRacing SDK is initialized";
                lbliracingStatus.BackColor = Color.Yellow;
            }
            
            if (sdk.IsConnected())
            {                    
                //MPH
                if (this.radioButtonMph.Checked)
                {
                    Speed = Convert.ToDouble(sdk.GetData("Speed")) * 2.23693629;
                    carTopSpeedb = Speed;
                }
                //KPH
                else
                {
                    Speed = Convert.ToDouble(sdk.GetData("Speed")) * 3.6;
                    carTopSpeedb = Speed;
                }

                carTopSpeeda = Convert.ToDouble(carTopSpeed.Value);
                lblSpeed.Text = "Current car speed: " + Math.Round(Speed, 0);
            
                if (Speed >= carTopSpeeda)
                {
                    carTopSpeeda = Speed;
                    //If user has allowed top speed auto update and not defined a constant fan speed
                    if ((chkAutoTopSpeed.Checked) && (!checkBoxManualSpeed.Checked)) {
                        carTopSpeed.Value = Convert.ToDecimal(carTopSpeedb);
                    }                        
                }

                if (checkBoxManualSpeed.Checked) // manual fan speed 
                {
                    Speed = manualSpeed;
                    fanProgressBar.Value = (int)Math.Round(Speed, 0);
                }
                else // remap car speed to 0-255 range, and then scale by max fan speed %
                {
                    if (Convert.ToDouble(sdk.GetData("VelocityX")) < 0)
                        Speed = 0;  // User is going in reverse, stop the fans...
                    Speed = Speed * (255 / carTopSpeeda) * (fanSpeedPercent / 100);
                    if (Speed > 255)
                        Speed = 255;  //Fan speed cannot exceed 255 (byte limit) 
                    if ((Convert.ToBoolean(sdk.GetData("IsReplayPlaying"))) == true)
                        Speed = replayFanSpeed;  // replay currently running, run fan according to setting
                }

                lblFanSpeed.Text = "Fan Speed (PWM): " + Math.Round(Speed, 0);  // update GUI with current fan speed
                writeSerialPort(Speed, e);       // write speed to arduino serial port      
            }
            else //iRacing not connected
            {
                if (checkBoxManualSpeed.Checked)
                {
                    lblFanSpeed.Text = "Fan speed (PWM): " + Math.Round(manualSpeed, 0);
                    fanProgressBar.Value = (int) Math.Round(manualSpeed,0);
                    writeSerialPort(manualSpeed, e);
                }
                else
                {
                    writeSerialPort(0, e);
                    lblFanSpeed.Text = "Fan speed (PWM): 0";
                }
            }
        }

        private void startSerialPort()
        {
            try
            {
                SP.Close();
                sdk.Shutdown();
            }
            catch (Exception){}

            foreach(string port in getSortedSerialPortsToOpen())
            { 
                try
                {
                    openSerialPort(port);

                    // We successfully opened this port, so set the combo box to that
                    // value and set our status to green.
                    cboPorts.SelectedItem = port;
                    lblArduinoStatus.BackColor = Color.LimeGreen;
                    label1.Text = "Connected to port " + port;
                    tmr.Enabled = true;
                    break;
                }
                catch (Exception)
                {
                    label1.Text = "Failed connecting to port " + port;
                    lblArduinoStatus.BackColor = Color.Red;
                    tmr.Enabled = false;
                }
            }
        }

        private List<string> getSortedSerialPortsToOpen()
        {
            List<string> ports = new List<string>();

            if( cboPorts.SelectedItem != null )
            {
                // If someone selected the item, let's not attempt all ports
                // just the one they told us to use.
                ports.Add(cboPorts.SelectedItem.ToString());
                return ports;
            }

            if( preferredPort != null && cboPorts.Items.IndexOf(preferredPort) >= 0)
            {
                // If the user had saved a port, lets force ourselves to use that
                // port and not all other ports.
                ports.Add(preferredPort);
                return ports;
            }

            // Lets try them all!
            foreach (string port in cboPorts.Items)
            {
                ports.Add(port);
            }
            return ports;
        }

        private void openSerialPort(string port)
        {
            SP = new SerialPort(port, 9600, Parity.None, 8);
            SP.Open();
        }

        private void writeSerialPort(double Speed, EventArgs e)
        {
            serialdata[0] = 255;
            serialdata[1] = 88;
            serialdata[2] = 255;
            serialdata[3] = Convert.ToByte(Speed);
            //serialdata[4] = Convert.ToByte(carTopSpeeda);

            /*serialdata[3] = Convert.ToByte(Gear + 1);
            serialdata[5] = Convert.ToByte((iRPM >> 8) & 0x00FF);
            serialdata[6] = Convert.ToByte(iRPM & 0x00FF);
            serialdata[7] = Convert.ToByte(iFuel);
            serialdata[8] = Convert.ToByte(iShift);
            serialdata[9] = Engine;
            serialdata[10] = Convert.ToByte((iSpeed_north >> 8) & 0x00FF);
            serialdata[11] = Convert.ToByte(iSpeed_north & 0x00FF);
            serialdata[12] = Convert.ToByte((iSpeed_west >> 8) & 0x00FF);
            serialdata[13] = Convert.ToByte(iSpeed_west & 0x00FF);
            serialdata[14] = Convert.ToByte((iSpeed_east >> 8) & 0x00FF);
            serialdata[15] = Convert.ToByte(iSpeed_east & 0x00FF);
            serialdata[16] = Convert.ToByte((iMeters >> 8) & 0x00FF);
            serialdata[17] = Convert.ToByte(iMeters & 0x00FF);
            serialdata[18] = Convert.ToByte(IsOnTrack);*/
            
            try
            {
                //SP.Write(serialdata, 0, 19);
                SP.Write(serialdata, 0, 4);
            }
            catch (Exception)
            {
                startSerialPort();
            }
        }

        private void cboPorts_SelectedValueChanged(object sender, EventArgs e)
        {
            startSerialPort();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Alt | Keys.U)) // Alt U hotkey for increase manual speed
            {
                if (numericUpDownManualSpeed.Value < 90)
                    numericUpDownManualSpeed.Value = numericUpDownManualSpeed.Value + 10;
                else
                    numericUpDownManualSpeed.Value = 100;
                return true;
            }
            if (keyData == (Keys.Alt | Keys.D)) // Alt D hotkey for decrease manual speed
            {
                if (numericUpDownManualSpeed.Value > 10)
                    numericUpDownManualSpeed.Value = numericUpDownManualSpeed.Value - 10;
                else
                    numericUpDownManualSpeed.Value = 0;
                return true;
            }
            if (keyData == (Keys.Alt | Keys.M)) // Alt M hotkey for manual speed checkbox
            {
                checkBoxManualSpeed.Checked = !checkBoxManualSpeed.Checked;
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.EnableManualSpeed = checkBoxManualSpeed.Checked;
            Properties.Settings.Default.ManualFanSpeed = System.Convert.ToInt32(numericUpDownManualSpeed.Value);
            Properties.Settings.Default.Port = cboPorts.SelectedItem.ToString();
            Properties.Settings.Default.TopSpeed = System.Convert.ToInt32(carTopSpeed.Value);
            Properties.Settings.Default.MaxFanSpeed = System.Convert.ToInt32(maxFanSpeed.Value);
            Properties.Settings.Default.AutoTopSpeed = chkAutoTopSpeed.Checked;
            Properties.Settings.Default.FanSpeedDuringReplay = System.Convert.ToInt32(numericUpDownReplayFanSpeed.Value);
            Properties.Settings.Default.Save();
        }

        private void carTopSpeed_ValueChanged(object sender, EventArgs e)
        {

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            writeSerialPort(0, e);  // stop fans if application closing
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void checkBoxManualSpeed_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkAutoTopSpeed_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void lblFanSpeed_Click(object sender, EventArgs e)
        {

        }

        private void lblSpeed_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged_2(object sender, EventArgs e)
        {

        }

        private void cboPorts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
        
    }
}
