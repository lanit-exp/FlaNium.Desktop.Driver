using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.Exceptions;
using FlaNium.Desktop.Driver.ElementFindStrategy;
using FlaNium.Desktop.Driver.FlaUI;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.FindElement {

    internal class FindElementExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            var searchValue = this.ExecutedCommand.Parameters["value"].ToString();
            var searchStrategy = this.ExecutedCommand.Parameters["using"].ToString();


            AutomationElement rootElement = DriverManager.GetRootElement();

            AutomationElement element;

            if (searchStrategy.Equals("xpath")) {
                if (searchValue.StartsWith("#")) {
                    searchValue = searchValue.TrimStart('#');
                    rootElement = rootElement.Automation.GetDesktop();
                }

                element = ByXpath.FindFirstByXPath(searchValue, rootElement);
            }
            else {
                var condition = ByHelper.GetStrategy(searchStrategy, searchValue);

                element = rootElement.FindFirstDescendant(condition);
            }

            if (element == null) {
                throw new AutomationException("Element cannot be found", ResponseStatus.NoSuchElement);
            }

            var registeredKey = this.Automator.ElementsRegistry.RegisterElement(new FlaUiDriverElement(element));
            var registeredObject = new JsonElementContent(registeredKey);

            return this.JsonResponse(ResponseStatus.Success, registeredObject);
        }

    }

}