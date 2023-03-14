using FlaNium.Desktop.Driver.Common;

namespace FlaNium.Desktop.Driver.CommandExecutors.ElementProperty {

    internal class GetWindowHandlesExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            return this.JsonResponse(ResponseStatus.UnknownCommand, "Command not implemented");
        }

    }

}