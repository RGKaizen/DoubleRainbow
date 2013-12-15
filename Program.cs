using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ColorChooserCSharp;

namespace DoubleRainbow
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            if (!Rainbow.Connect())
            {
                Console.WriteLine("Failed to connect to Arduino");
            }

            //Workshop.TestCompress();
            //Console.Read();

            //Rainbow.reportStrip();

            //Workshop.SpeedTest();
            
            Application.Run(new MainWindow());

            //Application.Run(new ColorChooser());

            //Application.Run(new KeyMonitor());

            //Rainbow.KaiClear();
            //Rainbow.ZenClear();
            //Workshop.Basic();
            //Application.Run(new BPMCalc());
            //Workshop.Demo();
            //Rainbow.KaiShow();


        }
    }
}
