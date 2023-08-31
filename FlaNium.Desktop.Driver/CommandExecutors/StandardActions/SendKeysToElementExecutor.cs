using FlaNium.Desktop.Driver.Common;

namespace FlaNium.Desktop.Driver.CommandExecutors.StandardActions {

    internal class SendKeysToElementExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var text = string.Join(string.Empty, this.ExecutedCommand.Parameters["value"]);

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            element.Type(text);

            return this.JsonResponse();
        }

    }

}