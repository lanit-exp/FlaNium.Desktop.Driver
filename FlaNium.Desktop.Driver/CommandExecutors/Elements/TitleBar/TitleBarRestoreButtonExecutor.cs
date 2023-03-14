using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.Exceptions;
using FlaNium.Desktop.Driver.FlaUI;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.TitleBar {

    class TitleBarRestoreButtonExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var titleBar = element.FlaUiElement.AsTitleBar();

            var result = titleBar.RestoreButton;

            if (result == null) {
                throw new AutomationException("Element cannot be found", ResponseStatus.NoSuchElement);
            }

            var itemRegisteredKey = this.Automator.ElementsRegistry.RegisterElement(new FlaUiDriverElement(result));

            var registeredObject = new JsonElementContent(itemRegisteredKey);

            return this.JsonResponse(ResponseStatus.Success, registeredObject);
        }

    }

}