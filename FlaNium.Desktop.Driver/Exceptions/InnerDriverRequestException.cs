using System;
using System.Net;

namespace FlaNium.Desktop.Driver.Exceptions {

    public class InnerDriverRequestException : Exception {
    
        public InnerDriverRequestException(string message, HttpStatusCode statusCode)
            : base(message) {
            StatusCode = statusCode;
        }
        
        public HttpStatusCode StatusCode { get; set; }

    }

}