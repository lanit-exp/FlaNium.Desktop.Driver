using System.Net;

namespace FlaNium.Desktop.Driver.Common {

    public class CommandResponse {

        public string Content { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }


        public static CommandResponse Create(HttpStatusCode code, string content) {
            return new CommandResponse { HttpStatusCode = code, Content = content };
        }

        public override string ToString() {
            return string.Format("{0}: {1}", this.HttpStatusCode, this.Content);
        }

    }

}