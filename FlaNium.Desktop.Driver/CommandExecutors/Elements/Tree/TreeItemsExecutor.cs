
using System;
using System.Linq;
using FlaUI.Core.AutomationElements;
using FlaNium.Desktop.Driver.FlaUI;
using FlaNium.Desktop.Driver.Common;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Tree
{
    class TreeItemsExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var tree = element.FlaUIElement.AsTree();

            var result = tree.Items;

            var flaUiDriverElementList = result
                .Select<AutomationElement, FlaUIDriverElement>((Func<AutomationElement, FlaUIDriverElement>)(x => new FlaUIDriverElement(x)))
                .ToList<FlaUIDriverElement>();

            var registeredKeys = this.Automator.ElementsRegistry.RegisterElements(flaUiDriverElementList);

            var registeredObjects = registeredKeys.Select(e => new JsonElementContent(e));

            return this.JsonResponse(ResponseStatus.Success, registeredObjects);
        }

        #endregion
    }
}
