using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoubleRainbow
{
    public static class Globals
    {
        // Number of leds on each strip
        public const int KaiLength = 48;
        public const int ZenLength = 32;

        public static ColorTypes.RGB wheel = null;
        public static bool isChanged = false;
        public static void passColor(ColorTypes.RGB inRGB)
        {
            wheel = new ColorTypes.RGB(inRGB.Red, inRGB.Green, inRGB.Blue);
            isChanged = true;
        }

        // Functions
        public static Random n = new Random();
        public static ColorTypes.RGB randomRGB()
        {

            int r = n.Next(127);
            int g = n.Next(127);
            int b = n.Next(127);
            if (r > 80)
                g /= 3;
            if (g > 50)
                r /= 4;
            if (b > 70)
            {
                g /= 3;
            }

            return new ColorTypes.RGB(r / 4, g / 4, b / 4);
        }
    }
}
