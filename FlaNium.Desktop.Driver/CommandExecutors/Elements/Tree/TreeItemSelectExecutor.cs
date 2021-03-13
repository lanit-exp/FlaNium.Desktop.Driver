
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Tree
{
    class TreeItemSelectExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var treeItem = element.FlaUIElement.AsTreeItem();

            treeItem.Select();

            return this.JsonResponse();
        }

        #endregion
    }
}
