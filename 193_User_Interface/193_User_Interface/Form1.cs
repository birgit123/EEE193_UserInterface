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


namespace _193_User_Interface
{

    public partial class Form1 : Form
    {
        private SerialPort port;

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
            port = new SerialPort("COM5", 9600, Parity.None, 8, StopBits.One);

            try
            {
                port.Open();

               // xButton1.Text = "Connected";
               // xButton1.Theme = ManiXButton.Theme.MSOffice2010_Green;

                /*if (xButton1.Text == "Not Connected")
                {

                    xButton1.Text = "Connected";
                    xButton1.Theme = ManiXButton.Theme.MSOffice2010_RED;
                    port.Close();
                    //textBox1.Clear();


                }*/

                SerialPortDataReceived(port);
                Read();
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

                if (words[0] == null && words[1] == null)
                {
                    latitude = 0;
                    longitude = 0;
                   // Location_ShowAll_Tab.Text = ("Location set to default");
                }
                else if (words[0] != null && words[1] != null)
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
                        textBox1.Text = ("Format Issue!!!!");
                        textBox2.Text = ("Format Issue!!!!");
                    }
                }
                else if (words[0] != null && words[1] == null)
                {
                    latitude = 0;
                    longitude = 0;
                     // Location_ShowAll_Tab.Text = ("Location set to default");
                     map_start(latitude, longitude);
                }
                else if (words[0] == null && words[1] != null)
                {
                    latitude = 0;
                    longitude = 0;
                     // Location_ShowAll_Tab.Text = ("Location set to default");
                      map_start(latitude, longitude);
                }
                else
                {
                    latitude = 0;
                    longitude = 0;
                     //Location_ShowAll_Tab.Text = ("Location set to default");
                      map_start(latitude, longitude);
                }
                if (words[2] == null)
                {
                    Heart_Rate = 0;
                    textBox3.Text = ("Set to default");
                   // Heart_Rate_ShowAll_Tab.Text = ("Set to default");
                }
                else
                {
                    try
                    {
                        Heart_Rate = Convert.ToInt32(words[2]);    // Convert String into int
                        textBox3.Text = ("" + Heart_Rate);
                       // Heart_Rate_ShowAll_Tab.Text = ("" + Heart_Rate);
                    }
                    catch (FormatException)
                    {
                        textBox3.Text = ("Format Error");
                        //Heart_Rate_ShowAll_Tab.Text = ("Format Issue!!!!");
                    }
                }
                if (words[3] == null)
                {
                    //Date_Time = ("Not available");
                    textBox3.Text = ("Not available");
                    //Date_Time_ShowAll_Tab.Text = ("Not available");
                }
                else
                {
                    try
                    {
                        Date_Time = words[3];                  //  Keeps string as a string
                        textBox4.Text = ("" + Date_Time);
                       // Date_Time_ShowAll_Tab.Text = ("" + Date_Time);
                    }
                    catch (FormatException)
                    {
                        textBox4.Text = ("Format Error");
                      //  Date_Time_ShowAll_Tab.Text = ("Format Issue!!!!");
                    }
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

            gmap.ShowCenter = false;

            //gmap.Overlays.Clear();
            GMapOverlay markersOverlay = new GMapOverlay("markers");
            GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(x,y), GMarkerGoogleType.red_pushpin);
            markersOverlay.Markers.Add(marker);
            gmap.Overlays.Add(markersOverlay);


        }

        public void SerialPortDataReceived(SerialPort read) // Creates buffer to read data 
        {
            byte[] buf;
            buf = new byte[read.BytesToRead];
            read.Read(buf, 0, buf.Length);
            string result = Encoding.UTF8.GetString(buf);
        }

        private void xButton1_Click_1(object sender, EventArgs e)
        {

            SerialPort();
            xButton1.Text = "Connected";
            xButton1.Theme = ManiXButton.Theme.MSOffice2010_Green;
            //Read();

            /*if (xButton1.Text  == "Not Connected")
            {
                
                xButton1.Text = "Connected";
                xButton1.Theme = ManiXButton.Theme.MSOffice2010_RED;
                port.Close();
                //textBox1.Clear();


            }*/
            /*  if (xButton1.Text == "Not Connected")
            {
                xButton1.Text = "Connected";
                xButton1.Theme = ManiXButton.Theme.MSOffice2010_Green;
               
            }       
           else if(xButton1.Text == "Connected")
            {
                xButton1.Text = "Not Connected";
                xButton1.Theme = ManiXButton.Theme.MSOffice2010_RED;          
            }*/

        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
                Read();
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
          
                Read();
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
                Read();
            
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
                Read();
            
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
            port.Close();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            Close();

        }
    }
}
