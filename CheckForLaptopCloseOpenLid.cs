using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.DirectoryServices.AccountManagement;
using System.ComponentModel;
using Microsoft.Win32;
//namespace TestWindowsDevTracker
//{
//    public partial class CheckForLaptopCloseOpenLid
//    { 
//        [DllImport(@"User32", SetLastError = true, EntryPoint = "RegisterPowerSettingNotification",
//            CallingConvention = CallingConvention.StdCall)]
//        private static extern IntPtr RegisterPowerSettingNotification(IntPtr hRecipient, ref Guid PowerSettingGuid, Int32 Flags);

//        static Guid GUID_LIDSWITCH_STATE_CHANGE = new Guid(0xBA3E0F4D, 0xB817, 0x4094, 0xA2, 0xD1, 0xD5, 0x63, 0x79, 0xE6, 0xA0, 0xF3);
//        private const int DEVICE_NOTIFY_WINDOW_HANDLE = 0x00000000;
//        private const int WM_POWERBROADCAST = 0x0218;
//        const int PBT_POWERSETTINGCHANGE = 0x8013;

//        [StructLayout(LayoutKind.Sequential, Pack = 4)]
//        internal struct POWERBROADCAST_SETTING
//        {
//            public Guid PowerSetting;
//            public uint DataLength;
//            public byte Data;
//        }

//        private bool? _previousLidState = null;


//        public CheckForLaptopCloseOpenLid(IntPtr handle)
//        {
//            RegisterForPowerNotifications();

//        }

//        [SecurityPermissionAttribute(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
//        protected override void WndProc(ref Message m)
//        {
//            switch (m.Msg)
//            {
//                case WM_POWERBROADCAST:
//                    OnPowerBroadcast(m.WParam, m.LParam);
//                    break;
//                default:
//                    break;
//            }
//            base.WndProc(ref m);
//        }


//        private void RegisterForPowerNotifications()
//        {
//            IntPtr handle = this.Handle;
//            Debug.WriteLine("Handle: " + handle.ToString()); //If this line is omitted, then lastError = 1008 which is ERROR_NO_TOKEN, otherwise, lastError = 0
//            IntPtr hLIDSWITCHSTATECHANGE = RegisterPowerSettingNotification(handle,
//                 ref GUID_LIDSWITCH_STATE_CHANGE,
//                 DEVICE_NOTIFY_WINDOW_HANDLE);
//            Debug.WriteLine("Registered: " + hLIDSWITCHSTATECHANGE.ToString());
//            Debug.WriteLine("LastError:" + Marshal.GetLastWin32Error().ToString());
//        }

//        private void OnPowerBroadcast(IntPtr wParam, IntPtr lParam)
//        {
//            if ((int)wParam == PBT_POWERSETTINGCHANGE)
//            {
//                POWERBROADCAST_SETTING ps = (POWERBROADCAST_SETTING)Marshal.PtrToStructure(lParam, typeof(POWERBROADCAST_SETTING));
//                IntPtr pData = (IntPtr)((int)lParam + Marshal.SizeOf(ps));
//                Int32 iData = (Int32)Marshal.PtrToStructure(pData, typeof(Int32));
//                if (ps.PowerSetting == GUID_LIDSWITCH_STATE_CHANGE)
//                {
//                    bool isLidOpen = ps.Data != 0;

//                    if (!isLidOpen == _previousLidState)
//                    {
//                        LidStatusChanged(isLidOpen);
//                    }

//                    _previousLidState = isLidOpen;
//                }
//            }
//        }

//        private void LidStatusChanged(bool isLidOpen)
//        {
//            if (isLidOpen)
//            {
//                //Do some action on lid open event
//                MessageBox.Show("Lid is now open");
//            }
//            else
//            {
//                //Do some action on lid close event
//                MessageBox.Show("Lid is now closed");
//            }
//        }
//    }
//}    




    //public partial class CheckForLaptopCloseOpenLid //: Window
    //{
    //    [DllImport("user32.dll")]
    //    static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);
        
    //    [DllImport(@"User32", SetLastError = true, EntryPoint = "RegisterPowerSettingNotification",
    //        CallingConvention = CallingConvention.StdCall)]
    //    private static extern IntPtr RegisterPowerSettingNotification(IntPtr hRecipient, ref Guid PowerSettingGuid,
    //        Int32 Flags);
                
    //    delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);
    //    IntPtr m_hhook = IntPtr.Zero;
    //    private const uint WINEVENT_OUTOFCONTEXT = 0;

    //    WinEventDelegate dele = null;
    //    internal struct POWERBROADCAST_SETTING
    //    {
    //        public Guid PowerSetting;
    //        public uint DataLength;
    //        public byte Data;
    //    }

    //    Guid GUID_LIDSWITCH_STATE_CHANGE = new Guid(0xBA3E0F4D, 0xB817, 0x4094, 0xA2, 0xD1, 0xD5, 0x63, 0x79, 0xE6, 0xA0, 0xF3);
    //    const int DEVICE_NOTIFY_WINDOW_HANDLE = 0x00000000;
    //    const int WM_POWERBROADCAST = 0x0218;
    //    const int PBT_POWERSETTINGCHANGE = 0x8013;

    //    private bool? _previousLidState = null;

    //    public CheckForLaptopCloseOpenLid(IntPtr hwnd)
    //    {
    //        RegisterForPowerNotifications(hwnd);
    //        //IntPtr hwnd = new WindowInteropHelper(this).Handle;
    //        //HwndSource.FromHwnd(hwnd).AddHook(new HwndSourceHook(WndProc));
    //        m_hhook = SetWinEventHook(WM_POWERBROADCAST, WM_POWERBROADCAST, IntPtr.Zero, dele, 0, 0, WINEVENT_OUTOFCONTEXT);
    //    }

    //    //IntPtr handle = new WindowInteropHelper(Application.Current.Windows[0]).Handle; to this: IntPtr handle = new WindowInteropHelper(this).Handle;
    //    private void RegisterForPowerNotifications(IntPtr handle)
    //    {
    //        Console.WriteLine($"RegisterForPowerNotifications {handle}");
    //        //IntPtr handle = new WindowInteropHelper(Application.Current.Windows[0]).Handle;
    //        //IntPtr handle = this.//new WindowInteropHelper(this).Handle;
    //        IntPtr hLIDSWITCHSTATECHANGE = RegisterPowerSettingNotification(handle,
    //             ref GUID_LIDSWITCH_STATE_CHANGE,
    //             DEVICE_NOTIFY_WINDOW_HANDLE);
    //    }

    //    IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    //    {
    //        Console.WriteLine($"WParam {wParam}, LParam {lParam}, msg {msg}");
    //        switch (msg)
    //        {
    //            case WM_POWERBROADCAST:
    //                OnPowerBroadcast(wParam, lParam);
    //                break;
    //            default:
    //                break;
    //        }
    //        return IntPtr.Zero;
    //    }

    //    //[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    //    //protected override IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled/*ref Message m*/)
    //    //{
    //    //    // Listen for operating system messages.
    //    //    switch (m.Msg)
    //    //    {
    //    //        // The WM_ACTIVATEAPP message occurs when the application
    //    //        // becomes the active application or becomes inactive.
    //    //        case WM_ACTIVATEAPP:

    //    //            // The WParam value identifies what is occurring.
    //    //            appActive = (((int)m.WParam != 0));

    //    //            // Invalidate to get new text painted.
    //    //            this.Invalidate();

    //    //            break;
    //    //    }
    //    //    base.WndProc(ref m);
    //    //    return (IntPtr)0;
    //    //}

    //    private void OnPowerBroadcast(IntPtr wParam, IntPtr lParam)
    //    {
    //        if ((int)wParam == PBT_POWERSETTINGCHANGE)
    //        {
    //            POWERBROADCAST_SETTING ps = (POWERBROADCAST_SETTING)Marshal.PtrToStructure(lParam, typeof(POWERBROADCAST_SETTING));
    //            IntPtr pData = (IntPtr)((int)lParam + Marshal.SizeOf(ps));
    //            Int32 iData = (Int32)Marshal.PtrToStructure(pData, typeof(Int32));
    //            if (ps.PowerSetting == GUID_LIDSWITCH_STATE_CHANGE)
    //            {
    //                bool isLidOpen = ps.Data != 0;

    //                if (!isLidOpen == _previousLidState)
    //                {
    //                    LidStatusChanged(isLidOpen);
    //                }

    //                _previousLidState = isLidOpen;
    //            }
    //        }
    //    }

    //    private void LidStatusChanged(bool isLidOpen)
    //    {
    //        if (isLidOpen)
    //        {
    //            //Do some action on lid open event
    //            Debug.WriteLine("{0}: Lid opened!", DateTime.Now);
    //        }
    //        else
    //        {
    //            //Do some action on lid close event
    //            Debug.WriteLine("{0}: Lid closed!", DateTime.Now);
    //        }
    //    }

    //}
//}
