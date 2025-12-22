using System;
using System.Linq;
using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.ElementFindStrategy;
using FlaNium.Desktop.Driver.FlaUI;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.FindElement {

    internal class FindChildElementsExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            var parentKey = this.ExecutedCommand.Parameters["ID"].ToString();
            var searchValue = this.ExecutedCommand.Parameters["value"].ToString();
            var searchStrategy = this.ExecutedCommand.Parameters["using"].ToString();

            var parent = this.Automator.ElementsRegistry.GetRegisteredElement(parentKey);

            AutomationElement[] elements;

            if (searchStrategy.Equals("xpath")) {
                elements = ByXpath.FindAllByXPath(searchValue, parent.FlaUiElement);
            }
            else {
                var condition = ByHelper.GetStrategy(searchStrategy, searchValue);

                elements = parent.FlaUiElement.FindAllDescendants(condition);
            }

            var flaUiDriverElementList = elements
                .Select<AutomationElement, FlaUiDriverElement>(
                    (Func<AutomationElement, FlaUiDriverElement>)(x => new FlaUiDriverElement(x)))
                .ToList<FlaUiDriverElement>();

            var registeredKeys = this.Automator.ElementsRegistry.RegisterElements(flaUiDriverElementList);

            var registeredObjects = registeredKeys.Select(e => new JsonElementContent(e));

            return this.JsonResponse(ResponseStatus.Success, registeredObjects);
        }

    }

}