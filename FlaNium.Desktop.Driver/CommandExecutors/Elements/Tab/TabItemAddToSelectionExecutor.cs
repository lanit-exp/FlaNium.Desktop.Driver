
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Tab
{
    class TabItemAddToSelectionExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var tab = element.FlaUIElement.AsTabItem();

            tab.AddToSelection();
                        
            return this.JsonResponse();
        }

        #endregion
    }
}
