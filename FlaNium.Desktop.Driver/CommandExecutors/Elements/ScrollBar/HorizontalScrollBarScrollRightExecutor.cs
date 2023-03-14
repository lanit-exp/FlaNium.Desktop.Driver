using FlaUI.Core.AutomationElements;
using FlaUI.Core.AutomationElements.Scrolling;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.ScrollBar {

    class HorizontalScrollBarScrollRightExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            HorizontalScrollBar scroll = element.FlaUiElement.AsHorizontalScrollBar();

            scroll.ScrollRight();

            return this.JsonResponse();
        }

    }

}