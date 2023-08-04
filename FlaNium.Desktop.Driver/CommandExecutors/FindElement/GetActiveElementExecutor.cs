using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.Exceptions;
using FlaNium.Desktop.Driver.FlaUI;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.FindElement {

    internal class GetActiveElementExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            AutomationElement uiItem = DriverManager.GetRootElement().Automation.FocusedElement();

            if (uiItem == null)
                throw new AutomationException("Active cannot be found ", ResponseStatus.NoSuchElement);

            var registeredKey = this.Automator.ElementsRegistry.RegisterElement(new FlaUiDriverElement(uiItem));
            var registeredObject = new JsonElementContent(registeredKey);

            return this.JsonResponse(ResponseStatus.Success, registeredObject);
        }

    }

}