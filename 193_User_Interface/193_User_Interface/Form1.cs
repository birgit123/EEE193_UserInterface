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

namespace _193_User_Interface
{
    public partial class Form1 : Form
    {
        //private SerialPort port;

        public Form1()
        {
            InitializeComponent();
           // SerialPort(); //call function for serial port control 
            //Read(); //call to function for reading in serial data 
                     
            FormBorderStyle = FormBorderStyle.None;  
            WindowState = FormWindowState.Maximized;    //Maximize the form window

        }

       // private void SerialPort()
       // {
       //     port = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);

            //try
           // {


           // }



       // }
            
        private void gmap_Load_1(object sender, EventArgs e)
        {
            // Initialize map:
            gmap.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            gmap.Position = new PointLatLng(38.5602, -121.4241);
            gmap.Zoom = 15;

            gmap.ShowCenter = false; //don't display red cross in center of map

            gmap.ShowCenter = false;

            //gmap.Overlays.Clear();
            GMapOverlay markersOverlay = new GMapOverlay("markers");
            GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(38.5602, -121.4241), GMarkerGoogleType.red_pushpin);
            markersOverlay.Markers.Add(marker);
            gmap.Overlays.Add(markersOverlay);
        }

        private void xButton1_Click_1(object sender, EventArgs e)
        {
            

            if (xButton1.Text == "Not Connected")
            {
                xButton1.Text = "Connected";
                xButton1.Theme = ManiXButton.Theme.MSOffice2010_Green;
            }          
           else if(xButton1.Text == "Connected")
            {
                xButton1.Text = "Not Connected";
                xButton1.Theme = ManiXButton.Theme.MSOffice2010_RED;          
            }

        }
    }
}
