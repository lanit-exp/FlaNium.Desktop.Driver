using System.IO;

namespace FlaNium.Desktop.Driver.Inject
{
    public class DLLFilesToInject
    {
        public enum DLLFile
        {
            DELPHI
        }



        public static string getFullDllPath(DLLFile dLLFile)
        {
            string filePath = null;

            switch (dLLFile)
            {
                case DLLFile.DELPHI:
                    filePath = "iLibs/DEDL.dll";
                    break;
            }

            string fullPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), filePath);

            if (!File.Exists(fullPath))
            {
                Logger.Error("File to inject not found : \"{0}\"", fullPath);

                return null;
            }

            return fullPath;
        }

        public static DLLFile GetDLLFile(string dllFile)
        {

            switch (dllFile)
            {
                case "DELPHI": return DLLFile.DELPHI;
                default: throw new InvalidDataException();
            }

        }
    }
}
