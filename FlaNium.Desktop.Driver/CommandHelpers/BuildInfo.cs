using System.Reflection;
using Newtonsoft.Json;

namespace FlaNium.Desktop.Driver.CommandHelpers {

    public class BuildInfo {

        private static string _version;

        [JsonProperty("version")]
        public string Version => _version ?? (_version = Assembly.GetExecutingAssembly().GetName().Version.ToString());

    }

}