using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;

namespace DoubleRainbow
{
    static class Workshop
    {
        public static Boolean flag = false;
        public static void Demo()
        {
            Exemplar a = new Exemplar(1, Colors.Red, Colors.Red, 0, Globals.KaiLength);

            Exemplar b = new Exemplar(1, Colors.Green, Colors.Green, 0, Globals.KaiLength);

            int z = 0;
            for(int j = 0; j < 2000; j++)
            {
                Rainbow.KaiLight(z, Colors.Red);
                Rainbow.KaiLight(Globals.KaiLength-z, Colors.Green);
                if(z == Globals.KaiLength-z)
                {
                    Rainbow.KaiLight(z, new ColorTypes.RGB(127,127,0));
                }

                Rainbow.KaiShow();

                z++;
                if(z>47)
                    z = 0;
            }

            Rainbow.KaiShow();

        }

        public static void Demo2(int sleep)
        {
            Rainbow.KaiLight(32, RandClr());
            Rainbow.KaiShow();
            Thread.Sleep(sleep);
        }

        public static void Basic()
        {
            Exemplar exem = new Exemplar(1, new ColorTypes.RGB(127,0,0), new ColorTypes.RGB(0,65,127), 0, Globals.KaiLength);
            for (int i = 0; i < Globals.KaiLength; i++)
            {
                Rainbow.Kai[i] = exem.getRGBPos(i);
            }
            Rainbow.KaiUpdate();
            Rainbow.KaiShow();
        }

        public static Random rand = new Random();
        public static ColorTypes.RGB RandClr()
        {
            return new ColorTypes.RGB(rand.Next(128), rand.Next(128), rand.Next(128));
        }
    

        public static void RisingSun()
        {
            
        }


        public static void SpeedTest()
        {
            Random n = new Random();

            for(int j = 0; j < 100; j++)
            {
                int red = n.Next(127);
                int green = n.Next(127);
                int blue = n.Next(127);
                for(int i = 0; i < Globals.KaiLength; i++)
                {
                    TestCompress(i, red, green, blue);
                }
                Rainbow.sendString(showByte());
            }
        }

        public static void TestCompress(int pos, int red, int green, int blue)
        {
            int cmd = 0; // 1 bit,     [7] 0 = change color, 1 = turn on
            int strip = 0; // 1 bit    [6] 0 = kai, 1 = zen
            // pos   6 bits   [0 -   5]
            // red   8 bits   [7 -  15]
            // green 8 bits   [16 - 22]
            // blue  8 bits   [23 - 31]
            // total = 32
            byte[] bytes = new byte[4];

            bytes[0] = (byte)pos;

            // strip
            if (strip == 1)
                bytes[0] += (byte)64;
            // cmd
            if (cmd == 1)
                bytes[0] += (byte)128;

            bytes[1] = (byte)red;
            bytes[2] = (byte)green;
            bytes[3] = (byte)blue;

            //Console.WriteLine("Pos: " + pos + " byte: " + bytes[0]);

            Rainbow.sendString(bytes);
        }


        public static byte[] showByte()
        {
            byte[] b = new byte[4];
            b[0] = 128;
            b[1] = 0;
            b[2] = 0;
            b[3] = 0;
            return b;
        }



        public static void BASetValue(BitArray array, int value, int start, int length)
        {

            String bitString = "";
            bitString = Convert.ToString(value, 2).PadLeft(length, '0');
            //Console.WriteLine(bitString);
            for (int i = 0; i < length; i++)
            {
                if(bitString[i].Equals('1'))
                    array[start + i] = true; 
                else
                    array[start + i] = false; 
            }
        }


    }
}
