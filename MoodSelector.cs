using ColorChooserCSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DoubleRainbow
{
    public partial class MoodSelector : Form
    {
        ColorChooser _chooser;
        public MoodSelector()
        {
            InitializeComponent();
            _moodSeq.clearColors();
            _at = new AnimationThread(Animation);
        }

        // Choose Color
        private void button1_Click(object sender, EventArgs e)
        {
            _chooser = new ColorChooser();
            _chooser.Show();
        }

        // Set Color
        private void button2_Click(object sender, EventArgs e)
        {
            if (_chooser == null) return;
            ColorTypes.RGB clr = new ColorTypes.RGB(_chooser.ActiveColor);
            if (clr == null) return;

            _moodSeq.addColor(clr);
            this.Invalidate();
        }


        // Animate Strip
        AnimationThread _at = null;
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
            }
        }
    

        public void Animation()
        {
            foreach (ColorTypes.RGB rgb in _moodSeq.getColors())
            {
                RainbowUtils.fillBoth(rgb);
            }
        }

        // Stop Animation
        private void button4_Click(object sender, EventArgs e)
        {
            if (!_at.Stop())
            {
                MessageBox.Show("I failed to stop it...");
            }
        }


    }
}
