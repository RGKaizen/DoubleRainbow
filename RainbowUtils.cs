﻿using System;
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

    }
}
