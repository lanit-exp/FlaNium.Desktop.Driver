using FlaNium.Desktop.Driver.Common;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.ToggleButton {

    class ToggleButtonGetToggleStateExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var toggleButton = element.FlaUiElement.AsToggleButton();

            var result = toggleButton.ToggleState;

            return this.JsonResponse(ResponseStatus.Success, result.ToString());
        }

    }

}