using System;

namespace DoubleRainbow
{
    public class ColorChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Written by Ken Getz
        /// </summary>
        private ColorTypes.RGB mRGB;
        private ColorTypes.HSV mHSV;

        public ColorChangedEventArgs(ColorTypes.RGB RGB, ColorTypes.HSV HSV)
        {
            mRGB = RGB;
            mHSV = HSV;
        }

        public ColorTypes.RGB RGB
        {
            get
            {
                return mRGB;
            }
        }

        public ColorTypes.HSV HSV
        {
            get
            {
                return mHSV;
            }
        }
    }
}