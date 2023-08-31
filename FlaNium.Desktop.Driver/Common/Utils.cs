using System.IO;
using System.Text.RegularExpressions;

namespace FlaNium.Desktop.Driver.Common {

    public static class Utils {

        public static string ReplaceSystemVarsIfPresent(string value) {
            Regex regex = new Regex(@"<[^<>]*>");

            string result = regex.Replace(value, match => {
                string varName = new Regex(@"(<\s*)|(\s*>)").Replace(match.Value, "");

                string varValue = System.Environment.GetEnvironmentVariable(varName);

                return varValue ?? match.Value;
            });

            return result;
        }


        public static bool PathExists(string path) {
            return File.Exists(path) || Directory.Exists(path);
        }


        public static void DeletePath(string path) {
            if (Directory.Exists(path)) {
                Directory.Delete(path, true);
            }
            else if (File.Exists(path)) {
                File.Delete(path);
            }
            else {
                throw new FileNotFoundException($"File or directory '{path}' NOT FOUND");
            }
        }

    }

}