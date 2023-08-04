using FlaNium.Desktop.Driver.Common;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.DataGridView {

    class DataGridViewHasAddRowExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            global::FlaUI.Core.AutomationElements.DataGridView item = element.FlaUiElement.AsDataGridView();

            var result = item.HasAddRow;

            return this.JsonResponse(ResponseStatus.Success, result);
        }

    }

}