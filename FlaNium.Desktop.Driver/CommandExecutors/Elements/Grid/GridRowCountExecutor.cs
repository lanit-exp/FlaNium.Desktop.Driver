
namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Grid
{
    using global::FlaUI.Core.AutomationElements;
    using FlaNium.Desktop.Driver.Common;

    class GridRowCountExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            Grid grid = element.FlaUIElement.AsGrid();

            var result = grid.RowCount;

            return this.JsonResponse(ResponseStatus.Success, result);
        }

        #endregion
    }
}
