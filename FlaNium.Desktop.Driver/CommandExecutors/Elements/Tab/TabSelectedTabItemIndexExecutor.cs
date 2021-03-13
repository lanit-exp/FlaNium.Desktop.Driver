
using FlaUI.Core.AutomationElements;
using FlaNium.Desktop.Driver.Common;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Tab
{
    class TabSelectedTabItemIndexExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var tab = element.FlaUIElement.AsTab();

            var result = tab.SelectedTabItemIndex;
                       
            return this.JsonResponse(ResponseStatus.Success, result.ToString());
        }

        #endregion
    }
}
