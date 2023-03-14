using System;
using System.Runtime.InteropServices;

namespace FlaNium.Desktop.Driver.Input {

    public static class KeyboardLayout {

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool PostMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        private static extern int LoadKeyboardLayout(string pwszKlid, uint flags);

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hwnd, IntPtr process);

        [DllImport("user32.dll")]
        private static extern ushort GetKeyboardLayout(uint thread);


        public static bool SwitchInputLanguageToEng() {
            const string lang = "00000409"; //Eng

            return SwitchInputLanguage(lang);
        }

        public static bool SwitchInputLanguage(string lang) {
            int ret = LoadKeyboardLayout(lang, 1);

            return PostMessage(GetForegroundWindow(), 0x50, 1, ret);
        }

        public static string GetKeyboardLayout() {
            return $"{GetKeyboardLayout(GetWindowThreadProcessId(GetForegroundWindow(), IntPtr.Zero)):x8}";
        }

    }

}