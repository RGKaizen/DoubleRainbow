using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using ColorChooserCSharp;
using System.IO;

namespace DoubleRainbow
{

    /*
     * Requirements:
     * Event-driven lighting. Run scripts for animations by accepting requests or events
     * events can be raised externally with custom scripts
     * Structure: source pixel, active width (left and right independent), animation mask
     * */

    public partial class RainbowGenerator : Form
    {
        Stopwatch s = new Stopwatch();
        const int KaiLength = Globals.KaiLength;

        public RainbowGenerator()
        {
            InitializeComponent();

        }

        // Play Action
        private Boolean repeater(object sender, EventArgs e)
        {
            HueGenerator huey = new HueGenerator(127.0f, 55.0f, 127.0f);
            s.Start();
            while (isOn)
            {
                Animate(huey);
            }
            s.Stop();
            return true;
        }

        public void Animate(HueGenerator huey)
        {

            // Color Generation 
            ColorTypes.HSV color_gen = huey.incrementCylinderSpace((float)Slider1Value/100, (float)Slider2Value/100, (float)Slider3Value/100);
            ColorTypes.RGB new_color = new ColorTypes.RGB(color_gen);

            // Position Generation
            Rainbow.Kai[23] = new_color;
            Rainbow.Kai[24] = new_color;
            Push();
            
            /*
            for (int i = 0; i < Globals.KaiLength; i++)
            {
                Rainbow.Kai[i] = myRGB;
            }
             */

            RefreshKai();
            Rainbow.BuddySystem();
        }

        // Pushes from the center like this <-- -->
        public void Push()
        {
            for (int i = 0; i < 23; i++)
            {
                Rainbow.Kai[i] = Rainbow.Kai[i + 1];
            }
            for (int i = 47; i > 24; i--)
            {
                Rainbow.Kai[i] = Rainbow.Kai[i - 1];
            }
        }

        // Use this to send commands to Kai!
        public void RefreshKai()
        {
            if (Rainbow.KaiUpdate())
                Rainbow.KaiShow();
            Thread.Sleep(refreshRate);
        }

        // Use this to send commands to Zen!
        public void RefreshZen()
        {
            if (Rainbow.ZenUpdate())
                Rainbow.ZenShow();
            Thread.Sleep(refreshRate);
        }

        // Starts/stops repeat thread
        Boolean isOn = false;
        private void PlayPause(object sender, EventArgs e)
        {
            if (isOn)
            {
                this.animWorker.CancelAsync();
                this.button2.Text = "Play";
                StoreState();
            }

            if (!isOn && !this.animWorker.IsBusy)
            {
                this.button2.Text = "Pause";
                this.animWorker.RunWorkerAsync(10);
            }

            isOn = !isOn;

        }

        private void ClearButton(object sender, EventArgs e)
        {
            RainbowUtils.TurnOff();
        }

        private void animWorker_DoWork(object sender, DoWorkEventArgs e)
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

        public double Slider1Value = 65;
        private void Slider1_Scroll(object sender, ScrollEventArgs e)
        {
            Slider1Value = Slider1.Value;
            Slider1ValueLbl.Text = "Val: " + Slider1Value;
        }

        public double Slider2Value = 45;
        private void Slider2_Scroll(object sender, ScrollEventArgs e)
        {
            Slider2Value = Slider2.Value;
            Slider2ValueLbl.Text = "Val: " + Slider2Value;
        }

        public double Slider3Value = 34;
        private void Slider3_Scroll(object sender, ScrollEventArgs e)
        {
            Slider3Value = Slider3.Value;
            Slider3ValueLbl.Text = "Val: " + Slider3Value;
        }

        public int refreshRate = 0;
        private void RefreshBar_Scroll(object sender, ScrollEventArgs e)
        {
            refreshRate = RefreshBar.Value;
            refreshLbl.Text = refreshRate + " ms";
            //DimLights(refreshRate);
        }

        private void DimLights(int val)
        {
            for(int i = 0; i < KaiLength; i++)
            {
                Rainbow.Kai[i] = AdjustVal(storedState[i], (val / (double)100));
            }
            RefreshKai();

        }

        ColorTypes.RGB[] storedState = new ColorTypes.RGB[KaiLength];
        private void StoreState()
        {
            Array.Copy(Rainbow.Kai, storedState, KaiLength);
        }

        private ColorTypes.RGB AdjustVal(ColorTypes.RGB rgb, double perc)
        {
            ColorTypes.HSV hsv = new ColorTypes.HSV(rgb);
            hsv.Value = (int)(hsv.Value * perc);
            return new ColorTypes.RGB(hsv);
        }

    }
}
