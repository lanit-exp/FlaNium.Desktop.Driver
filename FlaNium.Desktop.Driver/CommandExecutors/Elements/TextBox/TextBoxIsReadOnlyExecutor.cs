using FlaNium.Desktop.Driver.Common;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.TextBox {

    class TextBoxIsReadOnlyExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var textBox = element.FlaUiElement.AsTextBox();

            bool value = textBox.IsReadOnly;

            return this.JsonResponse(ResponseStatus.Success, value.ToString());
        }

    }

}