using FlaNium.Desktop.Driver.Common;

namespace FlaNium.Desktop.Driver.CommandExecutors {

    internal class SwitchToWindowExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            string title = this.ExecutedCommand.Parameters["name"].ToString();

            return this.JsonResponse(ResponseStatus.UnknownCommand, "SwitchToWindow command not implemented. Use SetRootElement commands.");
        }

    }

}