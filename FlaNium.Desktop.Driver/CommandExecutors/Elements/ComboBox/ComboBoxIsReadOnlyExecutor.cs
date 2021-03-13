
namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.ComboBox
{
    using global::FlaUI.Core.AutomationElements;
    using FlaNium.Desktop.Driver.Common;

    class ComboBoxIsReadOnlyExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            ComboBox comboBox = element.FlaUIElement.AsComboBox();

            var result = comboBox.IsReadOnly;

            return this.JsonResponse(ResponseStatus.Success, result);
        }

        #endregion
    }
}
