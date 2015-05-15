using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleRainbow
{
    public static class RainbowUtils
    {
        public static void fillBoth(ColorTypes.RGB rgb)
        {
            if (rgb == null) return;

            for (int i = 0; i < Globals.KaiLength; i++)
            {
                Rainbow.KaiSet(i, rgb);
            }
            update();
        }

        public static void update()
        {
            if (Rainbow.KaiUpdate())
            {
                Rainbow.KaiShow();
            }
            Rainbow.BuddySystem();
        }


        public static void TurnOff()
        {
            Rainbow.KaiClear();
            Rainbow.ZenClear();
        }

        public static ColorTypes.RGB increaseBrightness(ColorTypes.RGB rgb, int amt)
        {
            ColorTypes.HSV hsv = new ColorTypes.HSV(rgb);
            hsv.Value = hsv.Value + amt > 256 ? 256 : hsv.Value + amt;
            return new ColorTypes.RGB(hsv);
        }

    }
}
