using Newtonsoft.Json.Linq;

namespace FlaNium.Desktop.Driver.Common {

    public class Command {

        private readonly int _bodySize;

        public string Name { get; }
        public string SessionId { get; set; }

        public JObject Parameters { get; }


        public Command(string name, string jsonParameters) {
            Name = name;

            if (!string.IsNullOrEmpty(jsonParameters)) {
                Parameters = JObject.Parse(jsonParameters);
                _bodySize = jsonParameters.Length;
            }
            else {
                Parameters = new JObject();
                _bodySize = 0;
            }
        }


        public string GetParametersAsString() {
            if (_bodySize > 100_000)
                return
                    "REQUEST:\r\n" +
                    "░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░\n" +
                    "░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░\n" +
                    $" Content length: {_bodySize}\n" +
                    "░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░\n" +
                    "░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░\n";

            return Parameters.ToString();
        }

    }

}