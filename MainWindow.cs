using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using ColorChooserCSharp;
using System.IO;

namespace DoubleRainbow
{
    /* Todo List
     * 1. ActiveColor Wheel DONE, could still use some adjusting methinks
     * 2. ActiveColor Saver DONE, Needs better integration with Main
     * 3. More interesting transformations on Exemplars
     * 4. Animations
     * */

    public partial class MainWindow : Form
    {
        Stopwatch s = new Stopwatch();
        const int KaiLength = Globals.KaiLength;
        ColorChooser cs = null;

        public MainWindow()
        {
            InitializeComponent();

        }

        StringBuilder log = new StringBuilder();
        public void Log(string logMessage)
        {
            log.AppendLine(logMessage);
        }

        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Clear();
            //Thread.Sleep(250);
        }


        // Test Button
        private void testButton(object sender, EventArgs e)
        {
            SpeedTest();
            //gradientTest();
            //smoothTest();

        }

        private void SpeedTest()
        {
            int k = 0;

            CylinderSet(0.0f, 0.0f, 0.0f);

            for (int i = 0; i < 20; i++)
            {
                CylinderSpace();
            }
        }

        private void BaseTest()
        {
            for (int j = 0; j < 10; j++)
            {
                ColorTypes.RGB myRGB = null;
                if (j % 3 == 0)
                    myRGB = new ColorTypes.RGB(new ColorTypes.HSV(0, 256, 256));
                else if (j % 3 == 1)
                    myRGB = new ColorTypes.RGB(new ColorTypes.HSV(45, 256, 256));
                else if (j % 3 == 2)
                    myRGB = new ColorTypes.RGB(new ColorTypes.HSV(90, 256, 256));
                Exemplar a = new Exemplar(1, myRGB);
                for (int i = 0; i < Globals.KaiLength; i++)
                {
                    Rainbow.KaiLight(i, a.getRGBPos(i));
                }
                //Rainbow.KaiUpdate();
                Rainbow.KaiShow();
            }
        }

        // Play Action
        private Boolean repeater(object sender, EventArgs e)
        {
            CylinderSet(0.0f, 0.0f, 255.0f);
            counter = 0;
            s.Start();
            while (isOn)
            {
                CylinderSpace();
                //gradientTest(); // Auto-Gradient
                //animationTest(angle, b); // Animation testing
                //smoothTest(); // Smooth gradientrs
                //setColor(); // Control Directly
                //RAVE();
            }
            s.Stop();
            return true;
        }

        // Functions
        public float omega;
        public float radius;
        public float height;
        public int counter;
        public int counter2;

        public bool flipFlag = true;
        public void CylinderSpace()
        {
            ColorTypes.RGB myRGB = incrementCylinderSpace((float)Slider1Value / 100, (float)Slider2Value / 100, (float)Slider3Value / 100);
            /*
            counter += 1;
            counter2 -= 1;
            if (counter > 47)
                counter = 24;
            if (counter2 < 0)
                counter2 = 23;
            */
            Rainbow.Kai[23] = myRGB;
            Rainbow.Kai[24] = myRGB;
            Push();


           // Rainbow.Kai[counter%48] = myRGB;
           // Rainbow.Kai[counter2%48] = myRGB;

            /*
            for (int i = 0; i < Globals.KaiLength; i++)
            {
                Rainbow.Kai[i] = myRGB;
            }
             */

            RefreshKai();
        }

        public void Push()
        {
            for (int i = 0; i < 23; i++)
            {
                Rainbow.Kai[i] = Rainbow.Kai[i + 1];
            }
            for (int i = 47; i > 24; i--)
            {
                Rainbow.Kai[i] = Rainbow.Kai[i - 1];
            }
        }

        /*
         * 
            if (flipFlag)
                counter++;
            else
                counter--;

            if (counter >= 47 || counter <= 0)
                flipFlag = !flipFlag;

            if (counter == -1)
                counter = 0;*/

        public void CylinderSet()
        {
            omega = 128.0f;
            radius = 128.0f;
            height = 128.0f;
            counter = 0;

            for(int i = 0; i < Globals.KaiLength; i++)
            {
                Rainbow.Kai[i] = Globals.randomRGB();
            }
        }

        public void CylinderSet(float o, float r, float h)
        {
            omega = o;
            radius = r;
            height = h;
            counter = 0;
            bFlag = true;
            cFlag = true;
        }

        bool bFlag = true;
        bool cFlag = true;
        public ColorTypes.RGB incrementCylinderSpace(float a, float b, float c)
        {
            if (bFlag)
                radius = radius + b;
            else
                radius = radius - b;

            if (cFlag)
                height = height + c;
            else
                height = height - c;

            if (radius > 256.0f || radius <= 200.0f)
                bFlag = !bFlag;
            if (height > 256.0f || height <= 0.0f)
                cFlag = !cFlag;

            if (radius <= 128.0f)
                radius = 200.0f;

            if (radius > 256.0f)
                radius = 256.0f;

            if (height > 256.0f)
                height = 256.0f;

            omega =( omega+a) % 360;

            float hue_value = omega * 256 / 360;

            Log(omega + ":" + radius + ":" + height);

            float temp_height = height;
            if (height <= 60.0f)
                temp_height = 0.0f;
            spiral.curCoord = new ColorTypes.CylndrCoords(omega, radius, temp_height);
            return new ColorTypes.RGB(new ColorTypes.HSV((int)hue_value, (int)radius, (int)temp_height));
        }

        public void setColor()
        {
            if (Globals.isChanged)
            {
                for (int i = 0; i < Globals.KaiLength; i++)
                {
                    Rainbow.Kai[i] = Globals.wheel;
                }
                RefreshKai();
                Globals.isChanged = false;
            }
        }

        private void gradientTest()
        {
            Exemplar a = new Exemplar(1, Globals.randomRGB(), Globals.randomRGB(), 0, Globals.KaiLength);
            for (int i = 0; i < Globals.KaiLength; i++)
            {
                Rainbow.KaiLight(i, a.getRGBPos(i));
            }

            Rainbow.KaiUpdate();
            Rainbow.KaiShow();

            Exemplar b = new Exemplar(1, Globals.randomRGB(), Globals.randomRGB(), 0, Globals.ZenLength);
            for (int i = 0; i < Globals.ZenLength; i++)
            {
                Rainbow.ZenLight(i, b.getRGBPos(i));
            }

            Rainbow.ZenUpdate();
            Rainbow.ZenShow();
        }

        private void RAVE()
        {
        }

        bool flip = false;
        private void animationTest(ColorTypes.RGB color, ColorTypes.RGB color2)
        {
            ColorTypes.RGB blank = new ColorTypes.RGB(0, 0, 0);
            for (int i = 0; i < KaiLength; i++)
            {
                if (flip)
                {
                    Rainbow.Kai[i] = color;
                    Rainbow.Kai[KaiLength - (i + 1)] = color2;
                }
                else
                {
                    Rainbow.Kai[i] = color2;
                    Rainbow.Kai[KaiLength - (i + 1)] = color;
                }

                // Erases
                for (int j = 0; j < KaiLength; j++)
                {
                    if (i == j || KaiLength - i == j)
                    {
                        continue;
                    }
                    else if (i == KaiLength - i)
                    {
                        Rainbow.Kai[j] = Blender.equalMix(color, color2);
                    }
                    else
                    {
                        Rainbow.Kai[j] = Blender.variableMix(Rainbow.Kai[j], blank, 0.1 + 50 / Slider1Value);
                    }
                }


                if (Rainbow.KaiUpdate())
                {
                    Rainbow.KaiShow();
                    Thread.Sleep(refreshRate * 25);
                }
            }
            flip = !flip;

        }

        private void smoothTest()
        {
            //Exemplar red = new Exemplar(new RGB(127,0,0), 1);
            //Exemplar green = new Exemplar(new RGB(0, 127, 0), 1);
            //Exemplar blue = new Exemplar(new RGB(0, 0, 127), 1);
            //SmotherGenerator(red, green, 40);
            //SmotherGenerator(green, blue, 60);
            //SmotherGenerator(blue, red, 20); 

            // 4 segments dancing between two lists. Move the lists and move the segment

            List<ColorTypes.RGB> list1 = new List<ColorTypes.RGB>();
            //list1.Add(RGB1);
            //list1.Add(RGB2);
            list1.Add(new ColorTypes.RGB());
            list1.Add(new ColorTypes.RGB(0, 127, 0));

            List<ColorTypes.RGB> list2 = new List<ColorTypes.RGB>();
            //list2.Add(RGB2);
            list2.Add(new ColorTypes.RGB(0, 127, 0));
            //list2.Add(RGB1);
            list2.Add(new ColorTypes.RGB());

            Exemplar a = new Exemplar(1, list1);
            Exemplar b = new Exemplar(2, list2);
            int smoothness = 10;
            for (int i = 0; i < smoothness * 2; i++)
            {
                if (i < smoothness)
                    SmotherGenerator(a, b, smoothness);
                else if (i >= smoothness)
                    SmotherGenerator(b, a, smoothness);
            }

        }

        // Take two exemplars and return their interpolation angle point int time T, for variable T
        int SmotherTrig = 0;
        private int SmotherGenerator(Exemplar start, Exemplar finish, int smoothness)
        {
            SmotherTrig = SmotherTrig % smoothness;
            Rainbow.Kai = Smoothstone(start.RGBArr, finish.RGBArr, (double)SmotherTrig / smoothness);

            if (this.backgroundWorker1.CancellationPending)
                return ++SmotherTrig;

            Rainbow.KaiUpdate();
            RefreshKai();
            return ++SmotherTrig;
        }

        public ColorTypes.RGB[] Smoothstone(ColorTypes.RGB[] beg, ColorTypes.RGB[] end, double ratio)
        {
            int length = beg.Length;
            // Populates buffer 1 by 1

            ColorTypes.RGB[] result = new ColorTypes.RGB[length];
            for (int i = 0; i < length; i++)
            {

                // Calcualte difference from points
                int redDistance = (end[i].Red - beg[i].Red);
                int greenDistance = (end[i].Green - beg[i].Green);
                int blueDistance = (end[i].Blue - beg[i].Blue);

                ColorTypes.RGB temp = new ColorTypes.RGB();
                temp.Red = (int)(beg[i].Red + redDistance * ratio);
                temp.Green = (int)(beg[i].Green + greenDistance * ratio);
                temp.Blue = (int)(beg[i].Blue + blueDistance * ratio);
                result[i] = temp;
            }
            return result;
        }

        // Use this to send commands to Kai!
        public void RefreshKai()
        {
            if (Rainbow.KaiUpdate())
                Rainbow.KaiShow();
            //Rainbow.BuddySystem();
            Thread.Sleep(refreshRate);
        }

        // Use this to send commands to Kai!
        public void RefreshZen()
        {
            if (Rainbow.ZenUpdate())
                Rainbow.ZenShow();
            Thread.Sleep(refreshRate * 25);
        }

        //public bool isIdle = true;
        //public void DynamicRefresh()
        //{
        //    if (isIdle && refreshRate < 5000)
        //    {
        //        refreshRate *= 2;
        //    }

        //    if (!isIdle)
        //    {
        //        refreshRate = 1;
        //    }

        //}



        // Starts/stops repeat thread
        Boolean isOn = false;
        private void PlayPause(object sender, EventArgs e)
        {
            if (isOn)
            {
                this.backgroundWorker1.CancelAsync();
                this.button2.Text = "Play";
                SaveColors();
            }

            if (!isOn && !this.backgroundWorker1.IsBusy)
            {
                this.button2.Text = "Pause";
                this.backgroundWorker1.RunWorkerAsync(10);
            }

            isOn = !isOn;
            spiral.DrawFunction();

        }

        private void ClearButton(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            ColorTypes.RGB clr = new ColorTypes.RGB(0,0, 0);
            for(int i = 0; i < KaiLength; i++){
                Rainbow.KaiLight(i, clr);
            }
            Rainbow.KaiShow();
            //Rainbow.KaiClear();
            Rainbow.ZenClear();
        }

        private void handleExemplars(List<Exemplar> exemList)
        {
            ColorTypes.RGB[] ledBuffer = new ColorTypes.RGB[Globals.KaiLength];
            for (int i = 0; i < Globals.KaiLength; i++)
            {
                ColorTypes.RGB temp = exemList[0].getRGBPos(i);
                for (int j = 1; j < exemList.Count; j++)
                {
                    temp = Blender.equalMix(temp, exemList[j].getRGBPos(i));
                }
                ledBuffer[i] = temp;
            }

            Rainbow.Kai = ledBuffer;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Do not access the form's BackgroundWorker reference directly. 
            // Instead, use the reference provided by the sender parameter.
            BackgroundWorker bw = sender as BackgroundWorker;
            // Start the time-consuming operation.
            e.Result = repeater(bw, e);

            // If the operation was canceled by the user,  
            // set the DoWorkEventArgs.Cancel property to true. 
            if (bw.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        public double Slider1Value = 600;
        private void Slider1_Scroll(object sender, ScrollEventArgs e)
        {
            Slider1Value = Slider1.Value*3;
            Slider1ValueLbl.Text = "Val: " + Slider1Value;
        }

        public double Slider2Value = 200;
        private void Slider2_Scroll(object sender, ScrollEventArgs e)
        {
            Slider2Value = Slider2.Value*3;
            Slider2ValueLbl.Text = "Val: " + Slider2Value;
        }

        public double Slider3Value = 200;
        private void Slider3_Scroll(object sender, ScrollEventArgs e)
        {
            Slider3Value = Slider3.Value*3;
            Slider3ValueLbl.Text = "Val: " + Slider3Value;
        }

        public int refreshRate = 1;
        private void RefreshBar_Scroll(object sender, ScrollEventArgs e)
        {
            refreshRate = RefreshBar.Value;
            //DimLights(refreshRate);
        }

        private void DimLights(int val)
        {
            for(int i = 0; i < KaiLength; i++)
            {
                Rainbow.Kai[i] = AdjustVal(_maxColor[i], (val / (double)100));
            }
            RefreshKai();

        }

        ColorTypes.RGB[] _maxColor = new ColorTypes.RGB[KaiLength];
        private void SaveColors()
        {
            Array.Copy(Rainbow.Kai, _maxColor, KaiLength);
        }

        private ColorTypes.RGB AdjustHue(ColorTypes.RGB rgb, double perc)
        {
            ColorTypes.HSV hsv = ColorTypes.RGBtoHSV(rgb);
            hsv.Hue = (int)(hsv.Hue * perc);
            return ColorTypes.HSVtoRGB(hsv);
        }

        private ColorTypes.RGB AdjustVal(ColorTypes.RGB rgb, double perc)
        {
            ColorTypes.HSV hsv = ColorTypes.RGBtoHSV(rgb);
            hsv.value = (int)(hsv.value * perc);
            return ColorTypes.HSVtoRGB(hsv);
        }

        private ColorTypes.RGB AdjustSat(ColorTypes.RGB rgb, double perc)
        {
            ColorTypes.HSV hsv = ColorTypes.RGBtoHSV(rgb);
            hsv.Saturation = (int)(hsv.Saturation * perc);
            return ColorTypes.HSVtoRGB(hsv);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            spiral.updateCenters(Slider1.Value, Slider2.Value, Slider3.Value);
            spiral.GraphCylinder();
        }

    }
}
