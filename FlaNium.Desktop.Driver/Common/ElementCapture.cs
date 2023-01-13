using System;
using System.Drawing;
using System.Runtime.InteropServices;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Capturing;

namespace FlaNium.Desktop.Driver.Common {

    class ElementCapture {

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetWindowRect(IntPtr hwnd, out Rect lpRect);

        [DllImport("user32.dll")]
        private static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect {

            public int Left, Top, Right, Bottom;

        }


        public static CaptureImage CaptureImageOfElement(AutomationElement element) {
            var hwnd = element.Properties.NativeWindowHandle;

            GetWindowRect(hwnd, out var rect);

            var image = new Bitmap(rect.Right - rect.Left, rect.Bottom - rect.Top);

            using (var graphics = Graphics.FromImage(image)) {
                var hdcBitmap = graphics.GetHdc();
                PrintWindow(hwnd, hdcBitmap, 0);
                graphics.ReleaseHdc(hdcBitmap);
            }

            return new CaptureImage(image, Rectangle.FromLTRB(rect.Left, rect.Top, rect.Right, rect.Bottom), null);
        }

    }

}