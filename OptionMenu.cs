using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ColorChooserCSharp;

namespace DoubleRainbow
{
    public partial class OptionMenu : Form
    {
        public OptionMenu()
        {
            InitializeComponent();
            strip_comboBox.SelectedIndex = 2;
            Rainbow.KaiEnabled = true;
            Rainbow.ZenEnabled = true;
        }
        const int KaiLength = Globals.KaiLength;

        private void button1_Click(object sender, EventArgs e)
        {
            new ColorChooser().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new RainbowGenerator(new HueGenerator(127.0f, 55.0f, 127.0f)).Show();
        }

        private void OptionMenu_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            RainbowUtils.TurnOff();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new MoodSelector().Show();
        }

        private void strip_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            String selected = strip_comboBox.SelectedItem.ToString();
            if (selected.Equals("Both"))
            {
                Rainbow.KaiEnabled = true;
                Rainbow.ZenEnabled = true;
            }
            else if (selected.Equals("Kai"))
            {
                Rainbow.KaiEnabled = true;
                Rainbow.ZenEnabled = false;
            }
            else if (selected.Equals("Zen"))
            {
                Rainbow.KaiEnabled = false;
                Rainbow.ZenEnabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Workshop.Demo();
        }
    }
}
