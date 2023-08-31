using FlaNium.Desktop.Driver.Common;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.AutomationElements.Scrolling;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.ScrollBar {

    class HorizontalScrollBarScrollRightLargeExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            HorizontalScrollBar scroll = element.FlaUiElement.AsHorizontalScrollBar();

            scroll.ScrollRightLarge();

            return this.JsonResponse();
        }

    }

}