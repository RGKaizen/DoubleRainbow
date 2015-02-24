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
        BindingList<MoodSeq> moods_list;
        public MoodSelector()
        {
            InitializeComponent();
            InitMoodSeq();
            _at = new AnimationThread(Animation);           
            if(File.Exists(@"moods.txt"))
                moods_list = JsonConvert.DeserializeObject<BindingList<MoodSeq>>(File.ReadAllText(@"moods.txt"));
            else
                moods_list = new BindingList<MoodSeq>();

            moodsBindingSource.DataSource = moods_list;
            moodsCmbBox.DataSource = moodsBindingSource.DataSource;
            moodsCmbBox.SelectedIndex = -1;
        }

        // Serialize list on close
        private void MoodSelector_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            List<MoodSeq> json_list = new List<MoodSeq>();
            foreach (MoodSeq moodSeq in moodsCmbBox.Items)
            {
                json_list.Add(moodSeq);
            }

            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(@"moods.txt"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, json_list);
            }
        }

        private MoodSeqUI moodSeqUI_obj;
        private MoodSeq currentMood;
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
            currentMood = new MoodSeq();
            this.moodSeqUI_obj.Mood = currentMood;
        }

        // Choose Color
        private void colorChsBtn_Click(object sender, EventArgs e)
        {
          if(_chooser == null)
          { 
            _chooser = new ColorChooser();
            _chooser.Show();
          }
        }

        // Set Color
        private void addClrBtn_Click(object sender, EventArgs e)
        {
            if (_chooser == null) return;
            ColorTypes.RGB clr = new ColorTypes.RGB(_chooser.ActiveColor);
            if (clr == null) return;

            currentMood.Color_List.Add(clr);
            moodSeqUI_obj.Mood = currentMood;
            this.Invalidate();
        }

        private void addMoodbtn_Click(object sender, EventArgs e)
        {
            moods_list.Add(currentMood);
        }

        private void removeMoodBtn_Click(object sender, EventArgs e)
        {
            moods_list.Remove((MoodSeq)moodsCmbBox.SelectedItem);
        }

        private void newMood_Click(object sender, EventArgs e)
        {
            currentMood = new MoodSeq();
            DlgNameChooser dlg = new DlgNameChooser();
            DialogResult result = dlg.ShowDialog();
            if (result == DialogResult.OK)
                currentMood.Name = dlg.name;
            else
                return;
            moodSeqUI_obj.Mood = currentMood;
            moodNameLbl.Text = "Mood Name: " + currentMood.Name;

        }

        private void moodList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (moodsCmbBox.SelectedItem != null)
            {
                currentMood = (MoodSeq)moodsCmbBox.SelectedItem;
                moodSeqUI_obj.Mood = currentMood;
                moodSeqUI_obj.Invalidate();
                this.Invalidate();
            }
        }


        #region Animation
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
            for (int i = 0; i < color_list.Count; i++)
            {
                ColorTypes.RGB cur = color_list[i];
                ColorTypes.RGB next;
                if (i + 1 == color_list.Count)
                    next = color_list[0];
                else
                    next = color_list[i + 1];

                double j = 1.0;
                while (j > 0.0)
                {
                    ColorTypes.RGB show_color = Blender.variableMix(cur, next, j);
                    RainbowUtils.fillBoth(show_color);
                    Thread.Sleep(speed);
                    if (halt) return;
                    j -= 0.05;
                }

            }
        }
        #endregion



    }
}
