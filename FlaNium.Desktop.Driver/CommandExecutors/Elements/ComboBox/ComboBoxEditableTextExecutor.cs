
namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.ComboBox
{
    using global::FlaUI.Core.AutomationElements;
    using FlaNium.Desktop.Driver.Common;

    class ComboBoxEditableTextExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            ComboBox comboBox = element.FlaUIElement.AsComboBox();

            var text = comboBox.EditableText;

            return this.JsonResponse(ResponseStatus.Success, (object)text);
        }

        #endregion
    }
}
