using FlaNium.Desktop.Driver.Common;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.ComboBox {

    class ComboBoxEditableTextExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            global::FlaUI.Core.AutomationElements.ComboBox comboBox = element.FlaUiElement.AsComboBox();

            var text = comboBox.EditableText;

            return this.JsonResponse(ResponseStatus.Success, text);
        }

    }

}