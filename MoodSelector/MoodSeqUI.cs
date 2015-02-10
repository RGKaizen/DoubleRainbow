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
        public MoodSeq _moodSeq;

        public MoodSeqUI()
        {
            _moodSeq = new MoodSeq();
        }

        public void clearColors()
        {
            _moodSeq.Color_List.Clear();
            this.Invalidate();
        }

        public List<ColorTypes.RGB> getColors()
        {
            return _moodSeq.Color_List;
        }

        public void addColor(ColorTypes.RGB rgb)
        {
            if (_moodSeq.Color_List.Count == 10)
            {
                _moodSeq.Color_List.RemoveAt(0);
            }
            _moodSeq.Color_List.Add(rgb);
        }


        protected override void OnPaint(PaintEventArgs pe)
        {
            // Call the OnPaint method of the base class.
            base.OnPaint(pe);

            if (_moodSeq.Color_List.Count != 0)
            {

                Graphics g = pe.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                Size rect_size = new Size(this.ClientSize.Width / _moodSeq.Color_List.Count, this.ClientSize.Height);
                int count = 0;
                foreach (ColorTypes.RGB rgb in _moodSeq.Color_List)
                {
                    Point p = new Point(count * rect_size.Width, 0);
                    SolidBrush myBrush = new SolidBrush(ColorTypes.RGBtoColor(rgb));
                    g.FillEllipse(myBrush, new Rectangle(p, rect_size));
                    myBrush.Dispose();             
                    count++;
                }
            }
        }
    }
}
