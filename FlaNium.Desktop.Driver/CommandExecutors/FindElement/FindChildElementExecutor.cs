using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.Exceptions;
using FlaNium.Desktop.Driver.ElementFindStrategy;
using FlaNium.Desktop.Driver.FlaUI;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.FindElement {

    internal class FindChildElementExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            var parentKey = this.ExecutedCommand.Parameters["ID"].ToString();
            var searchValue = this.ExecutedCommand.Parameters["value"].ToString();
            var searchStrategy = this.ExecutedCommand.Parameters["using"].ToString();

            var parent = this.Automator.ElementsRegistry.GetRegisteredElement(parentKey);

            AutomationElement element;

            if (searchStrategy.Equals("xpath")) {
                element = ByXpath.FindFirstByXPath(searchValue, parent.FlaUiElement);
            }
            else {
                var condition = ByHelper.GetStrategy(searchStrategy, searchValue);

                element = parent.FlaUiElement.FindFirstDescendant(condition);
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