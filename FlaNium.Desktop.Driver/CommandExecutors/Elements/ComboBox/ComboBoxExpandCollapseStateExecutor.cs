
namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.ComboBox
{
    using global::FlaUI.Core.AutomationElements;
    using global::FlaUI.Core.Definitions;
    using FlaNium.Desktop.Driver.Common;

    class ComboBoxExpandCollapseStateExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            ComboBox comboBox = element.FlaUIElement.AsComboBox();

            ExpandCollapseState state = comboBox.ExpandCollapseState;

            return this.JsonResponse(ResponseStatus.Success, state.ToString());
        }

        #endregion
    }
}
