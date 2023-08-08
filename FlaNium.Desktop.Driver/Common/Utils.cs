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

    }

}