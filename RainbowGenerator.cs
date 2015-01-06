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
        const int KaiLength = Globals.KaiLength;
        HueGenerator _huey = null;
        AnimationThread _at = null;

        public RainbowGenerator()
        {
            InitializeComponent();
            _huey = new HueGenerator(127.0f, 55.0f, 127.0f);

        }

        public void Animate()
        {

            // Color Generation 
            ColorTypes.HSV color_gen = _huey.incrementCylinderSpace((float)Slider1Value/100, (float)Slider2Value/100, (float)Slider3Value/100);
            ColorTypes.RGB new_color = new ColorTypes.RGB(color_gen);

            // Position Generation
            Rainbow.Kai[23] = new_color;
            Rainbow.Kai[24] = new_color;
            Push();

            Rainbow.KaiShow();
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

        // Starts/stops repeat thread
        private void PlayPause(object sender, EventArgs e)
        {
            if (_at.Stop())
            {
                this.button2.Text = "Play";
                StoreState();
            }

            if (_at.Start())
            {
                this.button2.Text = "Pause";
            }
        }

        private void ClearButton(object sender, EventArgs e)
        {
            RainbowUtils.TurnOff();
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
