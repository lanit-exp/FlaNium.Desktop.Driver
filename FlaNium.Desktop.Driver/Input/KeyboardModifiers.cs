namespace FlaNium.Desktop.Driver.Input
{
    #region using

    using System.Collections.Generic;
    using System.Linq;
    using global::FlaUI.Core.WindowsAPI;
    using OpenQA.Selenium;



    #endregion

    internal class KeyboardModifiers : List<string>
    {
        #region Static Fields

        private static readonly List<string> Modifiers = new List<string>
                                                             {
                                                                 Keys.Control,
                                                                 Keys.LeftControl,
                                                                 Keys.Shift,
                                                                 Keys.LeftShift,
                                                                 Keys.Alt,
                                                                 Keys.LeftAlt
                                                             };


        private static readonly Dictionary<string, VirtualKeyShort> KeysMap =
            new Dictionary<string, VirtualKeyShort>
                                                           {
                                                                { Keys.NumberPad0, VirtualKeyShort.NUMPAD0 },
                                                                { Keys.NumberPad1, VirtualKeyShort.NUMPAD1 },
                                                                { Keys.NumberPad2, VirtualKeyShort.NUMPAD2 },
                                                                { Keys.NumberPad3, VirtualKeyShort.NUMPAD3 },
                                                                { Keys.NumberPad4, VirtualKeyShort.NUMPAD4 },
                                                                { Keys.NumberPad5, VirtualKeyShort.NUMPAD5 },
                                                                { Keys.NumberPad6, VirtualKeyShort.NUMPAD6 },
                                                                { Keys.NumberPad7, VirtualKeyShort.NUMPAD7 },
                                                                { Keys.NumberPad8, VirtualKeyShort.NUMPAD8 },
                                                                { Keys.NumberPad9, VirtualKeyShort.NUMPAD9 },
                                                                { Keys.Multiply, VirtualKeyShort.MULTIPLY },
                                                                { Keys.Add, VirtualKeyShort.ADD },
                                                                { Keys.Separator, VirtualKeyShort.SEPARATOR },
                                                                { Keys.Subtract, VirtualKeyShort.SUBTRACT },
                                                                { Keys.Divide, VirtualKeyShort.DIVIDE },
                                                                { Keys.F1, VirtualKeyShort.F1 },
                                                                { Keys.F2, VirtualKeyShort.F2 },
                                                                { Keys.F3, VirtualKeyShort.F3 },
                                                                { Keys.F4, VirtualKeyShort.F4 },
                                                                { Keys.F5, VirtualKeyShort.F5 },
                                                                { Keys.F6, VirtualKeyShort.F6 },
                                                                { Keys.F7, VirtualKeyShort.F7 },
                                                                { Keys.F8, VirtualKeyShort.F8 },
                                                                { Keys.F9, VirtualKeyShort.F9 },
                                                                { Keys.F10, VirtualKeyShort.F10 },
                                                                { Keys.F11, VirtualKeyShort.F11 },
                                                                { Keys.F12, VirtualKeyShort.F12 },
                                                                { Keys.Decimal, VirtualKeyShort.DECIMAL },
                                                                { Keys.Insert, VirtualKeyShort.INSERT },
                                                                { Keys.Cancel, VirtualKeyShort.CANCEL },
                                                                { Keys.Help, VirtualKeyShort.HELP },
                                                                { Keys.Backspace, VirtualKeyShort.BACK },
                                                                { Keys.Tab, VirtualKeyShort.TAB },
                                                                { Keys.Clear, VirtualKeyShort.CLEAR },
                                                                { Keys.Return, VirtualKeyShort.RETURN },
                                                                { Keys.Enter, VirtualKeyShort.ENTER },
                                                                { Keys.Shift, VirtualKeyShort.SHIFT },
                                                                { Keys.Control, VirtualKeyShort.CONTROL },
                                                                { Keys.Alt, VirtualKeyShort.ALT },
                                                                { Keys.Delete, VirtualKeyShort.DELETE },
                                                                { Keys.Pause, VirtualKeyShort.PAUSE },
                                                                { Keys.Space, VirtualKeyShort.SPACE },
                                                                { Keys.End, VirtualKeyShort.END },
                                                                { Keys.Home, VirtualKeyShort.HOME },
                                                                { Keys.Left, VirtualKeyShort.LEFT },
                                                                { Keys.Up, VirtualKeyShort.UP },
                                                                { Keys.Right, VirtualKeyShort.RIGHT },
                                                                { Keys.Down, VirtualKeyShort.DOWN },
                                                                { Keys.Escape, VirtualKeyShort.ESCAPE },
                                                           };
          
        #endregion

        #region Public Methods and Operators

        public static string GetKeyFromUnicode(char key)
        {
            return KeysMap.Keys.ToList().Find(modifier => modifier[0] == key);
        }
                

        public static bool IsModifier(string key)
        {
            return Modifiers.Contains(key);
        }

        public static bool IsKey(string key)
        {
            if (key == null) return false;

            return KeysMap.ContainsKey(key);
        }

        public static VirtualKeyShort GetVirtualKeyShort(string key)
        {
            VirtualKeyShort virtualKey;

            if (KeysMap.TryGetValue(key, out virtualKey))
            {
                return virtualKey;
            }

            return default;
        }

        #endregion
    }
}
