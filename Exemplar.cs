using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoubleRainbow
{
    public class Exemplar
    {
        private ColorTypes.RGB _color;
        private ColorTypes.RGB[] _ledBuffer = null;
        int _nLEDs = -1;
        private int _strip = -1;

        public ColorTypes.RGB[] RGBArr
        {
            get { return this._ledBuffer; }
        }

        public int Length
        {
            get { return this._ledBuffer.Length; }
        }

        // Higher priority dominates the led _color, equal priority requires angle blending formula
        int priority = 0;

        public Exemplar(int strip)
        {
            selectStrip(strip);
            for (int i = 0; i < _nLEDs; i++)
            {
                _ledBuffer[i] = Globals.randomRGB();
            }
        }

        // Single ActiveColor
        public Exemplar(int strip, ColorTypes.RGB c)
        {
            _color = c;
            selectStrip(strip);
            for (int i = 0; i < _nLEDs; i++)
            {
                _ledBuffer[i] = _color;
            }
        }

        // Multiple colors
        public Exemplar(int strip, List<ColorTypes.RGB> rgbList)
        {
            selectStrip(strip);
            int size = _nLEDs/rgbList.Count;
            int j = 0;
            for (int i = 0; i < _nLEDs; i++)
            {
                if (i != 0 && i % size == 0)
                    j = (j + 1) % size;

               _ledBuffer[i] = rgbList[j];
            }
        }

        // Draws gradients from one pos to another pos
        public Exemplar(int strip, ColorTypes.RGB startColor, ColorTypes.RGB endColor, int startPos, int endPos)
        {
            selectStrip(strip);
            _strip = strip;
            bool draw = false;

            int redDistance = (endColor.Red - startColor.Red);
            int greenDistance = (endColor.Green - startColor.Green);
            int blueDistance = (endColor.Blue - startColor.Blue);

            ColorTypes.RGB off = new ColorTypes.RGB();
            for (int i = 0; i < _nLEDs; i++)
            {

                if(startPos == i)
                    draw = true;
                if (draw)
                {
                    double ratio = (double)(i - startPos) / (double)(endPos - startPos);

                    ColorTypes.RGB temp = new ColorTypes.RGB();
                    temp.Red = (int)(startColor.Red + redDistance * ratio);
                    temp.Green = (int)(startColor.Green + greenDistance * ratio);
                    temp.Blue = (int)(startColor.Blue + blueDistance * ratio);

                    _ledBuffer[i] = temp;
                }
                if (!draw)
                        _ledBuffer[i] = off;
                if(endPos == i)
                    draw = false;
            }
        }

        // Changes size from 48 to 32 and vice verse
        private void selectStrip(int stripValue)
        {
            if (stripValue == 1)
                _nLEDs = Globals.KaiLength;
            else if (stripValue == 2)
                _nLEDs = Globals.ZenLength;
            _ledBuffer = new ColorTypes.RGB[_nLEDs];
        }

        public ColorTypes.RGB getRGBPos(int pos)
        {
            return _ledBuffer[pos];
        }

    }
}
