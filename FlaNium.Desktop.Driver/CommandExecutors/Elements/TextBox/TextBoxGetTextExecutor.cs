
using FlaUI.Core.AutomationElements;
using FlaNium.Desktop.Driver.Common;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.TextBox
{
    class TextBoxGetTextExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var textBox = element.FlaUIElement.AsTextBox();

            string value = textBox.Text;

            return this.JsonResponse(ResponseStatus.Success, value);
        }

        #endregion
    }
}
