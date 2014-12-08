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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _chooser = new ColorChooser();
            _chooser.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_chooser == null) return;
            ColorTypes.RGB clr = new ColorTypes.RGB(_chooser.ActiveColor);
            if (clr == null) return;

            _moodSeq.addColor(clr);
            this.Invalidate();
        }


    }
}
