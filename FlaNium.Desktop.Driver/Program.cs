using System;
using CommandLine;

namespace FlaNium.Desktop.Driver {

    internal class Program {

        [STAThread]
        private static void Main(string[] args) {
            Logo.PrintLogo();


            Parser.Default.ParseArguments<CommandLineOptions>(args)
                .WithParsed(Run);

            Console.Write("\n\nPress any key...");
            Console.ReadKey();
        }


        private static void Run(CommandLineOptions options) {
            // Настройка логирования
            if (!options.LogPath.Equals("")) {
                Logger.TargetFile(options.LogPath, options.Verbose);
            }
            else if (!options.Silent) {
                Logger.TargetConsole(options.Verbose);
            }
            else {
                Logger.TargetNull();
            }


            try {
                var listener = new Listener(options.Port, options.AllowedIps);
                listener.StartListening();
            }
            catch (Exception ex) {
                Logger.Fatal("Failed to start driver: {0}", ex);
                throw;
            }
        }

    }

}