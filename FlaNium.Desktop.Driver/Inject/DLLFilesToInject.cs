using System.IO;
using System.Reflection;

namespace FlaNium.Desktop.Driver.Inject {

    public static class DllFilesToInject {

        public static string GetDllFilePath(string dllName) {
            
            string dllFilePath = $"iLibs/{dllName}.dll";

            var fullPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), dllFilePath);

            if (File.Exists(fullPath)) return fullPath;

            throw new FileNotFoundException($"File to inject not found : \"{fullPath}\"");
        }

    }

}