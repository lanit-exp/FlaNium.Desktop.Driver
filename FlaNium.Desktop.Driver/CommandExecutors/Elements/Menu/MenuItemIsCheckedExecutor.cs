using FlaNium.Desktop.Driver.Common;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Menu {

    class MenuItemIsCheckedExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var menuItem = element.FlaUiElement.AsMenuItem();

            var result = menuItem.IsChecked;

            return this.JsonResponse(ResponseStatus.Success, result.ToString());
        }

    }

}