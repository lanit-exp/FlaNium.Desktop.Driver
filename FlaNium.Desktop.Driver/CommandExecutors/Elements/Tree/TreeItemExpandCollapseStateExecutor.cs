
using FlaUI.Core.AutomationElements;
using FlaNium.Desktop.Driver.Common;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Tree
{
    class TreeItemExpandCollapseStateExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var treeItem = element.FlaUIElement.AsTreeItem();

            var result = treeItem.ExpandCollapseState;

            return this.JsonResponse(ResponseStatus.Success, result.ToString());
        }

        #endregion
    }
}
