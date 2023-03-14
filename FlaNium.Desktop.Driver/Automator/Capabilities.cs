using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FlaNium.Desktop.Driver.Automator {

    internal class Capabilities {

        internal Capabilities() {
            this.App = string.Empty;
            this.InjectionDllType = string.Empty;
            this.Arguments = string.Empty;
            this.LaunchDelay = 2000;
            this.ProcessFindTimeOut = 30000;
            this.ConnectToRunningApp = false;
            this.InjectionActivate = false;
            this.ProcessName = string.Empty;
            this.ResponseTimeout = 300000;
        }


        [JsonProperty("app")] public string App { get; set; }

        [JsonProperty("args")] public string Arguments { get; set; }

        [JsonProperty("connectToRunningApp")] public bool ConnectToRunningApp { get; set; }

        [JsonProperty("launchDelay")] public int LaunchDelay { get; set; }

        [JsonProperty("processName")] public string ProcessName { get; set; }
        
        [JsonProperty("processFindTimeOut")] public int ProcessFindTimeOut { get; set; }

        [JsonProperty("injectionActivate")] public bool InjectionActivate { get; set; }

        [JsonProperty("injectionDllType")] public string InjectionDllType { get; set; }

        [JsonProperty("responseTimeout")] public int ResponseTimeout { get; set; }


        public static Capabilities CapabilitiesFromJsonString(string jsonString) {
            var capabilities = JsonConvert.DeserializeObject<Capabilities>(
                jsonString,
                new JsonSerializerSettings {
                    Error =
                        delegate(object sender, ErrorEventArgs args) { args.ErrorContext.Handled = true; }
                });

            return capabilities;
        }

        public string CapabilitiesToJsonString() {
            return JsonConvert.SerializeObject(this);
        }

    }

}