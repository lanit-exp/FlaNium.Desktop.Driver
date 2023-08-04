using FlaNium.Desktop.Driver.Common;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.AutomationElements.Scrolling;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.ScrollBar {

    class VerticalScrollBarScrollDownExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            VerticalScrollBar scroll = element.FlaUiElement.AsVerticalScrollBar();

            scroll.ScrollDown();

            return this.JsonResponse();
        }

    }

}