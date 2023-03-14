using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.Exceptions;
using FlaNium.Desktop.Driver.FlaUI;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Grid {

    class GridGetHeaderExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            global::FlaUI.Core.AutomationElements.Grid grid = element.FlaUiElement.AsGrid();

            var result = grid.Header;

            if (result == null) {
                throw new AutomationException("Element cannot be found", ResponseStatus.NoSuchElement);
            }

            var itemRegisteredKey = this.Automator.ElementsRegistry.RegisterElement(new FlaUiDriverElement(result));

            var registeredObject = new JsonElementContent(itemRegisteredKey);

            return this.JsonResponse(ResponseStatus.Success, registeredObject);
        }

    }

}