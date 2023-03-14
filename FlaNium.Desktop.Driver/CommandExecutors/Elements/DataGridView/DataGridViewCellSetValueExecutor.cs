using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.DataGridView {

    class DataGridViewCellSetValueExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var value = this.ExecutedCommand.Parameters["value"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            DataGridViewCell item = (DataGridViewCell)element.FlaUiElement;

            item.Value = value;

            return this.JsonResponse();
        }

    }

}