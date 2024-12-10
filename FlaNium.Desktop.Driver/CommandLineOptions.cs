namespace FlaNium.Desktop.Driver
{

    using CommandLine;


    internal class CommandLineOptions
    {

        [Option("log-path", Required = false, Default = "", HelpText = "write server log to file instead of stdout, increases log level to INFO")]
        public string LogPath { get; set; }


        [Option('p', "port", Required = false, Default = 9999, HelpText = "port to listen on")]
        public int Port { get; set; }


        [Option("url-base", Required = false, Default = "", HelpText = "base URL path prefix for commands, e.g. wd/url")]
        public string UrlBase { get; set; }

        [Option('v', "verbose", Required = false, Default = true, HelpText = "log verbosely")]
        public bool Verbose { get; set; }


        [Option('s', "silent", Required = false, Default = false, HelpText = "log nothing")]
        public bool Silent { get; set; }

    }
}
