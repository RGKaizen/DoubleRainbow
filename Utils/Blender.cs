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
            return new ColorTypes.RGB(Math.Max(b.Red, a.Red), Math.Max(b.Green, a.Green), Math.Max(b.Blue, a.Blue));
        }

        // a * perc + b * (1- perc)
        public static ColorTypes.RGB variableMix(ColorTypes.RGB a, ColorTypes.RGB b, double perc)
        {
            return new ColorTypes.RGB((int)(a.Red * perc + b.Red * (1 - perc)), (int)(a.Green * perc + b.Green * (1 - perc)), (int)(a.Blue * perc + b.Blue * (1 - perc)));
        }

        public static ColorTypes.RGB MagicMix(ColorTypes.RGB a, ColorTypes.RGB b, double ratio)
        {
            ColorTypes.RGB result = new ColorTypes.RGB();
            int redDistance = (b.Red - a.Red);
            int greenDistance = (b.Green - a.Green);
            int blueDistance = (b.Blue - a.Blue);

            result.Red = (int)(a.Red + redDistance * ratio);
            result.Green = (int)(a.Green + greenDistance * ratio);
            result.Blue = (int)(a.Blue + blueDistance * ratio);

            return result;      
        }

        public static ColorTypes.RGB ProperMix(ColorTypes.RGB a, ColorTypes.RGB b, double ratio)
        {

            ColorTypes.RGB result = new ColorTypes.RGB();
            result.Red = properMixAlgo(b.Red, a.Red);
            result.Green = properMixAlgo(b.Green, a.Green);
            result.Blue = properMixAlgo(b.Blue, a.Blue);
            return result;
        }

        private static int properMixAlgo(int color1, int color2)
        {
            return (int)( Math.Sqrt( ( Math.Pow(color1, 2) + Math.Pow(color2, 2) ) / 2) );
        }
    }
}
