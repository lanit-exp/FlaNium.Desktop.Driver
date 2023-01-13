using System;
using System.Net;

namespace FlaNium.Desktop.Driver.Exceptions {

    public class InnerDriverRequestException : Exception {

        public InnerDriverRequestException() {
        }

        public InnerDriverRequestException(string message, HttpStatusCode statusCode)
            : base(message) {
            this.StatusCode = statusCode;
        }

        public InnerDriverRequestException(string message, params object[] args)
            : base(string.Format(message, args)) {
        }

        public InnerDriverRequestException(string message, Exception innerException)
            : base(message, innerException) {
        }


        public HttpStatusCode StatusCode { get; set; }

    }

}