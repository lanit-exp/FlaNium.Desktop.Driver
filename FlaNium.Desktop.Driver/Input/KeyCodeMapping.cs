using System.Collections.Generic;
using FlaUI.Core.WindowsAPI;
using OpenQA.Selenium;
using WindowsInput.Native;

namespace FlaNium.Desktop.Driver.Input {

    internal class KeyCodeMapping : List<string> {

        private static readonly Dictionary<string, VirtualKeyCode> KeysMap =
            new Dictionary<string, VirtualKeyCode> {
                { Keys.NumberPad0, VirtualKeyCode.NUMPAD0 },
                { Keys.NumberPad1, VirtualKeyCode.NUMPAD1 },
                { Keys.NumberPad2, VirtualKeyCode.NUMPAD2 },
                { Keys.NumberPad3, VirtualKeyCode.NUMPAD3 },
                { Keys.NumberPad4, VirtualKeyCode.NUMPAD4 },
                { Keys.NumberPad5, VirtualKeyCode.NUMPAD5 },
                { Keys.NumberPad6, VirtualKeyCode.NUMPAD6 },
                { Keys.NumberPad7, VirtualKeyCode.NUMPAD7 },
                { Keys.NumberPad8, VirtualKeyCode.NUMPAD8 },
                { Keys.NumberPad9, VirtualKeyCode.NUMPAD9 },
                { Keys.Multiply, VirtualKeyCode.MULTIPLY },
                { Keys.Add, VirtualKeyCode.ADD },
                { Keys.Separator, VirtualKeyCode.SEPARATOR },
                { Keys.Subtract, VirtualKeyCode.SUBTRACT },
                { Keys.Divide, VirtualKeyCode.DIVIDE },
                { Keys.F1, VirtualKeyCode.F1 },
                { Keys.F2, VirtualKeyCode.F2 },
                { Keys.F3, VirtualKeyCode.F3 },
                { Keys.F4, VirtualKeyCode.F4 },
                { Keys.F5, VirtualKeyCode.F5 },
                { Keys.F6, VirtualKeyCode.F6 },
                { Keys.F7, VirtualKeyCode.F7 },
                { Keys.F8, VirtualKeyCode.F8 },
                { Keys.F9, VirtualKeyCode.F9 },
                { Keys.F10, VirtualKeyCode.F10 },
                { Keys.F11, VirtualKeyCode.F11 },
                { Keys.F12, VirtualKeyCode.F12 },
                { Keys.Decimal, VirtualKeyCode.DECIMAL },
                { Keys.Insert, VirtualKeyCode.INSERT },
                { Keys.Cancel, VirtualKeyCode.CANCEL },
                { Keys.Help, VirtualKeyCode.HELP },
                { Keys.Backspace, VirtualKeyCode.BACK },
                { Keys.Tab, VirtualKeyCode.TAB },
                { Keys.Clear, VirtualKeyCode.CLEAR },
                { Keys.Return, VirtualKeyCode.RETURN },
                { Keys.Enter, VirtualKeyCode.RETURN },
                { Keys.Shift, VirtualKeyCode.SHIFT },
                { Keys.Control, VirtualKeyCode.CONTROL },
                { Keys.Alt, VirtualKeyCode.LMENU },
                { Keys.Delete, VirtualKeyCode.DELETE },
                { Keys.Pause, VirtualKeyCode.PAUSE },
                { Keys.Space, VirtualKeyCode.SPACE },
                { Keys.End, VirtualKeyCode.END },
                { Keys.Home, VirtualKeyCode.HOME },
                { Keys.Left, VirtualKeyCode.LEFT },
                { Keys.Up, VirtualKeyCode.UP },
                { Keys.Right, VirtualKeyCode.RIGHT },
                { Keys.Down, VirtualKeyCode.DOWN },
                { Keys.Escape, VirtualKeyCode.ESCAPE },
            };


        private static VirtualKeyCode GetVirtualKeyCode(string key) {
            VirtualKeyCode virtualKey;

            if (KeysMap.TryGetValue(key, out virtualKey)) {
                return virtualKey;
            }

            return default;
        }

        public static ushort GetVirtualKeyUshort(string key) {
            VirtualKeyCode virtualKeyShort = GetVirtualKeyCode(key);

            short num = User32.VkKeyScan(key[0]);

            bool unicode = virtualKeyShort <= 0 && (num > 'þ' || num == -1);

            ushort keyCode = virtualKeyShort > 0 ? (ushort)virtualKeyShort : unicode ? key[0] : (ushort)num;

            return keyCode;
        }

    }

}