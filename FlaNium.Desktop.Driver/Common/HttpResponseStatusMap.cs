using System.Collections.Generic;
using System.Net;

namespace FlaNium.Desktop.Driver.Common {

    public class HttpResponseStatusMap {

        private static readonly Dictionary<ResponseStatus, HttpStatusCode> Map = new Dictionary<ResponseStatus, HttpStatusCode>() {
            { ResponseStatus.Success, HttpStatusCode.OK },
            { ResponseStatus.NoSuchDriver, HttpStatusCode.ServiceUnavailable },
            { ResponseStatus.NoSuchElement, HttpStatusCode.NotFound },
            { ResponseStatus.NoSuchFrame, HttpStatusCode.NotFound },
            { ResponseStatus.UnknownCommand, HttpStatusCode.BadRequest },
            { ResponseStatus.StaleElementReference, HttpStatusCode.BadRequest },
            { ResponseStatus.ElementNotVisible, HttpStatusCode.BadRequest },
            { ResponseStatus.InvalidElementState, HttpStatusCode.BadRequest },
            { ResponseStatus.UnknownError, HttpStatusCode.BadRequest },
            { ResponseStatus.ElementIsNotSelectable, HttpStatusCode.BadRequest },
            { ResponseStatus.JavaScriptError, HttpStatusCode.BadRequest },
            { ResponseStatus.XPathLookupError, HttpStatusCode.BadRequest },
            { ResponseStatus.Timeout, HttpStatusCode.RequestTimeout },
            { ResponseStatus.NoSuchWindow, HttpStatusCode.NotFound },
            { ResponseStatus.InvalidCookieDomain, HttpStatusCode.BadRequest },
            { ResponseStatus.UnableToSetCookie, HttpStatusCode.BadRequest },
            { ResponseStatus.UnexpectedAlertOpen, HttpStatusCode.BadRequest },
            { ResponseStatus.NoAlertOpenError, HttpStatusCode.BadRequest },
            { ResponseStatus.ScriptTimeout, HttpStatusCode.RequestTimeout },
            { ResponseStatus.InvalidElementCoordinates, HttpStatusCode.BadRequest },
            { ResponseStatus.ImeNotAvailable, HttpStatusCode.BadRequest },
            { ResponseStatus.ImeEngineActivationFailed, HttpStatusCode.BadRequest },
            { ResponseStatus.InvalidSelector, HttpStatusCode.BadRequest },
            { ResponseStatus.SessionNotCreatedException, HttpStatusCode.BadRequest },
            { ResponseStatus.MoveTargetOutOfBounds, HttpStatusCode.BadRequest }
        };

        public static HttpStatusCode GetStatusCode(ResponseStatus status) {
            return Map[status];
        }

    }

}