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
    public partial class MoodSeq : UserControl
    {

        private List<ColorTypes.RGB> _color_list = new List<ColorTypes.RGB>();

        public MoodSeq()
        {
            InitializeComponent();
            _color_list.Add(Colors.Red);
            _color_list.Add(Colors.Green);
        }

        public MoodSeq(ColorTypes.RGB rgb)
        {
            InitializeComponent();
            _color_list.Add(rgb);
        }

        public void addColor(ColorTypes.RGB rgb)
        {
            if (_color_list.Count == 10)
            {
                _color_list.RemoveAt(0);
            }
            _color_list.Add(rgb);
            this.Invalidate();
        }

        public void clearColors()
        {
            _color_list.Clear();
            this.Invalidate();
        }

        public List<ColorTypes.RGB> getColors()
        {
            return _color_list;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // Call the OnPaint method of the base class.
            base.OnPaint(pe);

            if(_color_list.Count !=0)
            {

                Graphics g = pe.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                Size rect_size = new Size(this.ClientSize.Width / _color_list.Count, this.ClientSize.Height);
                int count = 0;
                foreach (ColorTypes.RGB rgb in _color_list)
                {
                    Point p = new Point(count * rect_size.Width, 0);
                    SolidBrush myBrush = new SolidBrush(ColorTypes.RGBtoColor(rgb));
                    //g.FillRectangle(myBrush, new Rectangle(p, rect_size));
                    g.FillEllipse(myBrush, new Rectangle(p, rect_size));
                    myBrush.Dispose();             
                    count++;
                }
            }
        }
    }
}
