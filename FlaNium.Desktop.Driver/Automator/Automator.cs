using System;
using WindowsInput;

namespace FlaNium.Desktop.Driver.Automator {

    internal class Automator {

        private static readonly object LockObject = new object();

        private static volatile Automator _instance;


        public Automator(string session) {
            this.Session = session;
            this.ElementsRegistry = new ElementsRegistry();
            this.InputSimulator = new InputSimulator();
        }


        public Capabilities ActualCapabilities { get; set; }
        public ElementsRegistry ElementsRegistry { get; }
        public string Session { get; }
        public InputSimulator InputSimulator { get; }

        
        public static Automator InstanceForSession(string sessionId) {
            if (_instance == null) {
                lock (LockObject) {
                    if (_instance == null) {
                        if (sessionId == null) {
                            sessionId = Guid.NewGuid().ToString();
                        }

                        _instance = new Automator(sessionId);
                    }
                }
            }

            return _instance;
        }

    }

}