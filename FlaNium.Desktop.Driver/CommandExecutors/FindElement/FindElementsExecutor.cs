using System;
using System.Linq;
using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.Exceptions;
using FlaNium.Desktop.Driver.Extensions;
using FlaNium.Desktop.Driver.FlaUI;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.FindElement {

    internal class FindElementsExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var searchValue = this.ExecutedCommand.Parameters["value"].ToString();
            var searchStrategy = this.ExecutedCommand.Parameters["using"].ToString();


            AutomationElement activeWindow = DriverManager.GetActiveWindow();
            AutomationElement[] elements;

            if (searchStrategy.Equals("xpath")) {
                if (searchValue.StartsWith("#")) {
                    searchValue = searchValue.TrimStart('#');
                    activeWindow = activeWindow.Automation.GetDesktop();
                }

                elements = ByXpath.FindAllByXPath(searchValue, activeWindow);
            }
            else {
                var condition = ByHelper.GetStrategy(searchStrategy, searchValue);

                elements = activeWindow.FindAllDescendants(condition);
            }

            if (elements == null) {
                throw new AutomationException("Element cannot be found", ResponseStatus.NoSuchElement);
            }


            var flaUiDriverElementList = elements
                .Select<AutomationElement, FlaUIDriverElement>(
                    (Func<AutomationElement, FlaUIDriverElement>)(x => new FlaUIDriverElement(x)))
                .ToList<FlaUIDriverElement>();

            var registeredKeys = this.Automator.ElementsRegistry.RegisterElements(flaUiDriverElementList);

            var registeredObjects = registeredKeys.Select(e => new JsonElementContent(e));

            return this.JsonResponse(ResponseStatus.Success, registeredObjects);
        }

    }

}