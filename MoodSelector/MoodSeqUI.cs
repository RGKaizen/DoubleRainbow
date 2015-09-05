using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DoubleRainbow
{
    public partial class MoodSeqUI : UserControl
    {
        private MoodSeq mood;   // the name field
        public MoodSeq Mood   // the Name property
        {
            get
            {
                return mood;
            }
            set
            {
                mood = value;
                this.Invalidate();
            }
        }

        public MoodSeqUI()
        {
            mood = new MoodSeq();
        }

        public void clearColors()
        {
            mood.Color_List.Clear();
            this.Invalidate();
        }

        public List<DRColor.RGB> getColors()
        {
            return mood.Color_List;
        }

        public void addColor(DRColor.RGB rgb)
        {
            if (mood.Color_List.Count == 10)
            {
                mood.Color_List.RemoveAt(0);
            }
            mood.Color_List.Add(rgb);
        }


        protected override void OnPaint(PaintEventArgs pe)
        {
            // Call the OnPaint method of the base class.
            base.OnPaint(pe);

            if (mood.Color_List.Count != 0)
            {

                Graphics g = pe.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                Size rect_size = new Size(this.ClientSize.Width / mood.Color_List.Count, this.ClientSize.Height);
                int count = 0;
                foreach (DRColor.RGB rgb in mood.Color_List)
                {
                    Point p = new Point(count * rect_size.Width, 0);
                    SolidBrush myBrush = new SolidBrush(DRColor.RGBtoColor(RainbowUtils.increaseBrightness(rgb, 130)));
                    g.FillEllipse(myBrush, new Rectangle(p, rect_size));
                    myBrush.Dispose();             
                    count++;
                }
            }
        }
    }
}
