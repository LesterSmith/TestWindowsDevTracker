using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
//using System.DirectoryServices.AccountManagement;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Security.Permissions;
namespace TestWindowsDevTracker
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            //RegisterForPowerNotifications();
        }

        // all of the following code to the end of this class is for ** Start of Lid Action **
        [DllImport(@"User32", SetLastError = true, EntryPoint = "RegisterPowerSettingNotification",
            CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr RegisterPowerSettingNotification(IntPtr hRecipient, ref Guid PowerSettingGuid, Int32 Flags);

        static Guid GUID_LIDSWITCH_STATE_CHANGE = new Guid(0xBA3E0F4D, 0xB817, 0x4094, 0xA2, 0xD1, 0xD5, 0x63, 0x79, 0xE6, 0xA0, 0xF3);
        private const int DEVICE_NOTIFY_WINDOW_HANDLE = 0x00000000;
        private const int WM_POWERBROADCAST = 0x0218;
        const int PBT_POWERSETTINGCHANGE = 0x8013;

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        internal struct POWERBROADCAST_SETTING
        {
            public Guid PowerSetting;
            public uint DataLength;
            public byte Data;
        }

        private bool? _previousLidState = null;


        //public Fo(IntPtr handle)
        //{
        //    RegisterForPowerNotifications();

        //}

        [SecurityPermissionAttribute(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_POWERBROADCAST:
                    Console.WriteLine($"WndProc Message: {m}");
                    OnPowerBroadcast(m.WParam, m.LParam);
                    break;
                default:
                    break;
            }
            base.WndProc(ref m);
        }


        private void RegisterForPowerNotifications()
        {
            IntPtr handle = this.Handle;
            Debug.WriteLine("Handle: " + handle.ToString()); //If this line is omitted, then lastError = 1008 which is ERROR_NO_TOKEN, otherwise, lastError = 0
            IntPtr hLIDSWITCHSTATECHANGE = RegisterPowerSettingNotification(handle,
                 ref GUID_LIDSWITCH_STATE_CHANGE,
                 DEVICE_NOTIFY_WINDOW_HANDLE);
            Debug.WriteLine("Registered: " + hLIDSWITCHSTATECHANGE.ToString());
            Debug.WriteLine("LastError:" + Marshal.GetLastWin32Error().ToString());
        }

        private void OnPowerBroadcast(IntPtr wParam, IntPtr lParam)
        {
            Console.WriteLine($"OnPowerBroadcast wParam={wParam}, lParam={lParam}");
            if ((int)wParam == PBT_POWERSETTINGCHANGE)
            {
                POWERBROADCAST_SETTING ps = (POWERBROADCAST_SETTING)Marshal.PtrToStructure(lParam, typeof(POWERBROADCAST_SETTING));
                IntPtr pData = (IntPtr)((int)lParam + Marshal.SizeOf(ps));
                Int32 iData = (Int32)Marshal.PtrToStructure(pData, typeof(Int32));
                if (ps.PowerSetting == GUID_LIDSWITCH_STATE_CHANGE)
                {
                    Console.WriteLine($"ps.PowerSetting: {ps.PowerSetting}");
                    bool isLidOpen = ps.Data != 0;

                    if (!isLidOpen == _previousLidState)
                    {
                        LidStatusChanged(isLidOpen);
                    }

                    _previousLidState = isLidOpen;
                }
            }
        }

        private void LidStatusChanged(bool isLidOpen)
        {
            if (isLidOpen)
            {
                //Do some action on lid open event
                Console.WriteLine("Lid is now open");
            }
            else
            {
                //Do some action on lid close event
                Console.WriteLine("Lid is now closed");
            }
        }
        /// <summary>
        /// attempt to keep the form out of the taskbar
        /// </summary>
        /// <param name="value"></param>
        protected override void SetVisibleCore(bool value)
        {
            if (!IsHandleCreated)
            {
                this.CreateHandle();
                value = false;
            }
            base.SetVisibleCore(value);
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            RegisterForPowerNotifications();
        }

        // ** End of Lid Action **
    }
}
