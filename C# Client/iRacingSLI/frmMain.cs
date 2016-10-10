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

            if (ports.Length > 0)
            {
                cboPorts.Items.AddRange(ports);
                preferredPort = readSerialPortFromFile();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveSerialPortToFile(cboPorts.SelectedItem.ToString());
        }
        
        private void saveSerialPortToFile(string port)
        {
            try
            {
                System.IO.File.WriteAllText(CONFIG_FILE_NAME, port);
                MessageBox.Show("Details have been saved", 
                                "Save port details", 
                                MessageBoxButtons.OK, 
                                MessageBoxIcon.Information);
            }
            catch(Exception e)
            {
                MessageBox.Show("Failed to write to file" + CONFIG_FILE_NAME + "\n\n" + e.Message,
                                "Failed to write config file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string readSerialPortFromFile()
        {
            string my_port = "";
            if (System.IO.File.Exists(CONFIG_FILE_NAME))
            {
                try
                {
                    System.IO.StreamReader file =
                        new System.IO.StreamReader(CONFIG_FILE_NAME);
                    // Just read the first line...
                    my_port = file.ReadLine();
                    file.Close();
                }
                catch(Exception e)
                {
                    MessageBox.Show("Failed to read file " + CONFIG_FILE_NAME + "\n\n" + e.Message,
                                    "Failed to read file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return my_port;
        }
    }
}
