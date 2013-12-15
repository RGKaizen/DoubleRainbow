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
        public static UsbDevice MyUsbDevice;

        #region SET YOUR USB Vendor and Product ID!

        public static UsbDeviceFinder MyUsbFinder = new UsbDeviceFinder(9025, 32822);

        #endregion

        static UsbEndpointWriter writer = null;

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

        public static void ConfigTest()
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

        #region public functions

        // Use when modifying _kai directly
        public static bool KaiUpdate()
        {
            bool hasChanged = false;
            for (int i = 0; i < Globals.KaiLength; i++)
            {
                if (_kai[i].different(_kaiBuffer[i]))
                {
                    hasChanged = true;
                    KaiLight(i, _kai[i]);
                }
            }
            copyToBuffer(1);
            return hasChanged;
        }

        // Updates _zen from _zenBuffer
        public static bool ZenUpdate()
        {
            bool hasChanged = false;
            for (int i = 0; i < Globals.ZenLength; i++)
            {
                if (_zen[i].different(_zenBuffer[i]))
                {
                    hasChanged = true;
                    ZenLight(i, _zen[i]);
                }
            }
            copyToBuffer(2);
            return hasChanged;
        }

        // LED So _kai
        // Position and ActiveColor
        // Truely lit alive
        public static bool KaiLight(int pos, ColorTypes.RGB rgb)
        {
            posClamp(ref pos, 1);

            byte[] bytes = new byte[4];

            bytes[0] = (byte)pos;
            bytes[1] = (byte)rgb.Red;
            bytes[2] = (byte)rgb.Green;
            bytes[3] = (byte)rgb.Blue;

            sendString(bytes);
            return true;
        }

        // Lights up _zen.
        public static bool ZenLight(int pos, ColorTypes.RGB rgb)
        {
            posClamp(ref pos, 1);

            byte[] bytes = new byte[4];

            bytes[0] = (byte)pos;

            // Select Zen
            bytes[0] += (byte)64;

            bytes[1] = (byte)rgb.Red;
            bytes[2] = (byte)rgb.Green;
            bytes[3] = (byte)rgb.Blue;

            sendString(bytes);
            return true;
        }

        // Displays Arduino Buffer to reality
        public static bool KaiShow()
        {
            return display(1);
        }
        public static bool ZenShow()
        {
            return display(2);
        }

        // Clears Arduino's buffer and demonstrates
        public static void KaiClear()
        {
            clear(1);
        }
        public static void ZenClear()
        {
            clear(2);
        }

        // Prevents bad values
        public static void posClamp(ref int pos, int strip)
        {
            if (pos < 0)
                pos = 0;
            if (strip == 1 && pos > Globals.KaiLength)
                pos = Globals.KaiLength;

            if (strip == 2 && pos > Globals.ZenLength)
                pos = Globals.ZenLength;
        }

        // Diagnostics function to determine which strip is which by color
        public static void reportStrip()
        {
            clear(1);
            clear(2);
            KaiLight(0, new ColorTypes.RGB(127, 0, 0));
            ZenLight(0, new ColorTypes.RGB(0, 127, 0));
            display(1);
            display(2);
        }

        #endregion

        #region private functions

        private static void copyToBuffer(int strip)
        {
            if (strip == 1)
            {
                Array.Copy(_kai, _kaiBuffer, Globals.KaiLength);
            }
            else if (strip == 2)
            {
                Array.Copy(_zen, _zenBuffer, Globals.ZenLength);
            }
        }

        public static void sendString(Byte[] array)
        {
            int bytesWritten = 0;
            ErrorCode ec = ErrorCode.None;

            for(int i= 0 ; i< array.Length; i++)
            {
                Console.WriteLine(Convert.ToString(array[i], 2));
            }

            if (Connected)
            {
                ec = writer.Write(array, 2000, out bytesWritten);
            }

            Console.WriteLine("Bytes written: " + bytesWritten);

            if (ec != ErrorCode.None)
            {
                throw new Exception(UsbDevice.LastErrorString);
            }
        }

        // Sends Light up signal
        private static bool display(int strip)
        {
            int bytesWritten;
            ErrorCode ec = ErrorCode.None;
            if (Connected)
            {
                if (strip == 1)
                {
                    //Console.WriteLine("Kai Lit");
                    ec = writer.Write(KaiOnByte(), 2000, out bytesWritten);
                }
                else if (strip == 2)
                {
                    //Console.WriteLine("Zen Lit");
                    ec = writer.Write(ZenOnByte(), 2000, out bytesWritten);
                }
            }
            if (ec != ErrorCode.None)
            {
                throw new Exception(UsbDevice.LastErrorString);
            }
            return true;
        }

        private static byte[] KaiOnByte()
        {
            byte[] b = new byte[4];
            b[0] = 128;
            b[1] = 0;
            b[2] = 0;
            b[3] = 0;
            return b;
        }

        private static byte[] ZenOnByte()
        {
            byte[] b = new byte[4];
            b[0] = 128 + 64;
            b[1] = 0;
            b[2] = 0;
            b[3] = 0;
            return b;
        }

        public static void BuddySystem()
        {
            for (int i = 0; i < Globals.ZenLength; i++)
            {
                Zen[i] = Kai[i];
            }
            if(ZenUpdate())
                ZenShow();
        }

        // Clears the leds
        private static void clear(int strip)
        {
            ColorTypes.RGB blank = new ColorTypes.RGB(0, 0, 0);

            if (strip == 1)
            {
                for (int i = 0; i < Globals.KaiLength; i++)
                {
                    KaiLight(i, blank);
                }
                display(1);
            }
            else if (strip == 2)
            {
                for (int i = 0; i < Globals.ZenLength; i++)
                {
                    ZenLight(i, blank);
                }
                display(2);
            }
        }

        #endregion

 
    } // class
}
