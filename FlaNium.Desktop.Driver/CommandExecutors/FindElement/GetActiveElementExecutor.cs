using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.Exceptions;
using FlaNium.Desktop.Driver.FlaUI;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.FindElement {

    internal class GetActiveElementExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            AutomationElement uiItem = DriverManager.GetActiveWindow().Automation.FocusedElement();

            if (uiItem == null)
                throw new AutomationException("Active cannot be found ", ResponseStatus.NoSuchElement);

            var registeredKey = this.Automator.ElementsRegistry.RegisterElement(new FlaUIDriverElement(uiItem));
            var registeredObject = new JsonElementContent(registeredKey);

            return this.JsonResponse(ResponseStatus.Success, registeredObject);
        }

    }

}