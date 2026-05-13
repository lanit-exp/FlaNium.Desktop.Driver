using Newtonsoft.Json.Linq;

namespace FlaNium.Desktop.Driver.Common {

    public class Command {

        private readonly int bodySize;

        public string Name { get; }

        public JObject Parameters { get; }


        public Command(string name, string jsonParameters) {
            Name = name;

            if (!string.IsNullOrEmpty(jsonParameters)) {
                Parameters = JObject.Parse(jsonParameters);
                bodySize = jsonParameters.Length;
            }
            else {
                Parameters = new JObject();
                bodySize = 0;
            }
        }


        public string GetParametersAsString() {
            if (bodySize > 100_000)
                return
                    "REQUEST:\r\n" +
                    "░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░\n" +
                    "░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░\n" +
                    $" Content length: {bodySize}\n" +
                    "░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░\n" +
                    "░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░\n";

            return Parameters.ToString();
        }

    }

}