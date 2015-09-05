using System;

namespace DoubleRainbow
{
    public class ColorChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Written by Ken Getz
        /// </summary>
        private DRColor.RGB mRGB;
        private DRColor.HSV mHSV;

        public ColorChangedEventArgs(DRColor.RGB RGB, DRColor.HSV HSV)
        {
            mRGB = RGB;
            mHSV = HSV;
        }

        public DRColor.RGB RGB
        {
            get
            {
                return mRGB;
            }
        }

        public DRColor.HSV HSV
        {
            get
            {
                return mHSV;
            }
        }
    }
}