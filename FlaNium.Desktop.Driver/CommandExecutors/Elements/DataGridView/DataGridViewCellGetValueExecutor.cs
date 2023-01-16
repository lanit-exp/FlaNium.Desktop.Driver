using FlaNium.Desktop.Driver.Common;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.DataGridView {

    class DataGridViewCellGetValueExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            DataGridViewCell item = (DataGridViewCell)element.FlaUiElement;

            var value = item.Value;

            return this.JsonResponse(ResponseStatus.Success, value);
        }

    }

}