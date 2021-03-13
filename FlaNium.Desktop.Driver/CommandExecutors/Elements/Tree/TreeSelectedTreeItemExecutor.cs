
using FlaUI.Core.AutomationElements;
using FlaNium.Desktop.Driver.FlaUI;
using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.Exceptions;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Tree
{
    class TreeSelectedTreeItemExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var tree = element.FlaUIElement.AsTree();

            var result = tree.SelectedTreeItem;

            if (result == null)
            {
                throw new AutomationException("Element cannot be found", ResponseStatus.NoSuchElement);
            }

            var itemRegisteredKey = this.Automator.ElementsRegistry.RegisterElement(new FlaUIDriverElement(result));

            var registeredObject = new JsonElementContent(itemRegisteredKey);

            return this.JsonResponse(ResponseStatus.Success, registeredObject);
        }

        #endregion
    }
}
