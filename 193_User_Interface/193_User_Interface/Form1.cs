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
        private SerialPort port;
        private bool flag_connect = true;

        public Form1()
        {
            InitializeComponent();        
          //  SerialPort(); //call function for serial port control 
          //  Read(); //call to function for reading in serial data 
                     
            FormBorderStyle = FormBorderStyle.None;  
            WindowState = FormWindowState.Maximized;    //Maximize the form window

        }

        private void SerialPort()
        {
            //Initialize com port
            port = new SerialPort("COM5", 9600, Parity.None, 8, StopBits.One);

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
                xButton1.Text = "Connected";
                xButton1.Theme = ManiXButton.Theme.MSOffice2010_Green;

                //Read();
                
                Task.Run(() =>
                {
                    while (port.IsOpen)
                    {
                        Read();
                    }
                    
                }); 
                    

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

        private void Read()
        {
            long Inlatitude;
            long Inlongitude;
            double latitude;
            double longitude;
            int Heart_Rate;
            string Date_Time;

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
                    textBox1.Text = ("" + latitude);
                    textBox2.Text = ("" + longitude);
                    //Location_ShowAll_Tab.Text = ("Lat:" + latitude + " Lon:" + longitude);
                    map_start(latitude, longitude);              
                }
                catch (FormatException)
                {
                    // Location_ShowAll_Tab.Text = ("Format Issue!!!!");
                    textBox1.Text = ("Format Error");
                    textBox2.Text = ("Format Error");
                }

                if (words[2] == null)
                {
                    Heart_Rate = 0; //set default heart rate
                    textBox3.Text = ("Set to default");
                    // Heart_Rate_ShowAll_Tab.Text = ("Set to default");
                }
                else
                {
                    try
                    {
                        Heart_Rate = Convert.ToInt32(words[2]);    // Convert String into int
                        textBox3.Text = ("" + Heart_Rate);
                    }
                    catch (FormatException)
                    {
                        textBox3.Text = ("Format Error");
                    }
                }
                if (words[3] == null)
                {
                    textBox3.Text = ("Not available");
                }
                else
                {
                    try
                    {
                        Date_Time = words[3]; //  Keeps string as a string
                        textBox4.Text = ("" + Date_Time);
                    }
                    catch (FormatException)
                    {
                        textBox4.Text = ("Format Error");
                    }
                }
            }
            else
            {
                //set default lat/ long - csus
                latitude = 38.556868;
                longitude = -121.358592;
                map_start(latitude, longitude);
            }
        }

       // }

        private void gmap_Load_1(object sender, EventArgs e)
        {
 
        }

        private void map_start(double x, double y)
        {
            // Initialize map:
            gmap.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
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
            SerialPort();

            //Read();
        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {                      

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

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
            Task.Run(() =>
            {
                flag_connect = false;
                try
                {
                    //SerialPort();
                    port.Close();
                    //Set textboxes back to null
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";

                    this.Close();
                }
                catch(Exception exc)
                {
                    MessageBox.Show("Something went wrong with your disconnect button:\n\n" + exc + "\n\nPlease press OK and try again.");
                }
              
            });
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
    }
}
