using System;
//using System.Collections.Generic;
using System.ComponentModel;
//using System.Data;
using System.Drawing;
//using System.Text;
using System.Windows.Forms;
using CPI.Plot3D;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;


namespace DoubleRainbow
{
    public partial class Spiral : Panel
    {

        Graphics g = null;
        CPI.Plot3D.Plotter3D p = null;
        public Spiral()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            g = this.CreateGraphics();
            p = new CPI.Plot3D.Plotter3D(g);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            p.PenWidth = 4;
        }

        //500
        //580
        //580

        public int centerX = 580;
        public int centerY = 580;
        public int centerZ = 350;
        const int max_radius = 256;

        public void updateCenters(int x, int y, int z)
        {
            centerX = x;
            centerY = y;
            centerZ = z;
        }

        public bool isOn = false;
        public void Draw_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
        }

        public void DrawFunction()
        {
            if (isOn)
            {
                this.DrawWorker.CancelAsync();
                this.btnDrawSpiral.Text = "Play";
            }

            if (!isOn && !this.DrawWorker.IsBusy)
            {
                this.btnDrawSpiral.Text = "Pause";
                this.DrawWorker.RunWorkerAsync(10);
            }

            isOn = !isOn;
        }

        public void GraphCylinder()
        {
            g.Clear(Color.White);
            isOn = false;
            using (CPI.Plot3D.Plotter3D p = new CPI.Plot3D.Plotter3D(g))
            {
                p.PenWidth = 1;
                //Draw radius_norm
                for (int i = 0; i < 360; i += 8)
                {
                        double x = max_radius * Math.Cos(i * Math.PI / 180);
                        double y = max_radius * Math.Sin(i * Math.PI / 180);
                        //Console.WriteLine("Hue: " + angle + " Math.Cos " + Math.Cos(radians));
                        double z = 0.0;
                        int converted_omega = (int)i * 256 / 360;


                        float cx = centerX + (float)x;
                        float cz = centerY + (float)z;
                        float cy = centerZ + (float)y;
                        p.PenUp();
                        p.PenColor = ColorTypes.HSVtoColor(converted_omega, (int)max_radius, (int)(256 - z));
                        p.Location = new Point3D(centerY, centerZ, centerX);
                        p.PenDown();
                        p.MoveTo(new Point3D(cy, cz, cx));
                        for (int j = 0; j < 256; j += 8)
                        {                           
                            p.MoveTo(new Point3D(cy, cz+j, cx));
                        }
                }
            }
        }

        public ColorTypes.CylndrCoords curCoord = null;

        public bool tripWire = true;
        // This does the math in converting the public-facing variable curCoords into xyz positions (with an offset so its centered on the screen)
        // and draws them 
        // Local variables prevent the results from changing while curCoord is changing
        public void GraphColors()
        {
            double x, y, z;
            double radius_norm;
            double height;
            double radians;
            try
            {
               radius_norm = curCoord.radius / 256;
               height = curCoord.height;
               radians = ((curCoord.angle % 360) * Math.PI / 180);
            }
            catch (Exception e)
            {
                Console.WriteLine("This failed, oh well");
                return;
            }

            x = (radius_norm * max_radius) * Math.Cos(radians);
            y = (radius_norm * max_radius) * Math.Sin(radians);
            z = 256 - height;
            int converted_omega = (int)curCoord.angle * 256 / 360;


            float cx = centerX + (float)x;
            float cz = centerY + (float)z;
            float cy = centerZ + (float)y;

            p.PenColor = ColorTypes.HSVtoColor(converted_omega, (int)max_radius, (int)height);
            if (tripWire)
            {
                tripWire = false;
                p.Location = new CPI.Plot3D.Point3D(cy, cz, cx);
            }

            p.MoveTo(new Point3D(cy,cz,cx));

        }

        private void DrawCyl_Click(object sender, EventArgs e)
        {
            GraphCylinder();
        }

        private void DrawWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Do not access the form's BackgroundWorker reference directly. 
            // Instead, use the reference provided by the sender parameter.
            BackgroundWorker bw = sender as BackgroundWorker;
            // Start the time-consuming operation.
            e.Result = repeater(bw, e);

            // If the operation was canceled by the user,  
            // set the DoWorkEventArgs.Cancel property to true. 
            if (bw.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        private Boolean repeater(object sender, EventArgs e)
        {
            while (isOn)
            {
                GraphColors();
            }
            return true;
        }
    }
}