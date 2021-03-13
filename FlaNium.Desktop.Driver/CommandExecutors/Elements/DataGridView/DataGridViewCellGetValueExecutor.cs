
namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.DataGridView
{
    using global::FlaUI.Core.AutomationElements;
    using FlaNium.Desktop.Driver.Common;

    class DataGridViewCellGetValueExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            DataGridViewCell item = (DataGridViewCell) element.FlaUIElement;

            var value = item.Value;

            return this.JsonResponse(ResponseStatus.Success, value);
        }

        #endregion
    }
}
