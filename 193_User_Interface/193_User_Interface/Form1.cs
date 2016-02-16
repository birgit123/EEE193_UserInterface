using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;
using System.IO.Ports;
using System.IO;
using System.Threading;

namespace _193_User_Interface
{

    public partial class Form1 : Form
    {
        //private SerialPort port;
        public bool flag_connect = true;

        public Form1()
        {
            InitializeComponent();        
          //  SerialPort(); //call function for serial port control 
          //  Read(); //call to function for reading in serial data 
                     
            FormBorderStyle = FormBorderStyle.None;  
            WindowState = FormWindowState.Maximized;    //Maximize the form window

        }

        private class FlashPoint_Connection
        {
            //Initialize com port
            public SerialPort port = new SerialPort("COM5", 9600, Parity.None, 8, StopBits.One);
  
            private long Inlatitude { get; set; }
            private long Inlongitude { get; set; }
            public double latitude { get; set; }
            public double longitude { get; set; }
            public int Heart_Rate { get; set; }
            public string Date_Time { get; set; }

            public void Connect(bool flag_connect, ManiXButton.XButton xButton1)
            {
                try
                {
                    if (flag_connect)
                    {
                        port.Close();
                        xButton1.Text = "Click to Connect";
                        xButton1.Theme = ManiXButton.Theme.MSOffice2010_RED;

                    }

                    port.Open();
                    //Set button #1's status and color 
                    try
                    {
                        xButton1.Text = "Connected";
                        xButton1.Theme = ManiXButton.Theme.MSOffice2010_Green;
                    }
                    catch (Exception)
                    {
                        
                    }

                    //Read();

                    /*while(!flag_connect)
                    {
                        continue;

                        xButton1.Text = "Click to Connect";
                        xButton1.Theme = ManiXButton.Theme.MSOffice2010_RED;
                        port.Close();
                        Close();

                    }
                }*/

                    /* while (port.IsOpen && !flag_connect)
                     { 
                         //SerialPortDataReceived(port);
                         Read();
                         //flag_connect = false;

                         break;
                     }*/
                }
                catch (IOException)
                {
                    port.Close();
                    MessageBox.Show("Connection could not be established. Please press OK and try again.");
                    //Application.Exit();
                    //Close();
                }
            }
            public void Read(TextBox textBox1, TextBox textBox2, TextBox textBox3, TextBox textBox4)
            {
                string data = port.ReadTo("]");

                string[] words = data.Split(',');

                if (words.Length == 4 && words[0] != null && words[1] != null)
                {
                    try
                    {
                        Inlatitude = Int64.Parse(words[0]);
                        latitude = (Inlatitude / 1000000.0);
                        Inlongitude = Int64.Parse(words[1]);
                        longitude = (Inlongitude / 1000000.0);
                        try
                        {
                            textBox1.Text = ("" + latitude);
                            textBox2.Text = ("" + longitude);
                        }
                        catch(Exception)
                        { }
                        //Location_ShowAll_Tab.Text = ("Lat:" + latitude + " Lon:" + longitude);
                    }
                    catch (FormatException)
                    {
                        // Location_ShowAll_Tab.Text = ("Format Issue!!!!");
                        try
                        {
                            textBox1.Text = ("Format Error");
                            textBox2.Text = ("Format Error");
                        }
                        catch { }
                    }

                    if (words[2] == null)
                    {
                        Heart_Rate = 0; //set default heart rate
                        try
                        {
                            textBox3.Text = ("Set to default");
                        }
                        catch(Exception)
                        { }
                        // Heart_Rate_ShowAll_Tab.Text = ("Set to default");
                    }
                    else
                    {
                        try
                        {
                            Heart_Rate = Convert.ToInt32(words[2]);    // Convert String into int
                            textBox3.Text = ("" + Heart_Rate);
                        }
                        catch (Exception)
                        {
                            try
                            {
                                textBox3.Text = ("Format Error");
                            }
                            catch(Exception)
                            { }
                        }
                    }
                    if (words[3] == null)
                    {
                        try
                        {
                            textBox3.Text = ("Not available");
                        }
                        catch(Exception)
                        { }
                    }
                    else
                    {
                        try
                        {
                            Date_Time = words[3]; //  Keeps string as a string
                            textBox4.Text = ("" + Date_Time);
                        }
                        catch (Exception)
                        {
                            try
                            {
                                textBox4.Text = ("Format Error");
                            }
                            catch(Exception)
                            { }
                        }
                    }
                }
                else
                {
                    //set default lat/ long - csus
                    latitude = 38.556868;
                    longitude = -121.358592;
                }
            }
            public void Close()
            {
                port.Close();
            }
            //public void AppendTextBox1(string value, bool InvokeRequired, object Invoke, TextBox textBox1)
            //{
            //    if (InvokeRequired)
            //    {
            //        this.Invoke(new Action<string>(AppendTextBox1), new object[] { value });
            //        return;
            //    }
            //    textBox1.Text += value;
            //}
            //public void AppendTextBox2(string value, bool InvokeRequired, object Invoke, TextBox textBox2)
            //{
            //    if (InvokeRequired)
            //    {
            //        this.Invoke(new Action<string>(AppendTextBox2), new object[] { value });
            //        return;
            //    }
            //    textBox2.Text += value;
            //}

            //public void AppendTextBox3(string value, bool InvokeRequired, object Invoke, TextBox textBox3)
            //{
            //    if (InvokeRequired)
            //    {
            //        this.Invoke(new Action<string>(AppendTextBox3), new object[] { value });
            //        return;
            //    }
            //    textBox3.Text += value;
            //}

            //public void AppendTextBox4(string value, bool InvokeRequired, object Invoke, TextBox textBox4)
            //{
            //    if (InvokeRequired)
            //    {
            //        this.Invoke(new Action<string>(AppendTextBox4), new object[] { value });
            //        return;
            //    }
            //    textBox4.Text += value;
            //}
        }


        // }
        FlashPoint_Connection connection_1 = new FlashPoint_Connection();
        public void gmap_Load_1(object sender, EventArgs e)
        {
 
        }

        public void map_start(double x, double y)
        {
            // Initialize map:
            gmap.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            gmap.Position = new PointLatLng(x,y);
            gmap.Zoom = 15;

            gmap.ShowCenter = false; //don't display red cross in center of map

            gmap.Overlays.Clear();
            GMapOverlay markersOverlay = new GMapOverlay("markers");
            GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(x,y), GMarkerGoogleType.red_pushpin);
            markersOverlay.Markers.Add(marker);
            gmap.Overlays.Add(markersOverlay);

        }


        private void xButton1_Click_1(object sender, EventArgs e)
        {
            //Call function to open serial communication 
            backgroundWorker1.RunWorkerAsync();

            //Read();
        }
       
        private void xButton2_Click(object sender, EventArgs e)
        {
            
        }

        private void xButton3_Click(object sender, EventArgs e)
        {
          
        }

        private void xButton4_Click(object sender, EventArgs e)
        {
           
        }

        private void xButton5_Click(object sender, EventArgs e)
        {
           
        }

        private void xButton6_Click(object sender, EventArgs e)
        {
            flag_connect = false;
            try
            {
                //SerialPort();
                connection_1.Close();
                //Set textboxes back to null
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";

                //this.Close();
            }
            catch(Exception exc)
            {
                MessageBox.Show("Something went wrong with your disconnect button:\n\n" + exc + "\n\nPlease press OK and try again.");
            }
             
            //flag_connect = false;

            //SerialPort();
            //port.Close();

            //Set textboxes back to null
            //textBox1.Text = "";
           // textBox2.Text = "";
          //  textBox3.Text = "";
          //  textBox4.Text = "";
            
          //  Close();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            connection_1.Connect(flag_connect, xButton1);

            while (connection_1.port.IsOpen)
            {
                connection_1.Read(textBox1, textBox2, textBox3, textBox4);
                try
                {
                    map_start(connection_1.latitude, connection_1.longitude);
                    
                }
                catch (Exception)
                {

                }
                
            }
        }
        public void AppendTextBox1(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendTextBox1), new object[] { value });
                return;
            }
            textBox1.Text += value;
        }
        public void AppendTextBox2(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendTextBox2), new object[] { value });
                return;
            }
            textBox2.Text += value;
        }

        public void AppendTextBox3(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendTextBox3), new object[] { value });
                return;
            }
            textBox3.Text += value;
        }

        public void AppendTextBox4(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendTextBox4), new object[] { value });
                return;
            }
            textBox4.Text += value;
        }
        public void Maps(double x, double y)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendTextBox4), new object[] { x, y });
                return;
            }
            map_start(x, y);
        }

    }
}
