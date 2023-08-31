using FlaNium.Desktop.Driver.Common;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.CheckBox {

    class CheckBoxToggleStateExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            global::FlaUI.Core.AutomationElements.CheckBox checkBox = element.FlaUiElement.AsCheckBox();

            var toggleState = checkBox.Patterns.Toggle.PatternOrDefault.ToggleState;

            return this.JsonResponse(ResponseStatus.Success, toggleState.ToString());
        }

    }

}