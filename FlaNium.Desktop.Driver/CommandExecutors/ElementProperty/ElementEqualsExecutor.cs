using FlaNium.Desktop.Driver.Common;

namespace FlaNium.Desktop.Driver.CommandExecutors.ElementProperty {

    internal class ElementEqualsExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();
            var otherRegisteredKey = this.ExecutedCommand.Parameters["other"].ToString();

            return this.JsonResponse(ResponseStatus.Success, registeredKey == otherRegisteredKey);
        }

    }

}