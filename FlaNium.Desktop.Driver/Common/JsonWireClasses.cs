using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FlaNium.Desktop.Driver.Common {

    public class JsonElementContent {

        public JsonElementContent(string element) {
            this.Element = element;
        }


        [JsonProperty("ELEMENT")] public string Element { get; set; }

    }

    public class JsonResponse {

        public JsonResponse(string sessionId, ResponseStatus responseCode, object value) {
            this.SessionId = sessionId;
            this.Status = responseCode;

            this.Value = responseCode == ResponseStatus.Success ? value : this.PrepareErrorResponse(value);
        }

        private object PrepareErrorResponse(object value) {
            var result = new Dictionary<string, string> { { "error", JsonErrorCodes.Parse(this.Status) } };

            string message;
            if (value is Exception exception) {
                message = exception.Message;
                result.Add("stacktrace", exception.StackTrace);
            }
            else {
                message = value.ToString();
            }

            result.Add("message", message);

            return result;
        }


        [JsonProperty("sessionId")] public string SessionId { get; set; }

        [JsonProperty("status")] public ResponseStatus Status { get; set; }

        [JsonProperty("value")] public object Value { get; set; }

    }

}