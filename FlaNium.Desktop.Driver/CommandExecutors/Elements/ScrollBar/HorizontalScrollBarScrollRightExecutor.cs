
namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.ScrollBar
{
    using global::FlaUI.Core.AutomationElements;
    using global::FlaUI.Core.AutomationElements.Scrolling;

    class HorizontalScrollBarScrollRightExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            HorizontalScrollBar scroll = element.FlaUIElement.AsHorizontalScrollBar();

            scroll.ScrollRight();

            return this.JsonResponse();
        }

        #endregion
    }
}
