using System;
using Newtonsoft.Json;

namespace FlaNium.Desktop.Driver.Common {

    public class JsonElementContent {

        [JsonProperty("element-6066-11e4-a52e-4f735466cecf")] 
        public string Element { get; set; }

        
        public JsonElementContent(string element) {
            this.Element = element;
        }

    }

    public class JsonResponse {

        private const string ERROR_MES_PREFIX = "[FlaNium ERROR]: ";

        
        [JsonProperty("value")] 
        public Object Value { get; set; }

        [JsonProperty("status")]
        public ResponseStatus Status { get; set; }

        public JsonResponse(ResponseStatus responseStatus, object value) {
            this.Status = responseStatus;
            this.Value = responseStatus == ResponseStatus.Success ? value : PrepareErrorResponse(responseStatus, value);
        }

        private static object PrepareErrorResponse(ResponseStatus responseCode, object value) {
            ResponseValueError responseValueError = new ResponseValueError();

            responseValueError.Error = JsonErrorCodes.Parse(responseCode);
            
            if (value is Exception exception) {
                responseValueError.Message = ERROR_MES_PREFIX + exception.Message;
                responseValueError.Stacktrace = exception.StackTrace;
            } else {
                responseValueError.Message = ERROR_MES_PREFIX + value;
            }

            return responseValueError;
        }


        public class ResponseValueError {
            
            [JsonProperty("message")] 
            public string Message { get; set; }
            
            [JsonProperty("error")] 
            public string Error { get; set; }
            
            [JsonProperty("stacktrace")] 
            public string Stacktrace { get; set; }
           
        }

    }

}