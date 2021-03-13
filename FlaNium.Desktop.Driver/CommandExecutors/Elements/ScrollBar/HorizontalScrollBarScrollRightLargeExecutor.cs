
namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.ScrollBar
{
    using global::FlaUI.Core.AutomationElements;
    using global::FlaUI.Core.AutomationElements.Scrolling;

    class HorizontalScrollBarScrollRightLargeExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            HorizontalScrollBar scroll = element.FlaUIElement.AsHorizontalScrollBar();

            scroll.ScrollRightLarge();

            return this.JsonResponse();
        }

        #endregion
    }
}
