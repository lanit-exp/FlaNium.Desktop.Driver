
using FlaUI.Core.AutomationElements;
using FlaNium.Desktop.Driver.Common;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Menu
{
    class MenuItemIsCheckedExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var menuItem = element.FlaUIElement.AsMenuItem();

            var result = menuItem.IsChecked;

            return this.JsonResponse(ResponseStatus.Success, result.ToString());
        }

        #endregion
    }
}
