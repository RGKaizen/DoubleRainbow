using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DoubleRainbow
{
    class KeyEffect
    {
        public ColorTypes.RGB rgb;
        public int location;
        public Keys key;
        public int id;

        public KeyEffect(String line)
        {
            try
            {
                String[] args = line.Split('#');
                key = (Keys)Enum.Parse(typeof(Keys), args[0]);
                location = int.Parse(args[1]);
                String[] colors = args[2].Split(',');
                rgb = new ColorTypes.RGB(int.Parse(colors[0]), int.Parse(colors[1]), int.Parse(colors[2]));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error creating KeyEffect\n\n: " + line);
            }
        }

    }
}
