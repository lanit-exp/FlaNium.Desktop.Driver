using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;

namespace FlaNium.Desktop.Driver.Input {

    public static class CustomInput {

        // Keyboard ====================================================================================================

        private static void KeyboardSendInput(ushort keyCode, bool isKeyDown, bool isScanCode, bool isExtended,
                                              bool isUnicode) {
            KEYBDINPUT keyboardInput = new KEYBDINPUT() {
                time = 0,
                dwExtraInfo = User32.GetMessageExtraInfo()
            };

            if (!isKeyDown)
                keyboardInput.dwFlags |= KeyEventFlags.KEYEVENTF_KEYUP;

            if (isScanCode) {
                keyboardInput.wScan = keyCode;
                keyboardInput.dwFlags |= KeyEventFlags.KEYEVENTF_SCANCODE;
                if (isExtended || (keyCode & 65280) == 224)
                    keyboardInput.dwFlags |= KeyEventFlags.KEYEVENTF_EXTENDEDKEY;
            }
            else if (isUnicode) {
                keyboardInput.dwFlags |= KeyEventFlags.KEYEVENTF_UNICODE;
                keyboardInput.wScan = keyCode;
            }
            else
                keyboardInput.wVk = keyCode;

            if (User32.SendInput(1U, new INPUT[1] { INPUT.KeyboardInput(keyboardInput) }, INPUT.Size) == 0U)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }


        private static void KeyboardKeyPress(string key, bool down) {
            VirtualKeyShort virtualKeyShort = KeyboardModifiers.GetVirtualKeyShort(key);

            short num = User32.VkKeyScan(key[0]);

            bool unicode = virtualKeyShort <= 0 && (num > 'þ' || num == -1);

            ushort keyCode = virtualKeyShort > 0 ? (ushort)virtualKeyShort : unicode ? key[0] : (ushort)num;

            KeyboardSendInput(keyCode, down, false, false, unicode);
        }

        public static void KeyboardKeyDown(string key) {
            KeyboardKeyPress(key, true);
        }

        public static void KeyboardKeyUp(string key) {
            KeyboardKeyPress(key, false);
        }


        // Mouse =======================================================================================================

        public static void MouseMove(Point startPoint, int dx, int dy, int duration) {
            Interpolation.Execute(point => { Mouse.Position = new Point(point.X, point.Y); },
                startPoint,
                new Point(startPoint.X + dx, startPoint.Y + dy),
                TimeSpan.FromMilliseconds(duration),
                Touch.DefaultInterval,
                false);
        }

        public static void MouseMove(int dx, int dy, int duration) {
            Point startPoint = Mouse.Position;

            Interpolation.Execute(point => { Mouse.Position = new Point(point.X, point.Y); },
                startPoint,
                new Point(startPoint.X + dx, startPoint.Y + dy),
                TimeSpan.FromMilliseconds(duration),
                Touch.DefaultInterval,
                true);
        }

    }

}