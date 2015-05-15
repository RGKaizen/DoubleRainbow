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
        private MoodSeqUI current_moodSeqUI;
        private MoodSeq currentMood;

        public MoodSelector()
        {
            InitializeComponent();
            InitMoodSeq();
            _at = new AnimationThread(Animation);           
            if(File.Exists(@"moods.txt"))
                moods_list = JsonConvert.DeserializeObject<BindingList<MoodSeq>>(File.ReadAllText(@"moods.txt"));
            else
                moods_list = new BindingList<MoodSeq>();

            // How to bind a list to a combobox bidirectionally
            moodsBindingSource.DataSource = moods_list;
            moodsCmbBox.DataSource = moodsBindingSource.DataSource;
            moodsCmbBox.SelectedIndex = -1;
        }

        private void InitMoodSeq()
        {
            this.current_moodSeqUI = new DoubleRainbow.MoodSeqUI();
            this.current_moodSeqUI.BackColor = System.Drawing.Color.White;
            this.current_moodSeqUI.Location = new System.Drawing.Point(12, 50);
            this.current_moodSeqUI.Name = "_moodSeq";
            this.current_moodSeqUI.Size = new System.Drawing.Size(260, 26);
            this.current_moodSeqUI.TabIndex = 2;
            this.Controls.Add(this.current_moodSeqUI);
            current_moodSeqUI.clearColors();
            currentMood = new MoodSeq();
            this.current_moodSeqUI.Mood = currentMood;
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
            current_moodSeqUI.Mood = currentMood;
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
            current_moodSeqUI.Mood = currentMood;
            moodNameLbl.Text = "Mood Name: " + currentMood.Name;

        }

        private void moodList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (moodsCmbBox.SelectedItem != null)
            {
                currentMood = (MoodSeq)moodsCmbBox.SelectedItem;
                current_moodSeqUI.Mood = currentMood;
                current_moodSeqUI.Invalidate();
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
            List<ColorTypes.RGB> color_list = current_moodSeqUI.getColors();
            for (int i = 0; i < color_list.Count; i++)
            {

                ColorTypes.RGB cur = color_list[i];
                ColorTypes.RGB next = color_list[(i+1)%color_list.Count];

                // Smoothening effect
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

        // Serialize list on close
        private void MoodSelector_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            // Generate a list of the current mood combobox state
            List<MoodSeq> json_list = new List<MoodSeq>();
            foreach (MoodSeq moodSeq in moodsCmbBox.Items)
            {
                json_list.Add(moodSeq);
            }

            // Serialize the list
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(@"moods.txt"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, json_list);
            }
        }

    }
}
