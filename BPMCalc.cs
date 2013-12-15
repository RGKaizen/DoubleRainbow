using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

namespace DoubleRainbow
{
    public partial class BPMCalc : Form
    {
        public BPMCalc()
        {
            InitializeComponent();
        }

        Lighter lighter;
        Thread workerThread;


        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern short GetAsyncKeyState(Keys vkey);
        private void BPMCalc_Load(object sender, EventArgs e)
        {
            timer1.Start();
            lighter = new Lighter();          
        }

        public int clicks = 0;
        public Stopwatch sw = new Stopwatch(); 
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (IsSpaceDown())
            {
                if (workerThread != null && !workerThread.IsAlive)
                {
                    lighter = new Lighter();
                    workerThread = new Thread(lighter.Light);
                    workerThread.Start();
                }
                clicks++;
                if (sw.IsRunning)
                {
                  BPMBox.Text =  GenerateAverage(Convert.ToInt32(sw.ElapsedMilliseconds)).ToString();
                  lighter.sleep =  (60000)/GenerateAverage(Convert.ToInt32(sw.ElapsedMilliseconds));
                  sw.Reset();
 
                }
                sw.Start();

                if (runningCounter > 5)
                {
                    workerThread = new Thread(lighter.Light);
                }

            }
        }

        public int runningAverage = 0;
        public int runningCounter = 0;
        private int GenerateAverage(int elapsed)
        {
            runningAverage += (60000/elapsed);
            runningCounter++;
            Console.WriteLine(runningAverage + " " + runningCounter + " " + 60000/(runningAverage / runningCounter));
            return runningAverage / runningCounter;
        }

        private bool IsSpaceDown()
        {
            int state = Convert.ToInt32(GetAsyncKeyState(Keys.Space).ToString());
            switch (state)
            {
                case 0:
                    // "A has not been pressed since the last call.";
                    return false;
                case 1:
                    //  "A is not currently pressed, but has been pressed since the last call."
                    return true;
                case -32767:
                    // "A is currently pressed.";
                    return true;
            }
            return false;
        }

        private void ClrBtn_Click(object sender, EventArgs e)
        {
            runningAverage = 0;
            runningCounter = 0;
            lighter.RequestStop();
            workerThread.Join();
        }


    }

    public class Lighter
    {
        // Volatile is used as hint to the compiler that this data
        // member will be accessed by multiple threads.
        private volatile bool _shouldStop = false;

        public int sleep = 0;
        public void Light()
        {
            while (!_shouldStop)
            {
                Workshop.Demo();
                Thread.Sleep(sleep);   
            }
            Console.WriteLine("worker thread: terminating gracefully.");
        }
        public void RequestStop()
        {
            _shouldStop = true;
        }


    }
}
