namespace FlaNium.Desktop.Driver.Common {

    public class CommandInfo {

        public const string DeleteCommand = "DELETE";

        public const string GetCommand = "GET";

        public const string PostCommand = "POST";


        public CommandInfo(string method, string resourcePath) {
            this.ResourcePath = resourcePath;
            this.Method = method;
        }


        public string Method { get; set; }

        public string ResourcePath { get; set; }

    }

}