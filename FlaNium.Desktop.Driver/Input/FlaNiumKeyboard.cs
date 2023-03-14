using System;
using System.Collections.Generic;
using System.Linq;
using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;
using OpenQA.Selenium;

namespace FlaNium.Desktop.Driver.Input {

    internal class FlaNiumKeyboard {

        private readonly KeyboardModifiers modifiers = new KeyboardModifiers();


        private void KeyDown(string keyToPress) {
            var key = KeyboardModifiers.GetVirtualKeyShort(keyToPress);
            this.modifiers.Add(keyToPress);
            Keyboard.Press(key);
        }

        private void KeyUp(string keyToRelease) {
            var key = KeyboardModifiers.GetVirtualKeyShort(keyToRelease);
            this.modifiers.Remove(keyToRelease);
            Keyboard.Release(key);
        }


        public void SendKeys(char[] keysToSend) {
            var builder = keysToSend.Select(key => new KeyEvent(key)).ToList();

            this.SendKeys(builder);
        }


        private void ReleaseModifiers() {
            var tmp = this.modifiers.ToList();

            foreach (var modifierKey in tmp) {
                this.KeyUp(modifierKey);
            }
        }

        private void PressOrReleaseModifier(string modifier) {
            if (this.modifiers.Contains(modifier)) {
                this.KeyUp(modifier);
            }
            else {
                this.KeyDown(modifier);
            }
        }

        private void SendKeys(IEnumerable<KeyEvent> events) {
            foreach (var keyEvent in events) {
                if (keyEvent.IsNewLine()) {
                    Keyboard.Type(VirtualKeyShort.ENTER);
                }
                else if (keyEvent.IsModifierRelease()) {
                    this.ReleaseModifiers();
                }
                else if (keyEvent.IsModifier()) {
                    this.PressOrReleaseModifier(keyEvent.GetKey());
                }
                else if (keyEvent.IsKey()) {
                    Keyboard.Press(KeyboardModifiers.GetVirtualKeyShort(keyEvent.GetKey()));
                }
                else {
                    this.Type(keyEvent.GetCharacter());
                }
            }
        }

        private void Type(char key) {
            string str = Convert.ToString(key);

            if (this.modifiers.Contains(Keys.LeftShift) || this.modifiers.Contains(Keys.Shift)) {
                str = str.ToUpper();
            }

            Keyboard.Type(str);
        }

    }

}