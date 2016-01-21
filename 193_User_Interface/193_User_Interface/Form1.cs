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
        public Form1()
        {
            InitializeComponent();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

       /* private void gMapControl1_Load(object sender, EventArgs e)
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
        }*/

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer4_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer3_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer4_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gmap_Load(object sender, EventArgs e)
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

        private void xButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
