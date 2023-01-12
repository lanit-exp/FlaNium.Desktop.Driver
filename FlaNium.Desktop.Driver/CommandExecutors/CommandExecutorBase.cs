using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FlaNium.Desktop.Driver.CommandExecutors.Auxiliary;
using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.Exceptions;
using Newtonsoft.Json;

namespace FlaNium.Desktop.Driver.CommandExecutors {

    internal abstract class CommandExecutorBase {

        public Command ExecutedCommand { get; set; }

        protected Automator.Automator Automator { get; set; }


        public CommandResponse Do() {
            if (this.ExecutedCommand == null) {
                throw new NullReferenceException("ExecutedCommand property must be set before calling Do");
            }

            try {
                var session = this.ExecutedCommand.SessionId;
                this.Automator = Driver.Automator.Automator.InstanceForSession(session);

                return CommandResponse.Create(HttpStatusCode.OK, this.DoInOtherThread());
            }
            catch (AutomationException exception) {
                return CommandResponse.Create(HttpStatusCode.OK, this.JsonResponse(exception.Status, exception));
            }
            catch (NotImplementedException exception) {
                return CommandResponse.Create(HttpStatusCode.NotImplemented,
                    this.JsonResponse(ResponseStatus.UnknownCommand, exception));
            }
            catch (TimeoutException exception) {
                return CommandResponse.Create(HttpStatusCode.NotImplemented,
                    this.JsonResponse(ResponseStatus.Timeout, exception));
            }
            catch (Exception exception) {
                return CommandResponse.Create(HttpStatusCode.OK,
                    this.JsonResponse(ResponseStatus.UnknownError, exception));
            }
        }


        private string DoInOtherThread() {
            if (this is GetClipboardTextExecutor || this is SetClipboardTextExecutor) {
                return DoImpl();
            }

            string result = null;

            Task task = Task.Factory.StartNew(() => { result = DoImpl(); });


            try {
                if (!task.Wait(this.Automator.ActualCapabilities == null
                        ? 300000
                        : this.Automator.ActualCapabilities.ResponseTimeout)) {
                    throw new TimeoutException("Response timed out!!!");
                }
            }
            catch (AggregateException exception) {
                IEnumerator<Exception> enumerator = exception.InnerExceptions.GetEnumerator();
                enumerator.MoveNext();

                throw enumerator.Current;
            }

            return result;
        }

        protected abstract string DoImpl();

        /// <summary>
        /// The JsonResponse with SUCCESS status and NULL value.
        /// </summary>
        protected string JsonResponse() {
            return this.JsonResponse(ResponseStatus.Success, null);
        }

        protected string JsonResponse(ResponseStatus status, object value) {
            return JsonConvert.SerializeObject(
                new JsonResponse(this.Automator.Session, status, value),
                Formatting.Indented);
        }

    }

}