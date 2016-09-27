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
            cboPorts.Items.AddRange(ports);
            cboPorts.SelectedIndex = 0;
            string ArduinoPort = null;

            try
            {
                ArduinoPort = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "iRacingFanboi.txt");
                //if (ArduinoPort == "SDF") { }
            }
            catch (Exception e2)
            {
                //Do nothing
                //ArduinoPort = "test";
            }
          

            //if ((System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "iRacingFanboi.txt") != null)) {
            if (ArduinoPort != null) { 
                label1.Text = "Arduino set to " + ArduinoPort;
                cboPorts.SelectedItem = ArduinoPort;
            }
            else if (AutodetectArduinoPort() != null)
            {
                label1.Text = "Arduino autodetected on " + AutodetectArduinoPort();
                cboPorts.SelectedItem = AutodetectArduinoPort();
            }
            lblConn.Text = "No connection with iRacing API";
            sdk.Startup(); 
            tmr.Enabled = true;


        }

        private void tmr_Tick(object sender, EventArgs e) {
            if (Process.GetProcesses().Any(p => p.ProcessName.Contains("iRacingService")))
            {
                lblColor.BackColor = Color.FromArgb(0, 0, 200);
                if (Process.GetProcesses().Any(p => p.ProcessName.Contains("iRacingSim")))
                {
                    lblColor.BackColor = Color.FromArgb(0, 200, 0);
                    sdk.Startup();
                }
            } else {
                lblConn.Text = "iRacing NOT running";
                lblColor.BackColor = Color.FromArgb(200, 0, 0); sdk.Shutdown();
            }

            if (sdk.IsConnected())
            {
                lblConn.Text = "iRacing SDK IsConnected";
                
            }
            else if (sdk.IsInitialized)
             {
                lblConn.Text = "iRacing SDK IsInitialized";
                
             }
            

                if (sdk.IsConnected()) {

                    /*
                    Gear = Convert.ToInt32(sdk.GetData("Gear"));
                    RPM = Convert.ToDouble(sdk.GetData("RPM"));
                    Fuel = Convert.ToDouble(sdk.GetData("FuelLevelPct"));
                    Shift = Convert.ToDouble(sdk.GetData("ShiftIndicatorPct"));
                    Engine = Convert.ToByte(sdk.GetData("EngineWarnings"));
                    Speed_north = Convert.ToDouble(sdk.GetData("VelocityX"));
                    Meters = Convert.ToDouble(sdk.GetData("LapDist"));
                    IsOnTrack = Convert.ToInt32(sdk.GetData("IsOnTrack"));

                    if (Convert.ToInt32(sdk.GetData("VelocityY")) > 0 )
                    {
                        Speed_west = Convert.ToDouble(sdk.GetData("VelocityY"));
                        Speed_east = 0;
                    }
                    else if (Convert.ToInt32(sdk.GetData("VelocityY")) <=0)
                        {
                         Speed_east = Convert.ToDouble(sdk.GetData("VelocityY")) * -1;
                         Speed_west = 0;
                        }
                    this.Text = Shift.ToString();

                    iRPM = Convert.ToInt16(RPM);
                    iFuel = Convert.ToByte(Math.Round(Fuel * 100));
                    iShift = Convert.ToByte(Math.Round((Shift * 100 * 16) / 100));
                    iSpeed_north = Convert.ToInt16(Speed_north);
                    iSpeed_west = Convert.ToInt16(Speed_west);
                    iSpeed_east = Convert.ToInt16(Speed_east);
                    iMeters = Convert.ToInt16(Meters);*/
                    
                //MPH
                if (this.radioButtonMph.Checked)
                {
                    Speed = Convert.ToDouble(sdk.GetData("Speed")) * 2.23693629;
                    carTopSpeedb = Speed;
                }
                //KPH or Fan Speed user defined
                else
                {
                    Speed = Convert.ToDouble(sdk.GetData("Speed")) * 3.6;
                    carTopSpeedb = Speed;
                }

                carTopSpeeda = Convert.ToDouble(carTopSpeed.Value);
                lblSpeed.Text = "Current speed: " + Math.Round(Speed, 0);
            
                if (Speed >= carTopSpeeda)
                {
                    carTopSpeeda = Speed;
                    //If user has allowed top speed auto update and not defined a constant fan speed
                    if ((chkAutoTopSpeed.Checked) && (!radioButtonConst.Checked)) {
                        carTopSpeed.Value = Convert.ToDecimal(carTopSpeedb);
                    }                        
                }

                if (!radioButtonConst.Checked)
                {
                    Speed = Speed * (255 / carTopSpeeda);
                } else //User defined fan speed
                {
                    Speed = Convert.ToDouble(carTopSpeed.Value);
                }

                //Fan speed cannot exceed 255 (byte limit)
                if (Speed > 255)
                {
                    Speed = 255;
                }

                lblFanSpeed.Text = "Fan speed: " + Math.Round(Speed, 0);

                writeSerialPort(Speed, e);             
               
            }
            else //iRacing not connected
            {
                if (radioButtonConst.Checked)
                {
                    if (Convert.ToDouble(carTopSpeed.Value) < 256)
                    {
                        writeSerialPort(Convert.ToDouble(carTopSpeed.Value), e);
                    }
                    else { writeSerialPort(255, e);
                    }
                } else
                {
                    writeSerialPort(0, e);
                }

            }
        }            
        
        private void startSerialPort()
        {
            try {
                SP.Close();
                sdk.Shutdown();
            }
            catch (Exception e)
            {
                
            }
            try
            {
                SP = new SerialPort(cboPorts.Text, 9600, Parity.None, 8);

                SP.Open();
            }
            catch (Exception e)
            {
                
                MessageBox.Show("NO!");
                if (AutodetectArduinoPort() != null)
                {
                    label1.Text = "Arduino autodetected on " + AutodetectArduinoPort();
                    cboPorts.SelectedItem = AutodetectArduinoPort();
                    SP = new SerialPort(cboPorts.Text, 9600, Parity.None, 8);

                    SP.Open();
                }
                            }

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
            catch (Exception e2)
            {
                startSerialPort();
            }

        }

        private void trkWindFront_Scroll(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private string AutodetectArduinoPort()
        {

            

            ManagementScope connectionScope = new ManagementScope();
            SelectQuery serialQuery = new SelectQuery("SELECT * FROM Win32_SerialPort");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(connectionScope, serialQuery);

            try
            {
                foreach (ManagementObject item in searcher.Get())
                {
                    string desc = item["Description"].ToString();
                    string deviceId = item["DeviceID"].ToString();

                    if (desc.Contains("Arduino"))
                    {
                        return deviceId;
                    }
                }
            }
            catch (ManagementException e)
            {
                /* Do Nothing */
            }

            return null;
        }

        private void cboPorts_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void cboPorts_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }

        private void lblSpeed_Click(object sender, EventArgs e)
        {

        }
        
        private void cboPorts_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void cmbSerial_Click(object sender, EventArgs e)
        {
            tmr.Enabled = false;
            startSerialPort();
            tmr.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            //string fileName = "iRacingFanboi.txt";


            //System.IO.StreamWriter objWriter;
            //objWriter = new System.IO.StreamWriter(fileName, true);

            //objWriter.Write("txtNameText");
            //objWriter.Write(txtAddress.Text);
            //objWriter.Write(txtEmail.Text);
            //objWriter.Write(txtPhone.Text);
            //objWriter.Flush();

            System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "iRacingFanboi.txt", cboPorts.Text);
            label1.Text = "Arduino set to " + cboPorts.Text;

            MessageBox.Show("Details have been saved");
        }
    }
}
