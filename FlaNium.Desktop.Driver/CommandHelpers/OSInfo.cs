using System;
using Newtonsoft.Json;

namespace FlaNium.Desktop.Driver.CommandHelpers {

    // ReSharper disable once InconsistentNaming
    public class OSInfo {

        private static string _architecture;

        private static string _version;


        [JsonProperty("arch")]
        public string Architecture =>
            _architecture ?? (_architecture = Environment.Is64BitOperatingSystem ? "x64" : "x86");

        [JsonProperty("name")] public string Name => "windows";

        [JsonProperty("version")] public string Version => _version ?? (_version = Environment.OSVersion.VersionString);

    }

}