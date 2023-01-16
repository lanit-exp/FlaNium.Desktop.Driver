using FlaNium.Desktop.Driver.Common;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Grid {

    class GridColumnCountExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            global::FlaUI.Core.AutomationElements.Grid grid = element.FlaUiElement.AsGrid();

            var result = grid.ColumnCount;

            return this.JsonResponse(ResponseStatus.Success, result);
        }

    }

}