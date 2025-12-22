namespace FlaNium.Desktop.Driver
{

    using CommandLine;


    internal class CommandLineOptions
    {

        [Option("log-path", Required = false, Default = "", HelpText = "write server log to file instead of stdout, increases log level to INFO")]
        public string LogPath { get; set; }


        [Option('p', "port", Required = false, Default = 9999, HelpText = "port to listen on")]
        public int Port { get; set; }


        [Option("allowed-ips", Required = false, Default = "", HelpText = "List of IP addresses from which connections are allowed. Local addresses are available by default. (address separator - ',' )")]
        public string AllowedIps { get; set; }

        
        [Option('v', "verbose", Required = false, Default = false, HelpText = "log verbosely")]
        public bool Verbose { get; set; }


        [Option('s', "silent", Required = false, Default = false, HelpText = "log nothing")]
        public bool Silent { get; set; }
        
        
        [Option( "cashed-strategy-default", Required = false, Default = false, HelpText = "Set the search strategy to use cached items by default")]
        public bool CachedStrategyDefault { get; set; }

    }
}
