using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace DoubleRainbow
{
    public partial class KeyMonitor : Form
    {

        Random n = new Random();

        private const int NumThreads = 5;
        private static ManualResetEvent[] resetEvents;

        List<KeyEffect> KeyConfg;

        List<KeyEvt> KeyEvtList;

        public KeyMonitor()
        {
            InitializeComponent();

            // Config key list
            KeyEvtList = new List<KeyEvt>();
            string[] config_lines = File.ReadAllLines("config.txt");
            foreach (string line in config_lines)
            {
                if (line.Length < 3)
                    continue;
                String[] attrs = line.Split(':');
                if (attrs.Length==5)
                {
                    KeyEvtList.Add(new KeyEvt(attrs[0], attrs[1], attrs[2], attrs[3], attrs[4]));
                    
                }
                else if(attrs.Length ==3)
                {
                    KeyEvtList.Add(new KeyEvt(attrs[0], attrs[1], attrs[2]));
                }
            }

        }

        EffectThread effect = null;
        ColorTypes.RGB rgb = new ColorTypes.RGB(127, 0, 0);
        int counter = 0;
        private void Form1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            foreach (KeyEvt evt in KeyEvtList)
            {

                if ((ModifierKeys & Keys.Control) == Keys.Control)
                {
                    if (evt.exp != null && evt.key == e.KeyCode)
                    {
                        for(int i =0 ; i < evt.exp.Length; i++)
                        {
                            Rainbow.KaiLight(i, evt.exp.getRGBPos(i));
                        }
                        Rainbow.KaiShow();
                    }
                }
                else
                {
                    if (e.KeyCode == evt.key)
                    {
                        Rainbow.KaiLight(evt.pos, evt.color);
                        Rainbow.KaiShow();
                    }
                }
            }
        }

        ColorTypes.RGB blank = new ColorTypes.RGB(0, 0, 0);
        private void Form1_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            foreach (KeyEvt evt in KeyEvtList)
            {
                if (e.KeyCode == evt.key)
                {
                    Rainbow.KaiLight(evt.pos, blank);
                    Rainbow.KaiShow();
                }
            }

        }

        private static void DoWork(object o)
        {
            int index = (int)o;

            resetEvents[index].Set();
        }
    }

    public class KeyEvt
    {
        public Keys key;
        public int pos;
        public ColorTypes.RGB color;
        public Exemplar exp;

        public KeyEvt(String key_str, String pos_str, String color_str)
        {
            try
            {
                key = ConvertFromString(key_str);
                pos = Int32.Parse(pos_str);
                color = StrToColor(color_str);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to create KeyEvt ");
            }
        }

        public KeyEvt(String ctrl_key_str, String from_str, String to_str, String from_clr, String to_clr)
        {
            try
            {
                int from = Int32.Parse(from_str);
                int to = Int32.Parse(to_str);
                ColorTypes.RGB from_color = StrToColor(from_clr);
                ColorTypes.RGB to_color = StrToColor(to_clr);
                key = ConvertFromString(ctrl_key_str.Substring(1));
                exp = new Exemplar(1, from_color, to_color, from, to);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to create KeyEvt ");
            }
        }

        public ColorTypes.RGB StrToColor(String color_str)
        {
            try
            {
                String[] colors = color_str.Split(',');
                return new ColorTypes.RGB(int.Parse(colors[0]), int.Parse(colors[1]), int.Parse(colors[2]));
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed making color from string");
            }

            return new ColorTypes.RGB(0, 0, 0);
        }

        public static Keys ConvertFromString(string keystr)
        {
            return (Keys)Enum.Parse(typeof(Keys), keystr);
        }
    }
}
