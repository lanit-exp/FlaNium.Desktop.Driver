using System.Net;
using Newtonsoft.Json;

namespace FlaNium.Desktop.Driver.Common {

    public class CommandResponse {

        public string Content { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }

        
        public static CommandResponse Create(JsonResponse jsonResponse) {
            return new CommandResponse { 
                HttpStatusCode = HttpResponseStatusMap.GetStatusCode(jsonResponse.Status), 
                Content = JsonConvert.SerializeObject(jsonResponse, Formatting.Indented) 
            };
        }

        public override string ToString() {
            return string.Format("{0}: {1}", this.HttpStatusCode, this.Content);
        }

    }

}