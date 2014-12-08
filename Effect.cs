using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoubleRainbow
{
    public class Effect
    {
        int _center;
        int width_left;
        int width_right;
        ColorTypes.RGB[] effect_arr;

        public Effect(int center, int radius)
        {
            this._center = center;
            this.width_left = radius/2;
            this.width_right = radius/2;
            effect_arr = new ColorTypes.RGB[this.width_left + 1 + this.width_right];
        }

        public Effect(int center, int width_left, int width_right)
        {
            this._center = center;
            this.width_left = width_left;
            this.width_right = width_right;
            effect_arr = new ColorTypes.RGB[this.width_left + 1 + this.width_right];
        }


        public void Animate()
        {

            //fill
            ColorTypes.RGB color = new ColorTypes.RGB(127,0,0);
            for(int i = -width_left; i < effect_arr.Length; i++)
            {       
                Rainbow.KaiSet(_center+i, color);
            }
            Rainbow.KaiUpdate();

        }

    }
}
