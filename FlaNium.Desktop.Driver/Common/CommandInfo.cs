namespace FlaNium.Desktop.Driver.Common {

    public class CommandInfo {

        public const string DeleteCommand = "DELETE";

        public const string GetCommand = "GET";

        public const string PostCommand = "POST";


        public CommandInfo(string method, string resourcePath) {
            ResourcePath = resourcePath;
            Method = method;
        }


        public string Method { get; }

        public string ResourcePath { get; }

    }

}