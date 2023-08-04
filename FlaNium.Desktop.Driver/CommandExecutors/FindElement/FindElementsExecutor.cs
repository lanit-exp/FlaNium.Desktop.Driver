using System;
using System.Linq;
using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.Exceptions;
using FlaNium.Desktop.Driver.Extensions;
using FlaNium.Desktop.Driver.FlaUI;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.FindElement {

    internal class FindElementsExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            var searchValue = this.ExecutedCommand.Parameters["value"].ToString();
            var searchStrategy = this.ExecutedCommand.Parameters["using"].ToString();


            AutomationElement rootElement = DriverManager.GetRootElement();
            AutomationElement[] elements;

            if (searchStrategy.Equals("xpath")) {
                if (searchValue.StartsWith("#")) {
                    searchValue = searchValue.TrimStart('#');
                    rootElement = rootElement.Automation.GetDesktop();
                }

                elements = ByXpath.FindAllByXPath(searchValue, rootElement);
            }
            else {
                var condition = ByHelper.GetStrategy(searchStrategy, searchValue);

                elements = rootElement.FindAllDescendants(condition);
            }

            if (elements == null) {
                throw new AutomationException("Element cannot be found", ResponseStatus.NoSuchElement);
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