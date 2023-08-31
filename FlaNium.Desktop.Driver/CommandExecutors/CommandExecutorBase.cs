using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlaNium.Desktop.Driver.CommandExecutors.Auxiliary;
using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.Exceptions;

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

                return CommandResponse.Create(DoInOtherThread());
            }
            catch (AutomationException exception) {
                return CommandResponse.Create(JsonResponse(exception.Status, exception));
            }
            catch (NotImplementedException exception) {
                return CommandResponse.Create(JsonResponse(ResponseStatus.UnknownCommand, exception));
            }
            catch (TimeoutException exception) {
                return CommandResponse.Create(JsonResponse(ResponseStatus.Timeout, exception));
            }
            catch (Exception exception) {
                return CommandResponse.Create(JsonResponse(ResponseStatus.UnknownError, exception));
            }
        }


        private JsonResponse DoInOtherThread() {
            // Работа с буфером обмена может осуществляться только из основного потока выполнения программы,
            // все остальные Executor выполняются в отдельном потоке с прерыванием по таймауту.
            if (this is GetClipboardTextExecutor || this is SetClipboardTextExecutor) {
                return DoImpl();
            }

            JsonResponse result = JsonResponse(ResponseStatus.UnknownError, "UnknownError in DoInOtherThread method!");

            Task task = Task.Factory.StartNew(() => { result = DoImpl(); });


            try {
                if (!task.Wait(this.Automator.ActualCapabilities == null
                        ? 120000
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

        protected abstract JsonResponse DoImpl();

        protected JsonResponse JsonResponse(ResponseStatus status = ResponseStatus.Success, object value = null) {
            return new JsonResponse(status, value);
        }
        
    }

}