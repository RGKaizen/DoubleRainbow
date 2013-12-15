using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoubleRainbow
{
    public static class Blender
    {
        public static ColorTypes.RGB equalMix(ColorTypes.RGB a, ColorTypes.RGB b)
        {
            return new ColorTypes.RGB((a.Red + b.Red) / 2, (a.Green + b.Green) / 2, (a.Blue + b.Blue) / 2);
        }

        public static ColorTypes.RGB addHighest(ColorTypes.RGB a, ColorTypes.RGB b)
        {
            int red = b.Red;
            if (a.Red > b.Red)
                red = a.Red;
            int green = b.Green;
            if (a.Green > b.Green)
                green = a.Green;
            int blue = b.Blue;
            if (a.Blue > b.Blue)
                blue = a.Blue;
            return new ColorTypes.RGB(red, green, blue);
        }

        // a * perc + b * (1- perc)
        public static ColorTypes.RGB variableMix(ColorTypes.RGB a, ColorTypes.RGB b, double perc)
        {
            return new ColorTypes.RGB((int)(a.Red * perc + b.Red * (1 - perc)), (int)(a.Green * perc + b.Green * (1 - perc)), (int)(a.Blue * perc + b.Blue * (1 - perc)));
        }

        public static ColorTypes.RGB[] MagicMix(ColorTypes.RGB[] a, ColorTypes.RGB[] b, double ratio)
        {
            int length = a.Length;
            // Populates buffer 1 by 1

            ColorTypes.RGB[] result = new ColorTypes.RGB[length];
            for (int i = 0; i < length; i++)
            {

                // Calcualte difference from points
                int redDistance = (b[i].Red - a[i].Red);
                int greenDistance = (b[i].Green - a[i].Green);
                int blueDistance = (b[i].Blue - a[i].Blue);

                ColorTypes.RGB temp = new ColorTypes.RGB();
                temp.Red = (int)(a[i].Red + redDistance * ratio);
                temp.Green = (int)(a[i].Green + greenDistance * ratio);
                temp.Blue = (int)(a[i].Blue + blueDistance * ratio);
                result[i] = temp;
            }
            return result;      
        }

    }
}
