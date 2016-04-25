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
        public SerialPort port;
        private bool flag_connect = true;
        double latitude;
        double longitude;
        int Heart_Rate;
        string Date_Time;
        String str_lat = "";
        String str_lon = "";
        String str_heart_rate = "";
        String str_date = "";
        String[] arr_data = new String[4];

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
            port = new SerialPort("COM9", 9600, Parity.None, 8, StopBits.One);

            try
            {
                flag_connect = true;
                
                port.Open();
                //Set button #1's status and color 
                xButton1.Text = "Connected";
                xButton1.Theme = ManiXButton.Theme.MSOffice2010_Green;
            }
            catch (IOException)
            {
                port.Close();
                MessageBox.Show("Connection could not be established. Please try again.");             
                //Application.Exit();
                //Close();
            }
         }

        public static String[] Read(SerialPort port)
        {
            long Inlatitude;
            long Inlongitude;
            string strLatitude = "";
            string strLongitude = "";
            double latitude;
            double longitude;
            int Heart_Rate;
            bool flag_badData = false;
            string Date_Time;
            string[] words = {""};
            string data = "";

            do
            {
                flag_badData = false;
                data = port.ReadTo("]");
                words = data.Split(',');
                if (words.Length != 4 || words[0].Length < 8 || words[1].Length < 10 || words[2].Length > 3)
                {
                    flag_badData = true;
                }
                else
                {
                    strLatitude = words[0].Remove(0, words[0].Length - 8);
                    strLongitude = words[1].Remove(0, words[1].Length - 10);
                    if (!IsAllDigits(strLatitude) || !IsAllDigits(strLongitude) || !IsAllDigits(words[2]))
                    {
                        flag_badData = true;
                    }
                }
                
            } while ( flag_badData || Int64.Parse(strLatitude) > 90000000 || Int64.Parse(strLatitude) < -90000000 || Int64.Parse(strLongitude) < -180000000 || Int64.Parse(strLongitude) > 180000000);
            // lat = 90 long = 180
            if (words[0] != null && words[1] != null)
            {
                try
                {
                    Inlatitude = Int64.Parse(strLatitude);
                    latitude = (Inlatitude / 1000000.0);
                    Inlongitude = Int64.Parse(strLongitude);
                    longitude = (Inlongitude / 1000000.0);
                    words[0] = ("" + latitude);
                    words[1] = ("" + longitude);
                    //Location_ShowAll_Tab.Text = ("Lat:" + latitude + " Lon:" + longitude);            
                }
                catch (FormatException)
                {
                    // Location_ShowAll_Tab.Text = ("Format Issue!!!!");
                    latitude = 38.556868;
                    words[0] = "38.556868";
                    longitude = -121.358592;
                    words[1] = "-121.358592";
                    Read(port);
                }

                if (words[2] == null)
                {
                    Heart_Rate = 0; //set default heart rate
                    words[2] = ("N/A");
                    // Heart_Rate_ShowAll_Tab.Text = ("Set to default");
                }
                else
                {
                    try
                    {
                        Heart_Rate = Convert.ToInt32(words[2]);    // Convert String into int
                        words[2] = ("" + Heart_Rate);
                    }
                    catch (FormatException)
                    {
                        words[2] = ("Format Error");
                    }
                }
                if (words[3] == null)
                {
                    words[3] = ("Not available");
                }
                else
                {
                    try
                    {
                        Date_Time = words[3]; //  Keeps string as a string
                        words[3] = ("" + Date_Time);
                    }
                    catch (FormatException)
                    {
                        words[3] = ("Format Error");
                    }
                }
            }
            else
            {
                //set default lat/ long - csus
                latitude = 38.556868;
                words[0] = "38.556868";
                longitude = -121.358592;
                words[1] = "-121.358592";
                Read(port);
            }
            return words;
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
            gmap.Zoom = 35;

            gmap.ShowCenter = false; //don't display red cross in center of map

            gmap.Overlays.Clear();
            GMapOverlay markersOverlay = new GMapOverlay("markers");
            GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(x,y), GMarkerGoogleType.red_pushpin);
            markersOverlay.Markers.Add(marker);
            gmap.Overlays.Add(markersOverlay);

        }


        private async void xButton1_Click_1(object sender, EventArgs e)
        {
            //Call function to open serial communication 
            SerialPort();
            new Thread(new ThreadStart(BackGround_Data)).Start();

            Refresh_Buttons();
            Refresh_Text();

            while (flag_connect)
            {
                if (latitude == 0 || longitude == 0)
                {
                    Thread.Sleep(1000);
                    continue;
                }
                if (!flag_connect)
                {
                    Thread.CurrentThread.Abort();
                }
                else
                {
                    arr_data = Read(port);

                    latitude = Convert.ToDouble(arr_data[0]);
                    //latitude = double.Parse(arr_data[0]);
                    longitude = Convert.ToDouble(arr_data[1]);
                    //longitude = double.Parse(arr_data[1]);
                    Heart_Rate = Convert.ToInt32(arr_data[2]);
                    //Heart_Rate = Int32.Parse(arr_data[2]);
                    str_lat = arr_data[0];
                    str_lon = arr_data[1];
                    str_heart_rate = arr_data[2];
                    str_date = arr_data[3];
                }

                Thread.Sleep(800);
                map_start(latitude, longitude);
                Port_Text_Data();
                Refresh_Buttons();
                Refresh_Text();
                //Thread.Sleep(800);
                await PutTaskDelay();
            }

            //Read();
        }

        private void Port_Text_Data()
        {
            textBox1.Text = str_lat;
            textBox2.Text = str_lon;
            textBox3.Text = str_heart_rate;
            textBox4.Text = str_date;

        }

        private void Refresh_Text()
        {
            textBox1.Refresh();
            textBox2.Refresh();
            textBox3.Refresh();
            textBox4.Refresh();
        }

        private void Refresh_Buttons()
        {
            xButton1.Refresh();
            xButton2.Refresh();
            xButton3.Refresh();
            xButton4.Refresh();
            xButton6.Refresh();
        }

        private void xButton6_Click(object sender, EventArgs e)
        {
            flag_connect = false;
            port.Close();

            xButton1.Text = "Click to Connect";
            xButton1.Theme = ManiXButton.Theme.MSOffice2010_RED;
            Refresh_Buttons();
            //xButton1.PerformClick();

            //flag_connect = false;

            //SerialPort();
            //port.Close();

            //Set textboxes back to null
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            Refresh_Text();

            //Close();
        }
        public void BackGround_Data()
        {
            Thread.Sleep(10);
            if (flag_connect)
            {
                arr_data = Read(port);
                latitude = Convert.ToDouble(arr_data[0]);
                //latitude = double.Parse(arr_data[0]);
                longitude = Convert.ToDouble(arr_data[1]);
                //longitude = double.Parse(arr_data[1]);
                Heart_Rate = Convert.ToInt32(arr_data[2]);
                //Heart_Rate = Int32.Parse(arr_data[2]);
                str_lat = arr_data[0];
                str_lon = arr_data[1];
                str_heart_rate = arr_data[2];
            }
            else
            {
                Thread.CurrentThread.Abort();
            }
        }

        private void splitContainer3_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void xButton7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }


   
        static bool IsAllDigits(string s)
        {
            foreach (char c in s)
            {
                if (!char.IsDigit(c) && !c.Equals('-'))
                    return false;
            }
            return true;
        }

        async Task PutTaskDelay()
        {
            await Task.Delay(5000);
        }


    }

}
