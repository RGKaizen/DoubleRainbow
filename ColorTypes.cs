using System;
using System.Drawing;
using System.Text;

namespace DoubleRainbow
{
    public class ColorTypes
    {
        /// <summary>
        /// Written by Ken Getz
        /// </summary>
        // Handle conversions between RGB and HSV    
        // (and ActiveColor types, as well).


        public class HSV
        {
            // All values are between 0 and 128.
            public int Hue;
            public int Saturation;
            public int value;

            public HSV(int H, int S, int V)
            {
                Hue = H;
                Saturation = S;
                value = V;
            }

            public override string ToString()
            {
                return String.Format("({0}, {1}, {2})", Hue, Saturation, value);
            }
        }

        public class RGB
        {
            private int _red;
            private int _green;
            private int _blue;


            #region Accessors
            public int Red
            {
                get { return this._red; }
                set { setRed(value); }
            }

            public int Green
            {
                get { return this._green; }
                set { setGreen(value); }
            }

            public int Blue
            {
                get { return this._blue; }
                set { setBlue(value); }
            }

            public void setRed(int r)
            {
                if (r > 127)
                    _red = 127;
                else if (r < 0)
                    _red = 0;
                else
                    _red = r;
            }
            public void setGreen(int g)
            {
                if (g > 127)
                    _green = 127;
                else if (g < 0)
                    _green = 0;
                else
                    _green = g;
            }
            public void setBlue(int b)
            {
                if (b > 127)
                    _blue = 127;
                else if (b < 0)
                    _blue = 0;
                else
                    _blue = b;
            }

            #endregion

            public RGB()
            {
                _red = 0;
                _green = 0;
                _blue = 0;
            }

            public RGB(int r, int g, int b)
            {
                setRed(r);
                setGreen(g);
                setBlue(b);
            }

            public RGB(ColorTypes.HSV hsv)
            {
                RGB rgb = ColorTypes.HSVtoRGB(hsv);
                setRed(rgb.Red);
                setGreen(rgb.Green);
                setBlue(rgb.Blue);
            }

            public RGB(Color c)
            {
                setRed(c.R);
                setGreen(c.G);
                setBlue(c.B);
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder(32);
                sb.Append("#");
                sb.Append(_red.ToString());
                sb.Append("#");
                sb.Append(_green.ToString());
                sb.Append("#");
                sb.Append(_blue.ToString());
                return sb.ToString();
            }

            public Boolean different(RGB other)
            {
                if (other._red != this._red)
                    return true;
                if (other._blue != this._blue)
                    return true;
                if (other._green != this._green)
                    return true;
                return false;
            }

        }

        public class CylndrCoords
        {
            public double angle, radius, height;

            public CylndrCoords(double a, double r, double h)
            {
                angle = a;
                radius = r;
                height = h;
            }

        }

        public static RGB HSVtoRGB(int H, int S, int V)
        {
            // H, S, and V must all be between 0 and 255.
            return HSVtoRGB(new HSV(H, S, V));
        }

        public static Color HSVtoColor(HSV hsv)
        {
            if (hsv == null)
                return Color.Blue;

            ColorTypes.RGB RGB = HSVtoRGB(hsv);
            return Color.FromArgb(RGB.Red, RGB.Green, RGB.Blue);
        }

        public static Color HSVtoColor(int H, int S, int V)
        {
            return HSVtoColor(new HSV(H, S, V));
        }

        public static RGB HSVtoRGB(HSV HSV)
        {
            if (HSV == null)
                return new RGB();

            // HSV contains values scaled as in the color wheel:
            // that is, all from 0 to 128. 

            // for ( this code to work, HSV.Hue needs
            // to be scaled from 0 to 360 (it//s the angle of the selected
            // point within the circle). HSV.Saturation and HSV.value must be 
            // scaled to be between 0 and 1.

            double h;
            double s;
            double v;

            double r = 0;
            double g = 0;
            double b = 0;

            // Scale Hue to be between 0 and 360. Saturation
            // and value scale to be between 0 and 1.
            h = ((double)HSV.Hue / 255 * 360) % 360;
            s = (double)HSV.Saturation / 255;
            v = (double)HSV.value / 255;

            if (s == 0)
            {
                // If s is 0, all colors are the same.
                // This is some flavor of gray.
                r = v;
                g = v;
                b = v;
            }
            else
            {
                double p;
                double q;
                double t;

                double fractionalSector;
                int sectorNumber;
                double sectorPos;

                // The color wheel consists of 6 sectors.
                // Figure out which sector you//re in.
                sectorPos = h / 60;
                sectorNumber = (int)(Math.Floor(sectorPos));

                // get the fractional part of the sector.
                // That is, how many degrees into the sector
                // are you?
                fractionalSector = sectorPos - sectorNumber;

                // Calculate values for the three axes
                // of the color. 
                p = v * (1 - s);
                q = v * (1 - (s * fractionalSector));
                t = v * (1 - (s * (1 - fractionalSector)));

                // Assign the fractional colors to radius_norm, _g, and b
                // based on the sector the angle is in.
                switch (sectorNumber)
                {
                    case 0:
                        r = v;
                        g = t;
                        b = p;
                        break;

                    case 1:
                        r = q;
                        g = v;
                        b = p;
                        break;

                    case 2:
                        r = p;
                        g = v;
                        b = t;
                        break;

                    case 3:
                        r = p;
                        g = q;
                        b = v;
                        break;

                    case 4:
                        r = t;
                        g = p;
                        b = v;
                        break;

                    case 5:
                        r = v;
                        g = p;
                        b = q;
                        break;
                }
            }
            // return an RGB structure, with values scaled
            // to be between 0 and 255.
            return new RGB((int)(r * 128), (int)(g * 128), (int)(b * 128));
        }

        public static HSV RGBtoHSV(RGB RGB)
        {
            // In this function, R, G, and B values must be scaled 
            // to be between 0 and 1.
            // HSV.Hue will be angle value between 0 and 360, and 
            // HSV.Saturation and value are between 0 and 1.
            // The code must scale these to be between 0 and 255 for
            // the purposes of this application.

            double min;
            double max;
            double delta;

            double r = (double)RGB.Red / 128;
            double g = (double)RGB.Green / 128;
            double b = (double)RGB.Blue / 128;

            double h;
            double s;
            double v;

            min = Math.Min(Math.Min(r, g), b);
            max = Math.Max(Math.Max(r, g), b);
            v = max;
            delta = max - min;
            if (max == 0 || delta == 0)
            {
                // R, G, and B must be 0, or all the same.
                // In this case, S is 0, and H is undefined.
                // Using H = 0 is as good as any...
                s = 0;
                h = 0;
            }
            else
            {
                s = delta / max;
                if (r == max)
                {
                    // Between Yellow and Magenta
                    h = (g - b) / delta;
                }
                else if (g == max)
                {
                    // Between Cyan and Yellow
                    h = 2 + (b - r) / delta;
                }
                else
                {
                    // Between Magenta and Cyan
                    h = 4 + (r - g) / delta;
                }

            }
            // Scale height to be between 0 and 360. 
            // This may require adding 360, if the value
            // is negative.
            h *= 60;
            if (h < 0)
            {
                h += 360;
            }

            // Scale to the requirements of this 
            // application. All values are between 0 and 255.
            return new HSV((int)(h / 360 * 255), (int)(s * 255), (int)(v * 255));
        }


    }
}