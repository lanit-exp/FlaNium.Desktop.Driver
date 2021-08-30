
namespace FlaNium.Desktop.Driver.Common
{
    using System;
    using System.Runtime.InteropServices;
    using System.Drawing;
    using global::FlaUI.Core.AutomationElements;
    using global::FlaUI.Core.Capturing;

    class ElementCapture
    {

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("user32.dll")]
        private static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left, Top, Right, Bottom;
        }


        public static CaptureImage CaptureImageOfElement(AutomationElement element)
        {
            var hwnd = element.Properties.NativeWindowHandle;

            GetWindowRect(hwnd, out var rect);

            var image = new Bitmap(rect.Right - rect.Left, rect.Bottom - rect.Top);

            using (var graphics = Graphics.FromImage(image))
            {
                var hdcBitmap = graphics.GetHdc();
                PrintWindow(hwnd, hdcBitmap, 0);
                graphics.ReleaseHdc(hdcBitmap);
            }

            return new CaptureImage(image, Rectangle.FromLTRB(rect.Left, rect.Top, rect.Right, rect.Bottom), null);
        }

    }
}
