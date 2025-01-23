using System;
using System.Runtime.InteropServices;
using FlaUI.Core.WindowsAPI;

namespace FlaNium.Desktop.Driver.CommandHelpers {

    public class WindowsAPIHelpers {

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern uint GetDpiForWindow(IntPtr hWnd);


        //--------------------------------------------------------------------------------------------------------------

        public static void SetConsoleWindowsForeground() {
            IntPtr consoleWindow = GetConsoleWindow();

            ShowWindow(consoleWindow, 9);
            SetForegroundWindow(consoleWindow);
        }

        public static double GetScale(IntPtr hWnd) {
            var dpi = GetDpiForWindow(hWnd);

            return dpi == 0 ? 1.0 : dpi / 96.0;
        }
        

        //--------------------------------------------------------------------------------------------------------------
        public static void WindowOnTop(IntPtr hWnd) {
            SetWindowPos(hWnd, IntPtr.Zero);
        }

        public static void WindowAlwaysOnTopEnable(IntPtr hWnd) {
            SetWindowPos(hWnd, new IntPtr(-1));
        }

        public static void WindowAlwaysOnTopDisable(IntPtr hWnd) {
            SetWindowPos(hWnd, new IntPtr(-2));
        }

        private static void SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter) {
            ShowWindow(hWnd, 5);
            SetForegroundWindow(hWnd);
            User32.SetWindowPos(hWnd, hWndInsertAfter, 0, 0, 0, 0, 0x0002 | 0x0001);
        }

    }

}