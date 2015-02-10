using ColorChooserCSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DoubleRainbow
{
    public partial class MoodSelector : Form
    {
        ColorChooser _chooser;
        public MoodSelector()
        {
            InitializeComponent();
            InitMoodSeq();
            _at = new AnimationThread(Animation);
        }

        private MoodSeqUI moodSeqUI_obj;
        private void InitMoodSeq()
        {
            this.moodSeqUI_obj = new DoubleRainbow.MoodSeqUI();
            this.moodSeqUI_obj.BackColor = System.Drawing.Color.White;
            this.moodSeqUI_obj.Location = new System.Drawing.Point(12, 50);
            this.moodSeqUI_obj.Name = "_moodSeq";
            this.moodSeqUI_obj.Size = new System.Drawing.Size(260, 26);
            this.moodSeqUI_obj.TabIndex = 2;
            this.Controls.Add(this.moodSeqUI_obj);
            moodSeqUI_obj.clearColors();
        }

        // Choose Color
        private void colorChsBtn_Click(object sender, EventArgs e)
        {
            _chooser = new ColorChooser();
            _chooser.Show();
        }

        // Set Color
        private void addClrBtn_Click(object sender, EventArgs e)
        {
            if (_chooser == null) return;
            ColorTypes.RGB clr = new ColorTypes.RGB(_chooser.ActiveColor);
            if (clr == null) return;

            moodSeqUI_obj.addColor(clr);
            moodSeqUI_obj.Invalidate();
            this.Invalidate();
        }


        // Animate Strip
        AnimationThread _at = null;
        // Starts/stops repeat thread
        private void PlayPause(object sender, EventArgs e)
        {
            if (_at.isOn)
            {
                this.animateBtn.Text = "Play";
                _at.Stop();
                halt = true;
            }
            else
            {
                this.animateBtn.Text = "Pause";
                _at.Start();
                halt = false;
            }
        }
    
        int speed = 100;
        Boolean halt = false;
        public void Animation()
        {
            List<ColorTypes.RGB> color_list = moodSeqUI_obj.getColors();
            for(int i = 0; i < color_list.Count; i++)
            {
                ColorTypes.RGB cur = color_list[i];
                ColorTypes.RGB next;
                if(i+1 == color_list.Count)
                    next = color_list[0];
                else
                    next = color_list[i + 1];

                double j = 1.0;
                while(j > 0.0)
                { 
                    ColorTypes.RGB show_color = Blender.variableMix(cur, next, j);
                    RainbowUtils.fillBoth(show_color);
                    Thread.Sleep(speed);
                    if (halt) return;
                    j-=0.05;
                }

            }
        }

        private void addMoodbtn_Click(object sender, EventArgs e)
        {
            DlgNameChooser dlg = new DlgNameChooser();
            DialogResult result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                moodSeqUI_obj._moodSeq.Name = dlg.name;
            }

            moodList.Items.Add(moodSeqUI_obj._moodSeq);
        }

        private void removeMoodBtn_Click(object sender, EventArgs e)
        {
            moodList.Items.Remove(moodList.SelectedItem);
        }

        private void moodList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (moodList.SelectedItem != null)
            {
                moodSeqUI_obj._moodSeq = (MoodSeq)moodList.SelectedItem;
                moodSeqUI_obj.Invalidate();
                this.Invalidate();
            }
        }

        // Serialize list on close
        private void MoodSelector_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(@"moods.txt"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {    
                foreach(MoodSeq moodSeq in moodList.Items)
                {
                    serializer.Serialize(writer, moodSeq);
                }
            }
        }

    }
}
