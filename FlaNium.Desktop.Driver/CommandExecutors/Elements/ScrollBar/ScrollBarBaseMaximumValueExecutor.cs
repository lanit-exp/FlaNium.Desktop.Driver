using FlaNium.Desktop.Driver.Common;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.ScrollBar {

    class ScrollBarBaseMaximumValueExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var scroll = element.FlaUiElement.AsVerticalScrollBar();

            var result = scroll.MaximumValue;

            return this.JsonResponse(ResponseStatus.Success, result);
        }

    }

}