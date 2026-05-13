using System;
using WindowsInput;

namespace FlaNium.Desktop.Driver.Automator {
    internal class Automator {

        private static Automator _instance;

        private Automator() {
            Session = Guid.NewGuid().ToString();
            ElementsRegistry = new ElementsRegistry();
            InputSimulator = new InputSimulator();
        }


        public Capabilities ActualCapabilities { get; set; }
        public ElementsRegistry ElementsRegistry { get; }
        public string Session { get; }
        public InputSimulator InputSimulator { get; }


        public static Automator GetInstance() {
            if (_instance == null) {
                _instance = new Automator();
            }

            return _instance;
        }

    }
}