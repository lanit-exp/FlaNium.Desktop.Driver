
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Tree
{
    class TreeItemCollapseExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var treeItem = element.FlaUIElement.AsTreeItem();

            treeItem.Collapse();

            return this.JsonResponse();
        }

        #endregion
    }
}
