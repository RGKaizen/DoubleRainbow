using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoubleRainbow
{
    public static class Blender
    {
        public static DRColor.RGB equalMix(DRColor.RGB a, DRColor.RGB b)
        {
            return new DRColor.RGB((a.Red + b.Red) / 2, (a.Green + b.Green) / 2, (a.Blue + b.Blue) / 2);
        }

        public static DRColor.RGB addHighest(DRColor.RGB a, DRColor.RGB b)
        {
            return new DRColor.RGB(Math.Max(b.Red, a.Red), Math.Max(b.Green, a.Green), Math.Max(b.Blue, a.Blue));
        }

        // a * perc + b * (1- perc)
        public static DRColor.RGB variableMix(DRColor.RGB a, DRColor.RGB b, double perc)
        {
            return new DRColor.RGB((int)(a.Red * perc + b.Red * (1 - perc)), (int)(a.Green * perc + b.Green * (1 - perc)), (int)(a.Blue * perc + b.Blue * (1 - perc)));
        }

        public static DRColor.RGB MagicMix(DRColor.RGB a, DRColor.RGB b, double ratio)
        {
            DRColor.RGB result = new DRColor.RGB();
            int redDistance = (b.Red - a.Red);
            int greenDistance = (b.Green - a.Green);
            int blueDistance = (b.Blue - a.Blue);

            result.Red = (int)(a.Red + redDistance * ratio);
            result.Green = (int)(a.Green + greenDistance * ratio);
            result.Blue = (int)(a.Blue + blueDistance * ratio);

            return result;      
        }

        public static DRColor.RGB ProperMix(DRColor.RGB a, DRColor.RGB b, double ratio)
        {

            DRColor.RGB result = new DRColor.RGB();
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
