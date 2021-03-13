namespace FlaNium.Desktop.Driver
{
    using CommandLine;
    using System;

    internal class Program
    {

        [STAThread]
        private static void Main(string[] args)
        {
            Logo.printLogo();


            Parser.Default.ParseArguments<CommandLineOptions>(args)
                .WithParsed<CommandLineOptions>(Run);

            Console.Write("\n\nPress any key...");
            Console.ReadKey();
        }


        private static void Run(CommandLineOptions options)
        {
            // Настройка логирования
            if (!options.LogPath.Equals(""))
            {
                Logger.TargetFile(options.LogPath, options.Verbose);
            }
            else if (!options.Silent)
            {
                Logger.TargetConsole(options.Verbose);
            }
            else
            {
                Logger.TargetNull();
            }


            try
            {
                var listener = new Listener(options.Port);
                Listener.UrnPrefix = options.UrlBase;

                Console.WriteLine("Starting Windows Desktop Driver on port {0}\n", options.Port);

                listener.StartListening();
            }
            catch (Exception ex)
            {
                Logger.Fatal("Failed to start driver: {0}", ex);
                throw;
            }


        }

    }
}
