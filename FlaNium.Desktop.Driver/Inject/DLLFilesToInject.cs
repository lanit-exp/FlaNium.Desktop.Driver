using System.IO;
using System.Reflection;

namespace FlaNium.Desktop.Driver.Inject {

    public static class DllFilesToInject {

        public static string GetDllFilePath(string dllType) {
            string dllFilePath;

            switch (dllType) {
                case "DELPHI":
                    dllFilePath = "iLibs/DEDL.dll";

                    break;

                default: throw new InvalidDataException($"Incorrect injectDllType Capabilities: {dllType}");
            }

            var fullPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), dllFilePath);

            if (File.Exists(fullPath)) return fullPath;

            throw new FileNotFoundException($"File to inject not found : \"{fullPath}\"");
        }

    }

}