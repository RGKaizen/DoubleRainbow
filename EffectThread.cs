using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace DoubleRainbow
{
    class EffectThread
    {
        private volatile bool _shouldStop;

        private ColorTypes.RGB _rgb = null;
        private int _center = 24;

        public EffectThread(int loc, ColorTypes.RGB rgb)
        {
            _center= loc;
            _rgb = rgb;
        }

        public void DoWork()
        {
            try
            {
                while (!_shouldStop)
                {
                    PulseAt();
                }
                //DecayBehavior();
            }
            catch (ThreadInterruptedException e)
            {
                Console.WriteLine("newThread cannot go to sleep - " +
                    "interrupted by main thread.");
            }

            Console.WriteLine("worker thread: terminating gracefully.");
        }


        private void PulseAt()
        {

            ColorTypes.RGB rgb = _rgb;//Globals.randomRGB();
            for(int i = -2; i < 3; i++)
            {
                Rainbow.KaiLight(_center+i, rgb);
            }
            Rainbow.KaiShow();
        }


        public void DecayBehavior()
        {
            ColorTypes.RGB rgb = _rgb;
            for (int j = 0; j < 20; j++)
            {
                for (int i = -3; i < 3; i++)
                {
                    Rainbow.KaiLight(_center + i, rgb);
                    rgb = Blender.variableMix(rgb, blank, 0.9);
                }
                Rainbow.KaiShow();
            }
        }

        private void PulseAtV2()
        {
            ColorTypes.RGB rgb = _rgb;//Globals.randomRGB();
            for (int i = -3; i < 3; i++)
            {
                Rainbow.Kai[_center+i] = rgb;
            }

            if (Rainbow.KaiUpdate())
            {
                Rainbow.KaiShow();
            }
        }

        public void DecayBehaviorV2()
        {
            ColorTypes.RGB rgb = Globals.randomRGB();
            for (int j = 0; j < 20; j++)
            {
                for (int i = -3; i < 3; i++)
                {
                    Rainbow.Kai[_center + i] = rgb;
                    rgb = Blender.variableMix(rgb, blank, 0.9);
                }

                if (Rainbow.KaiUpdate())
                {
                    Rainbow.KaiShow();
                    Thread.Sleep(70);
                }
            }
        }

        ColorTypes.RGB blank = new ColorTypes.RGB(0, 0, 0);
        public void RequestStop()
        {
            _shouldStop = true;
            for(int i = -3; i < 3; i++)
            {
                Rainbow.KaiLight(_center+i, blank);
            }
           Rainbow.KaiShow();
        }
    }
}
