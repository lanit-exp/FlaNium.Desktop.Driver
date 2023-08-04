using FlaNium.Desktop.Driver.Common;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.TextBox {

    class TextBoxGetTextExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var textBox = element.FlaUiElement.AsTextBox();

            string value = textBox.Text;

            return this.JsonResponse(ResponseStatus.Success, value);
        }

    }

}