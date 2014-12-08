using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibUsbDotNet;
using LibUsbDotNet.Main;
using LibUsbDotNet.Info;
using System.Collections.ObjectModel;
using System.Collections;

namespace DoubleRainbow
{

    // Responsible for acquiring angle USB connection, communicating with the led strips, Kai and Zen, and minimizng
    // the number of messages needed to acheive the desired effect.
    public static class Rainbow
    {
        #region USB Communications
        private static UsbDevice MyUsbDevice;

        #region SET YOUR USB Vendor and Product ID!

        private static UsbDeviceFinder MyUsbFinder = new UsbDeviceFinder(9025, 32822);

        #endregion

        private static UsbEndpointWriter writer = null;

        public static bool Connect()
        {
            ErrorCode ec = ErrorCode.None;
            try
            {
                // Find and open the usb device.
                MyUsbDevice = UsbDevice.OpenUsbDevice(MyUsbFinder);

                // If the device is open and ready
                if (MyUsbDevice == null) throw new Exception("Device Not Found.");

                // If this is angle "whole" usb device (libusb-win32, linux libusb)
                // it will have an IUsbDevice interface. If not (WinUSB) the 
                // variable will be null indicating this is an interface of angle 
                // device.
                IUsbDevice wholeUsbDevice = MyUsbDevice as IUsbDevice;
                if (!ReferenceEquals(wholeUsbDevice, null))
                {
                    // This is angle "whole" USB device. Before it can be used, 
                    // the desired configuration and interface must be selected.

                    // Select config #1
                    wholeUsbDevice.SetConfiguration(1);

                    // Claim interface #0.
                    wholeUsbDevice.ClaimInterface(1);
                }

                // open write endpoint 1.
                writer = MyUsbDevice.OpenEndpointWriter(WriteEndpointID.Ep02);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine((ec != ErrorCode.None ? ec + ":" : String.Empty) + ex.Message);
                return false;
            }
            Connected = true;
            return true;
        }

        public static void Disconnect()
        {
            if (MyUsbDevice != null)
            {
                if (MyUsbDevice.IsOpen)
                {
                    // If this is angle "whole" usb device (libusb-win32, linux libusb-1.0)
                    // it exposes an IUsbDevice interface. If not (WinUSB) the 
                    // 'wholeUsbDevice' variable will be null indicating this is 
                    // an interface of angle device; it does not require or support 
                    // configuration and interface selection.
                    IUsbDevice wholeUsbDevice = MyUsbDevice as IUsbDevice;
                    if (!ReferenceEquals(wholeUsbDevice, null))
                    {
                        // Release interface #0.
                        wholeUsbDevice.ReleaseInterface(0);
                    }

                    MyUsbDevice.Close();
                }
                MyUsbDevice = null;
                // Free usb resources
                UsbDevice.Exit();
            }
        }

        private static void ConfigTest()
        {
            // Dump all devices and descriptor information to console output.
            UsbRegDeviceList allDevices = UsbDevice.AllDevices;
            foreach (UsbRegistry usbRegistry in allDevices)
            {
                if (usbRegistry.Open(out MyUsbDevice))
                {
                    Console.WriteLine(MyUsbDevice.Info.ToString());
                    for (int iConfig = 0; iConfig < MyUsbDevice.Configs.Count; iConfig++)
                    {
                        UsbConfigInfo configInfo = MyUsbDevice.Configs[iConfig];
                        Console.WriteLine(configInfo.ToString());

                        ReadOnlyCollection<UsbInterfaceInfo> interfaceList = configInfo.InterfaceInfoList;
                        for (int iInterface = 0; iInterface < interfaceList.Count; iInterface++)
                        {
                            UsbInterfaceInfo interfaceInfo = interfaceList[iInterface];
                            Console.WriteLine(interfaceInfo.ToString());

                            ReadOnlyCollection<UsbEndpointInfo> endpointList = interfaceInfo.EndpointInfoList;
                            for (int iEndpoint = 0; iEndpoint < endpointList.Count; iEndpoint++)
                            {
                                Console.WriteLine(endpointList[iEndpoint].ToString());
                            }
                        }
                    }
                }
            }
        }

        #endregion

        public static Boolean Connected = false;
        public static Boolean KaiEnabled = false;
        public static Boolean ZenEnabled = false;

        private static ColorTypes.RGB[] _kai;
        private static ColorTypes.RGB[] _zen;
        private static ColorTypes.RGB[] _kaiBuffer;
        private static ColorTypes.RGB[] _zenBuffer;

        public static ColorTypes.RGB[] Kai
        {
            get { return _kai ; }
            set { Array.Copy(value, _kai, 48);}
        }

        public static ColorTypes.RGB[] Zen
        {
            get { return _zen; }
            set { Array.Copy(value, _zen, 32);}
        }

        // Creates static arrays and initializes to default color;
        static Rainbow()
        {

            _kai = new ColorTypes.RGB[Globals.KaiLength];
            _zen = new ColorTypes.RGB[Globals.ZenLength];

            _kaiBuffer = new ColorTypes.RGB[Globals.KaiLength];
            _zenBuffer = new ColorTypes.RGB[Globals.ZenLength];

            for (int i = 0; i < Globals.KaiLength; i++)
            {
                _kai[i] = new ColorTypes.RGB();
                _kaiBuffer[i] = new ColorTypes.RGB();
            }

            for (int i = 0; i < Globals.ZenLength; i++)
            {
                _zen[i] = new ColorTypes.RGB();
                _zenBuffer[i] = new ColorTypes.RGB();
            }
        }

        #region Public Functions

        // Updates only different values to LED strip
        public static bool KaiUpdate()
        {
            return update(_kai, _kaiBuffer, Globals.KaiLength, 1);
        }
        public static bool ZenUpdate()
        {
            return update(_zen, _zenBuffer, Globals.ZenLength, 2);
        }

        // Gurantees position isn't out of bounds
        public static void KaiSet(int pos, ColorTypes.RGB rgb)
        {
            posClamp(ref pos, 1);
            _kai[pos] = rgb;
        }
        public static void ZenSet(int pos, ColorTypes.RGB rgb)
        {
            posClamp(ref pos, 2);
            _zen[pos] = rgb;
        }

        // Displays Arduino Buffer to reality
        public static bool KaiShow()
        {
            if (KaiEnabled)
            {
                return display(1);
            }
            else return false;
        }
        public static bool ZenShow()
        {
            if (ZenEnabled)
            {
                return display(2);
            }
            else return false;
        }

        // Clears Arduino's buffer and updates
        public static void KaiClear()
        {
            clear(1);
        }
        public static void ZenClear()
        {
            clear(2);
        }

        // Diagnostics function to determine which strip is which by color
        public static void reportStrip()
        {
            clear(1);
            clear(2);
            sendClr(0, new ColorTypes.RGB(127, 0, 0), 1);
            sendClr(0, new ColorTypes.RGB(0, 127, 0), 2);
            display(1);
            display(2);
        }
        public static void BuddySystem()
        {
            for (int i = 0; i < Globals.ZenLength; i++)
            {
                double zen_ratio = i;
                zen_ratio = zen_ratio * Globals.KaiLength / Globals.ZenLength;
                //Console.WriteLine("Kai[" + zen_ratio + "] Zen[" + i + "]");
                Zen[i] = Kai[(int)Math.Floor(zen_ratio)];
            }
            if (ZenUpdate())
                ZenShow();
        }

        #endregion

        #region Private Functions

        // Prevents bad values
        private static void posClamp(ref int pos, int strip)
        {
            if (pos < 0)
                pos = 0;
            if (strip == 1 && pos > Globals.KaiLength)
                pos = Globals.KaiLength;

            if (strip == 2 && pos > Globals.ZenLength)
                pos = Globals.ZenLength;
        }

        /*
        *  Used to push LED changes to strip.
        */
        private static bool update(ColorTypes.RGB[] main, ColorTypes.RGB[] buffer, int length, int strip)
        {
            bool hasChanged = false;
            for (int i = 0; i < length; i++)
            {
                if (main[i].different(buffer[i]))
                {
                    hasChanged = true;
                    sendClr(i, main[i], strip);
                }
            }
            Array.Copy(main, buffer, length);
            return hasChanged;
        }

        // Immediately updates the ardunio's buffer for the color
        private static bool sendClr(int pos, ColorTypes.RGB rgb, int strip)
        {
            posClamp(ref pos, 1);

            // Create color packet, add 64 for second led strip
            byte[] bytes = new byte[4];
            bytes[0] = (byte)pos;
            if (strip == 2) bytes[0] += (byte)64;
            bytes[1] = (byte)rgb.Red;
            bytes[2] = (byte)rgb.Green;
            bytes[3] = (byte)rgb.Blue;

            int bytesWritten = 0;
            ErrorCode ec = ErrorCode.None;

            if (Connected)
            {
                ec = writer.Write(bytes, 2000, out bytesWritten);
            }
            if (ec != ErrorCode.None)
            {
                throw new Exception(UsbDevice.LastErrorString);
            }

            return true;
        }

        // Sends Light up signal
        private static bool display(int strip)
        {
            byte[] b = new byte[4];
            b[0] = 128;
            if (strip == 2) b[0] += (byte)64;
            b[1] = 0;
            b[2] = 0;
            b[3] = 0;

            int bytesWritten;
            ErrorCode ec = ErrorCode.None;
            if (Connected)
            {
                ec = writer.Write(b, 2000, out bytesWritten);
            }
            if (ec != ErrorCode.None)
            {
                throw new Exception(UsbDevice.LastErrorString);
            }
            return true;
        }

        // Clears the leds
        private static void clear(int strip)
        {
            ColorTypes.RGB blank = new ColorTypes.RGB(0, 0, 0);

            if (strip == 1)
            {
                for (int i = 0; i < Globals.KaiLength; i++)
                {
                    sendClr(i, blank, 1);
                }
                display(1);
            }
            else if (strip == 2)
            {
                for (int i = 0; i < Globals.ZenLength; i++)
                {
                    sendClr(i, blank, 2);
                }
                display(2);
            }
        }

        #endregion

 
    } // class
}
