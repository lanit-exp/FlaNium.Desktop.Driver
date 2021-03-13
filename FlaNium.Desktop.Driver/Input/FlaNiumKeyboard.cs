namespace FlaNium.Desktop.Driver.Input
{
    #region using

    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    using global::FlaUI.Core.Input;
    using global::FlaUI.Core.WindowsAPI;
    using OpenQA.Selenium;

    
    #endregion

    internal class FlaNiumKeyboard
    {
        #region Fields

        private readonly KeyboardModifiers modifiers = new KeyboardModifiers();

        #endregion

        #region Constructors and Destructors
               

        #endregion

        #region Public Methods and Operators

        public void KeyDown(string keyToPress)
        {
            var key = KeyboardModifiers.GetVirtualKeyShort(keyToPress);
            this.modifiers.Add(keyToPress);
            
            Keyboard.Press(key);
        }

        public void KeyUp(string keyToRelease)
        {
            var key = KeyboardModifiers.GetVirtualKeyShort(keyToRelease);
            this.modifiers.Remove(keyToRelease);
            Keyboard.Release(key);
        }

        public void SendKeys(char[] keysToSend)
        {
            var builder = keysToSend.Select(key => new KeyEvent(key)).ToList();

            this.SendKeys(builder);
        }

        #endregion

        #region Methods

        protected void ReleaseModifiers()
        {
            var tmp = this.modifiers.ToList();

            foreach (var modifierKey in tmp)
            {
                this.KeyUp(modifierKey);
            }
        }

        private void PressOrReleaseModifier(string modifier)
        {
            if (this.modifiers.Contains(modifier))
            {
                this.KeyUp(modifier);
            }
            else
            {
                this.KeyDown(modifier);
            }
        }

        private void SendKeys(IEnumerable<KeyEvent> events)
        {
            foreach (var keyEvent in events)
            {
                if (keyEvent.IsNewLine())
                {
                    Keyboard.Type(VirtualKeyShort.ENTER);
                }
                else if (keyEvent.IsModifierRelease())
                {
                    this.ReleaseModifiers();
                }
                else if (keyEvent.IsModifier())
                {
                    this.PressOrReleaseModifier(keyEvent.GetKey());
                }
                else if (keyEvent.IsKey())
                {
                    Keyboard.Press(KeyboardModifiers.GetVirtualKeyShort(keyEvent.GetKey()));
                }
                else
                {
                    this.Type(keyEvent.GetCharacter());
                }
            }
        }

        private void Type(char key)
        {
            string str = Convert.ToString(key);

            if (this.modifiers.Contains(Keys.LeftShift) || this.modifiers.Contains(Keys.Shift))
            {
                str = str.ToUpper();
            }

            Keyboard.Type(str);
        }

        #endregion
    }
}
