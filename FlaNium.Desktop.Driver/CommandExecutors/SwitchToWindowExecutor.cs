using System;
using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.FlaUI;

namespace FlaNium.Desktop.Driver.CommandExecutors {

    internal class SwitchToWindowExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            string title = this.ExecutedCommand.Parameters["name"].ToString();
            try {
                DriverManager.SwitchWindow(title);

                return this.JsonResponse(ResponseStatus.Success, (object)DriverManager.GetActiveWindow().Title);
            }
            catch (NullReferenceException ex) {
                return this.JsonResponse(ResponseStatus.NoSuchWindow, (object)ex);
            }
            catch (AccessViolationException ex) {
                return this.JsonResponse(ResponseStatus.ElementIsNotSelectable, (object)ex);
            }
        }

    }

}