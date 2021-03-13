

using FlaUI.Core.AutomationElements;
using FlaNium.Desktop.Driver.Common;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.TextBox
{
    class TextBoxIsReadOnlyExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var textBox = element.FlaUIElement.AsTextBox();

            bool value = textBox.IsReadOnly;

            return this.JsonResponse(ResponseStatus.Success, value.ToString());
        }

        #endregion
    }
}
