
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.ListBox
{
    class ListBoxItemSetCheckedExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var value = this.ExecutedCommand.Parameters["value"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var listBoxItem = element.FlaUIElement.AsListBoxItem();

            listBoxItem.IsChecked = bool.Parse(value);

            return this.JsonResponse();
        }

        #endregion
    }
}
