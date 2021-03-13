
using FlaUI.Core.AutomationElements;
using FlaNium.Desktop.Driver.Common;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.ListBox
{
    class ListBoxItemIsCheckedExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var listBoxItem = element.FlaUIElement.AsListBoxItem();

            var result = listBoxItem.IsChecked;
                        
            return this.JsonResponse(ResponseStatus.Success, result.ToString());
        }

        #endregion
    }
}
