using FlaNium.Desktop.Driver.Common;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Spinner {

    class SpinnerIsOnlyValueExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var spinner = element.FlaUiElement.AsSpinner();

            bool value = spinner.IsOnlyValue;

            return this.JsonResponse(ResponseStatus.Success, value.ToString());
        }

    }

}