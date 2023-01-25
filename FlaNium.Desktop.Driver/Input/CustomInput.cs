using System.ComponentModel;
using System.Runtime.InteropServices;
using FlaUI.Core.WindowsAPI;

namespace FlaNium.Desktop.Driver.Input {

    public static class CustomInput {

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


        private static void KeyboardKeyPress(char key, bool down) {
            short num = User32.VkKeyScan(key);

            if (num > 'þ' || num == -1) {
                KeyboardSendInput(key, down, false, false, true);
            }
            else {
                byte keyCode = (byte)((uint)num & byte.MaxValue);

                KeyboardSendInput(keyCode, down, false, false, false);
            }
        }

        public static void KeyboardKeyDown(char key) {
            KeyboardKeyPress(key, true);
        }

        public static void KeyboardKeyUp(char key) {
            KeyboardKeyPress(key, false);
        }

    }

}