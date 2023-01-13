using System.Collections.Generic;
using FlaNium.Desktop.Driver.Input;

namespace FlaNium.Desktop.Driver.Automator {

    internal class Automator {

        private static readonly object LockObject = new object();

        private static volatile Automator _instance;


        public Automator(string session) {
            this.Session = session;
            this.ElementsRegistry = new ElementsRegistry();
            this.FlaNiumKeyboard = new FlaNiumKeyboard();
        }


        public Capabilities ActualCapabilities { get; set; }
        public ElementsRegistry ElementsRegistry { get; }
        public string Session { get; }
        public FlaNiumKeyboard FlaNiumKeyboard { get; }


        public static T GetValue<T>(IReadOnlyDictionary<string, object> parameters, string key) where T : class {
            object valueObject;
            parameters.TryGetValue(key, out valueObject);

            return valueObject as T;
        }

        public static Automator InstanceForSession(string sessionId) {
            if (_instance == null) {
                lock (LockObject) {
                    if (_instance == null) {
                        if (sessionId == null) {
                            sessionId = "AwesomeSession";
                        }

                        _instance = new Automator(sessionId);
                    }
                }
            }

            return _instance;
        }

    }

}