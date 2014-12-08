using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoubleRainbow
{
    public class HueGenerator
    {
        // Functions
        private float hue;
        private float saturation;
        private float value;
        private bool sat_sign_flag = true;
        private bool value_sign_flag = true;

        private float saturation_high_border = 244.0f;
        private float value_high_border = 244.0f;

        private float saturation_low_border = 0.0f;
        private float value_low_border = 0.0f;

        /// <summary>
        /// Initializes the Hue Animation
        /// </summary>
        /// <param name="initial_hue"></param>
        /// <param name="intial_sat"></param>
        /// <param name="initial_val"></param>
        public HueGenerator(float initial_hue, float intial_sat, float initial_val)
        {
            hue = initial_hue;
            saturation = intial_sat;
            value = initial_val;
            sat_sign_flag = true;
            value_sign_flag = true;
        }

        /// <summary>
        /// Returns the next color in the generator
        /// </summary>
        /// <param name="hue_delta"></param>
        /// <param name="sat_delta"></param>
        /// <param name="value_delta"></param>
        /// <returns></returns>
        public ColorTypes.HSV incrementCylinderSpace(float hue_delta, float sat_delta, float value_delta)
        {

            // Add or subtract delta based on flag state
            saturation = sat_sign_flag ? saturation += sat_delta : saturation -= sat_delta;
            value = value_sign_flag ? value += value_delta : value -= value_delta;

            // Flip flag state when borders hit
            sat_sign_flag = (saturation > saturation_high_border || saturation <= saturation_low_border) ? !sat_sign_flag : sat_sign_flag;
            value_sign_flag = (value > value_high_border || value <= value_low_border) ? !value_sign_flag : value_sign_flag;

            saturation = variClamp(saturation, 200.0f, 256.0f);
            value = variClamp(value, 0.0f, 256.0f);
            hue = (hue + hue_delta) % 360;

            // Angle to 8 bit
            float hue_value = hue * 256 / 360;

            return new ColorTypes.HSV((int)hue_value, (int)saturation, (int)value);
        }

        /// <summary>
        /// Clamps value between min and max exclusively
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private float variClamp(float value, float min, float max)
        {
            if(value > max)
            {
                return max;
            }
            else if(value < min)
            { 
                return min;
            }
            return value;

        }

    }
}
