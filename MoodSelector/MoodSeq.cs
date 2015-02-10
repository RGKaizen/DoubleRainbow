using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoubleRainbow
{
    public class MoodSeq
    {
        public String Name = "";
        public List<ColorTypes.RGB> Color_List = new List<ColorTypes.RGB>();

        public MoodSeq()
        {
            Color_List.Add(Colors.Red);
            Color_List.Add(Colors.Green);
        }

        public MoodSeq(ColorTypes.RGB rgb)
        {
            Color_List.Add(rgb);

        }

        public override string ToString()
        {
            return Name;
        }
    }
}
