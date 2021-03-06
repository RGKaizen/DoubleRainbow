﻿using System;
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
using DoubleRainbow.Interfaces;

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
        IHueGenerator _huey = null;
        AnimationThread _at = null;

        public RainbowGenerator(IHueGenerator huey)
        {
            InitializeComponent();
            _huey = huey;
            _at = new AnimationThread(Animate);
        }

        public void Animate()
        {

            // Color Generation 
            DRColor.HSV color_gen = _huey.getNextColor((float)Slider1Value, (float)Slider2Value, (float)Slider3Value);
            DRColor.RGB new_color = new DRColor.RGB(color_gen);

            // Position Generation
            Rainbow.Kai[23] = new_color;
            Rainbow.Kai[24] = new_color;
            Push();
            RainbowUtils.update();
            Thread.Sleep(refreshRate);
        }

        // Pushes from the center like this --> <--
        public void Push()
        {
            double mid = 23.5;
            for (int i = 0; i < 23; i++)
            {
                int wave_up = 0+i;//(int)(Math.Ceiling(mid + i));
                int wave_down = 47-i;//(int)(Math.Floor(mid - i));
                Rainbow.Kai[wave_up] = Rainbow.Kai[wave_up + 1];
                Rainbow.Kai[wave_down] = Rainbow.Kai[wave_down - 1];
            }
        }

        // Starts/stops repeat thread
        private void PlayPause(object sender, EventArgs e)
        {
            if (_at.isOn)
            {
                this.button2.Text = "Play";
                _at.Stop();
            }
            else
            {
                this.button2.Text = "Pause";
                _at.Start();
                StoreState();
            }
        }

        private void ClearButton(object sender, EventArgs e)
        {
            RainbowUtils.TurnOff();
        }

        public double Slider1Value = 61;
        private void Slider1_Scroll(object sender, ScrollEventArgs e)
        {
            Slider1Value = Math.Pow(Math.E, (5.545d * Slider1.Value / 1000) );
            Slider1ValueLbl.Text = "Val: " + String.Format("{0:N2}", Slider1Value);
        }

        public double Slider2Value = 45;
        private void Slider2_Scroll(object sender, ScrollEventArgs e)
        {
            Slider2Value = Math.Pow(Math.E, (5.545d * Slider2.Value / 1000) );
            Slider2ValueLbl.Text = "Val: " + String.Format("{0:N2}", Slider2Value);
        }

        public double Slider3Value = 34;
        private void Slider3_Scroll(object sender, ScrollEventArgs e)
        {
            Slider3Value = Math.Pow(Math.E, (5.545d * Slider3.Value / 1000));
            Slider3ValueLbl.Text = "Val: " + String.Format("{0:N2}", Slider3Value);
        }

        public int refreshRate = 0;
        private void RefreshBar_Scroll(object sender, ScrollEventArgs e)
        {
            refreshRate = RefreshBar.Value;
            refreshLbl.Text = refreshRate + " ms";
        }

        DRColor.RGB[] storedState = new DRColor.RGB[KaiLength];
        private void StoreState()
        {
            Array.Copy(Rainbow.Kai, storedState, KaiLength);
        }

        private DRColor.RGB AdjustVal(DRColor.RGB rgb, double perc)
        {
            DRColor.HSV hsv = new DRColor.HSV(rgb);
            hsv.Value = (int)(hsv.Value * perc);
            return new DRColor.RGB(hsv);
        }

    }
}
