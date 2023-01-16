using System;
using System.Reflection;

namespace FlaNium.Desktop.Driver {

    internal static class Logo {

        private const string LOGO = "\t    ______    __            _   __                            \n" +
                                    "\t   / ____/   / /  ____ _   / | / /  (^_^) __  __   ____ ___  \n" +
                                    "\t  / /_      / /  / __ `/  /  |/ /   / /  / / / /  / __ `__ \\ \n" +
                                    "\t / __/     / /  / /_/ /  / /|  /   / /  / /_/ /  / / / / / / \n" +
                                    "\t/_/       /_/   \\__,_/  /_/ |_/   /_/   \\__,_/  /_/ /_/ /_/  \n" +
                                    "\t============================================================";

        public static void PrintLogo() {
            Console.WriteLine(LOGO);
            Console.WriteLine("\t\t\t\t\t\t    Version: " +
                              Assembly.GetExecutingAssembly().GetName().Version + "\n\n");
        }

    }

}