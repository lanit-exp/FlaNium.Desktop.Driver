using OpenQA.Selenium;

namespace FlaNium.Desktop.Driver.Input {

    internal class KeyEvent {

        private readonly char character;

        private readonly string unicodeKey;


        public KeyEvent(char ch) {
            this.character = ch;
            this.unicodeKey = KeyboardModifiers.GetKeyFromUnicode(this.character);
        }


        public char GetCharacter() {
            return this.character;
        }

        public string GetKey() {
            return this.unicodeKey;
        }

        public bool IsModifier() {
            return KeyboardModifiers.IsModifier(this.unicodeKey);
        }

        public bool IsKey() {
            return KeyboardModifiers.IsKey(this.unicodeKey);
        }

        public bool IsModifierRelease() {
            return this.GetKey() == Keys.Null;
        }

        public bool IsNewLine() {
            return this.GetCharacter() == '\n';
        }

    }

}