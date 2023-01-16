using FlaNium.Desktop.Driver.Common;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.ListBox {

    class ListBoxItemIsCheckedExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var listBoxItem = element.FlaUiElement.AsListBoxItem();

            var result = listBoxItem.IsChecked;

            return this.JsonResponse(ResponseStatus.Success, result.ToString());
        }

    }

}