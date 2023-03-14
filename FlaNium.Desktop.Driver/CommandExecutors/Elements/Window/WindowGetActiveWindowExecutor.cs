using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.FlaUI;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Window {

    class WindowGetActiveWindowExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var activeWindow = DriverManager.GetRootElement();

            var itemRegisteredKey =
                this.Automator.ElementsRegistry.RegisterElement(new FlaUiDriverElement(activeWindow));

            var registeredObject = new JsonElementContent(itemRegisteredKey);

            return this.JsonResponse(ResponseStatus.Success, registeredObject);
        }

    }

}