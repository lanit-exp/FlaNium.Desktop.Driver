using System;
using System.Drawing;
using System.Runtime.InteropServices;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Capturing;
using FlaUI.Core.WindowsAPI;

namespace FlaNium.Desktop.Driver.CommandHelpers {

    class CaptureActions {

        //----------------- захват изображения элемента напрямую -------------------------------------------------------

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

        //------------------- захват экрана ----------------------------------------------------------------------------

        public static CaptureImage Element(AutomationElement element, double scale = 1.0) =>
            CaptureRectangle(element.BoundingRectangle, scale);

        public static CaptureImage Screen(double scale = 1.0) =>
            CaptureRectangle(
                new Rectangle(User32.GetSystemMetrics(SystemMetric.SM_XVIRTUALSCREEN),
                    User32.GetSystemMetrics(SystemMetric.SM_YVIRTUALSCREEN),
                    User32.GetSystemMetrics(SystemMetric.SM_CXVIRTUALSCREEN),
                    User32.GetSystemMetrics(SystemMetric.SM_CYVIRTUALSCREEN)
                ), scale);

        //------------------------------------------
        private static CaptureImage CaptureRectangle(Rectangle bounds, double scale) {
            
            Bitmap bitmap;
            Rectangle sBounds;
            
            if (scale <= 1) {
                sBounds = bounds;
                
                bitmap = CaptureDesktopToBitmap(bounds.Width, bounds.Height, (dest, src) => {
                    Gdi32.BitBlt(dest,
                        0,
                        0,
                        bounds.Width,
                        bounds.Height,
                        src,
                        bounds.X,
                        bounds.Y,
                        CopyPixelOperation.SourceCopy | CopyPixelOperation.CaptureBlt);
                });
            }
            else {
                sBounds = ScaleBounds(bounds, scale);

                bitmap = CaptureDesktopToBitmap((int)(sBounds.Width / scale), (int)(sBounds.Height / scale),
                    (dest, src) => {
                        Gdi32.SetStretchBltMode(dest, StretchMode.STRETCH_HALFTONE);
                        Gdi32.StretchBlt(dest,
                            0,
                            0,
                            sBounds.Width,
                            sBounds.Height,
                            src,
                            sBounds.X,
                            sBounds.Y,
                            (int)(sBounds.Width * scale),
                            (int)(sBounds.Height * scale),
                            TernaryRasterOperations.SRCCOPY | TernaryRasterOperations.CAPTUREBLT);
                    });
            }
            
            return new CaptureImage(bitmap, sBounds, null);
        }

       
        private static Bitmap CaptureDesktopToBitmap(
            int width,
            int height,
            Action<IntPtr, IntPtr> action) {
            IntPtr desktopWindow = User32.GetDesktopWindow();
            IntPtr windowDc = User32.GetWindowDC(desktopWindow);
            IntPtr compatibleDc = Gdi32.CreateCompatibleDC(windowDc);
            IntPtr compatibleBitmap = Gdi32.CreateCompatibleBitmap(windowDc, width, height);
            IntPtr bmp = Gdi32.SelectObject(compatibleDc, compatibleBitmap);
            action(compatibleDc, windowDc);
            Bitmap bitmap = Image.FromHbitmap(compatibleBitmap);
            Gdi32.SelectObject(compatibleDc, bmp);
            Gdi32.DeleteObject(compatibleBitmap);
            Gdi32.DeleteDC(compatibleDc);
            User32.ReleaseDC(desktopWindow, windowDc);

            return bitmap;
        }

        private static Rectangle ScaleBounds(Rectangle bounds, double scale) {
            return new Rectangle(
                (int)(bounds.X * scale),
                (int)(bounds.Y * scale),
                (int)(bounds.Width * scale),
                (int)(bounds.Height * scale));
        }

        //--------------------------------------------------------------------------------------------------------------
    }

}