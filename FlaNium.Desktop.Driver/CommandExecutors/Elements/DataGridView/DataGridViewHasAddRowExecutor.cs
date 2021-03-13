
namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.DataGridView
{
    using global::FlaUI.Core.AutomationElements;
    using FlaNium.Desktop.Driver.Common;

    class DataGridViewHasAddRowExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            DataGridView item = element.FlaUIElement.AsDataGridView();

            var result = item.HasAddRow;

            return this.JsonResponse(ResponseStatus.Success, result);
        }

        #endregion
    }
}
