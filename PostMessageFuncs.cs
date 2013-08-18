using System;
using System.Text;
using System.Threading;
using System.Collections;
using System.Runtime.InteropServices;

namespace PostMessageFuncs
{
    /// <summary>
    /// Sends keystrokes to the specified window
    /// </summary>
    public class PostMessage
    {
        [DllImport("user32.dll", EntryPoint = "PostMessage")]
        private static extern int _PostMessage(int hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", EntryPoint = "PostMessage")]
        private static extern int _PostMessage(int hWnd, int msg, int wParam, uint lParam);
        [DllImport("user32.dll", EntryPoint = "MapVirtualKey")]
        private static extern int _MapVirtualKey(int uCode, int uMapType);

        /// <summary>
        /// Sends keystrokes to the specified window
        /// </summary>
        /// <param name="hWnd">Window's hWnd</param>
        /// <param name="keys">String of keys to send</param>
        /// <returns>Returns number of keystrokes sent, -1 if an error occurs</returns>
        public int SendKeys(int hWnd, string keys)
        {
            if (hWnd <= 0 || keys.Length == 0)
                return -1;

            int ret = 0, i = 0;

            System.Text.StringBuilder str = new System.Text.StringBuilder(keys.ToUpper());

            str.Replace(Convert.ToChar("`"), Convert.ToChar(0xC0));
            str.Replace(Convert.ToChar("~"), Convert.ToChar(0xC0));
            str.Replace(Convert.ToChar("-"), Convert.ToChar(0xBD));
            str.Replace(Convert.ToChar("="), Convert.ToChar(0xBB));
            str.Replace(Convert.ToChar("^"), Convert.ToChar(0x11));
            str.Replace(Convert.ToChar("'"), Convert.ToChar(0xDE));
            str.Replace("{TAB}", Convert.ToChar(0x9).ToString());
            str.Replace("{ENTER}", Convert.ToChar(0xD).ToString());
            str.Replace("{ESC}", Convert.ToChar(0x1B).ToString());
            str.Replace("{DEL}", Convert.ToChar(0x2E).ToString());
            str.Replace("{F5}", Convert.ToChar(0x74).ToString());
            str.Replace("{F12}", Convert.ToChar(0x7B).ToString());
            str.Replace("{SHIFTD}", Convert.ToChar(0xC1).ToString());
            str.Replace("{SHIFTU}", Convert.ToChar(0xC2).ToString());
            str.Replace("{BACK}", Convert.ToChar(0x08).ToString());
            str.Replace("{HOME}", Convert.ToChar(0x24).ToString());

            for (int ix = 1; ix <= str.Length; ++ix)
            {
                char chr = str[i];

                if (Convert.ToInt32(chr) == 0xC1)
                {
                    _PostMessage(hWnd, 0x100, 0x10, 0x002A0001);
                    _PostMessage(hWnd, 0x100, 0x10, 0x402A0001);
                    Thread.Sleep(1);
                }
                else if (Convert.ToInt32(chr) == 0xC2)
                {
                    _PostMessage(hWnd, 0x101, 0x10, 0xC02A0001);
                    Thread.Sleep(1);
                }
                else
                {
                    ret = _MapVirtualKey(Convert.ToInt32(chr), 0);
                    if (_PostMessage(hWnd, 0x100, Convert.ToInt32(chr), MakeLong(1, ret)) == 0)
                        return -1;

                    Thread.Sleep(1);

                    if (_PostMessage(hWnd, 0x101, Convert.ToInt32(chr), (MakeLong(1, ret) + 0xC0000000)) == 0)
                        return -1;
                }
                i++;
            }
            return i;
        }

        /// <summary>
        /// Sends characters to the specified window
        /// </summary>
        /// <param name="hWnd">Window's hWnd</param>
        /// <param name="keys">String of chars to send</param>
        /// <returns>Returns number of chars sent, -1 if an error occurs</returns>
        public int SendChars(int hWnd, string keys)
        {
            if (hWnd <= 0 || keys.Length == 0)
                return -1;

            int i = 0;

            for (int ix = 1; ix <= keys.Length; ++ix)
            {
                char chr = keys[i];
                //0x0102 WM_CHAR
                if(_PostMessage(hWnd, 0x0102, Convert.ToInt32(chr), 0) == 0)
                    return -1;
                Thread.Sleep(1);
                i++;
            }

            return i;
        }

        /// <summary>
        /// Send one mouse click to specified window
        /// </summary>
        /// <param name="hWnd">Window's hWnd</param>
        /// <param name="button">Button that will be clicked (left or right)</param>
        /// <param name="x">X Coordinate on which to click</param>
        /// <param name="y">Y Coordinate on which to click</param>
        /// <returns>Returns true if successful, false if otherwise</returns>
        public bool MouseClick(int hWnd, string button, int x, int y)
        {
            //If hWnd is 0, return false
            if (hWnd <= 0)
                return false;

            //If string in parameter 2 isn't either "left" or "right"
            if (button.ToLower() != "left" && button.ToLower() != "right")
                return false;

            //Post the WM_MOUSEMOVE message
            if (_PostMessage(hWnd, 0x200, 0, MakeLong(x, y)) == 0)
                return false;

            //Figure out which button to click
            if (button.ToLower() == "left")
            {
                //Post the WM_LBUTTONDOWN message
                if (_PostMessage(hWnd, 0x201, 1, MakeLong(x, y)) == 0)
                    return false;

                //Post the WM_LBUTTONUP message
                if (_PostMessage(hWnd, 0x202, 0, MakeLong(x, y)) == 0)
                    return false;
            }
            else
            {
                //Post the WM_RBUTTONDOWN message
                if (_PostMessage(hWnd, 0x204, 2, MakeLong(x, y)) == 0)
                    return false;

                //Post the WM_RBUTTONUP message
                if (_PostMessage(hWnd, 0x205, 0, MakeLong(x, y)) == 0)
                    return false;
            }

            return true;
        }

        public bool MouseMove(int hWnd, int x, int y)
        {
            //If hWnd is 0, return false
            if (hWnd <= 0)
                return false;

            if (_PostMessage(hWnd, 0x0200, 0, MakeLong(x, y)) == 0)
                return false;

            return true;

        }

        public bool MouseScroll(int hWnd, string scrollDirection, int X, int Y)
        {
            IntPtr scrolldown = (IntPtr)((-120 << 16));
            IntPtr scrollup = (IntPtr)((120 << 16));

            //If hWnd is 0, return false
            if (hWnd <= 0)
                return false;

            if (!MouseMove(hWnd, X, Y))
                return false;

            if (scrollDirection.ToLower() == "down")
            {
                if (_PostMessage(hWnd, 0x20A, scrolldown, IntPtr.Zero) == 0)
                    return false;
            }
            else
            {
                if (_PostMessage(hWnd, 0x20A, scrollup, IntPtr.Zero) == 0)
                    return false;
            }

            return true;
        }

		public bool HoldKey(int hWnd, string holdkey, string key)
        {
            //If hWnd is 0 or parameter two is incorrect, return false
            if (hWnd <= 0 ||
                (holdkey.ToLower() != "alt" &&
                holdkey.ToLower() != "shift" &&
                holdkey.ToLower() != "ctrl"))
                return false;

            int wParam;
            uint lParam;

            //Set up wParam and lParam based upon which button needs pressing
            switch (holdkey.ToLower())
            {
                case "alt":
                    wParam = 0x12;
                    lParam = 0;
                    break;

                case "shift":
                    wParam = 0x11;
                    lParam = 0;
                    break;

                case "ctrl":
                    wParam = 0x02;
                    lParam = 0;
                    break;

                default:
                    return false;
            }
			
            //Post the WM_KEYDOWN message, return false if unsuccessful
            if (_PostMessage(hWnd, 0x100, wParam, lParam) == 0)
                return false;

            //Sleep for half a second to emulate the delay you get when you hold a key down on your keyboard
            Thread.Sleep(500);

            
            //Post the WM_KEYDOWN message with the repeat flag turned on, return false if unsuccessful
            if (SendKeys(hWnd, key) == 0)
                return false;

            //Sleep for 1/20th of a second between posting the message
            Thread.Sleep(50);
				
            //Post the WM_KEYUP message, return false if unsuccessful
            if (_PostMessage(hWnd, 0x101, wParam, (lParam + 0xC0000000)) == 0)
                return false;

            return true;
        }


        /// <summary>
        /// Create the lParam for PostMessage
        /// </summary>
        /// <param name="a">HiWord</param>
        /// <param name="b">LoWord</param>
        /// <returns>Returns the long value</returns>
        private static uint MakeLong(int a, int b)
        {
            return (uint)((uint)((ushort)(a)) | ((uint)((ushort)(b) << 16)));
        }
    }

    /// <summary>
    /// Gather information about windows using IsWindowVisible and GetWindowText API
    /// </summary>
    public class WindowInfo
    {
        [DllImport("user32.dll", EntryPoint = "IsWindowVisible")]
        private static extern bool _IsWindowVisible(int hWnd);
        [DllImport("user32.dll", EntryPoint = "GetWindowText")]
        private static extern int _GetWindowText(int hWnd, StringBuilder buf, int nMaxCount);

        /// <summary>
        /// Returns the window title of the specified window
        /// </summary>
        /// <param name="hWnd">A handle to the window</param>
        /// <param name="length">Length of the string to be returned</param>
        /// <returns></returns>
        public string GetWindowTitle(int hWnd, int length)
        {
            StringBuilder str = new StringBuilder(length);
            _GetWindowText(hWnd, str, length);
            return str.ToString();
        }

        /// <summary>
        /// Returns true if the window is visible, false if not
        /// </summary>
        /// <param name="hWnd">A handle to the window</param>
        /// <returns></returns>
        public bool IsVisible(int hWnd)
        {
            return _IsWindowVisible(hWnd);
        }
    }
    /// <summary>
    /// Enumerate open windows
    /// </summary>
    public class WindowArray : ArrayList
    {
        private delegate bool EnumWindowsCB(int handle, IntPtr param);

        [DllImport("user32")]
        private static extern int EnumWindows(EnumWindowsCB cb,
            IntPtr param);

        private static bool MyEnumWindowsCB(int hwnd, IntPtr param)
        {
            GCHandle gch = (GCHandle)param;
            WindowArray itw = (WindowArray)gch.Target;
            itw.Add(hwnd);
            return true;
        }

        /// <summary>
        /// Returns an array of all open windows and their hWnds
        /// </summary>
        public WindowArray()
        {
            GCHandle gch = GCHandle.Alloc(this);
            EnumWindowsCB ewcb = new EnumWindowsCB(MyEnumWindowsCB);
            EnumWindows(ewcb, (IntPtr)gch);
            gch.Free();
        }
    }
}