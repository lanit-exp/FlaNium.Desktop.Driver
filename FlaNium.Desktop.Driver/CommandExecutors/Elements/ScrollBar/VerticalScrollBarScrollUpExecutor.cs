
namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.ScrollBar
{
    using global::FlaUI.Core.AutomationElements;
    using global::FlaUI.Core.AutomationElements.Scrolling;

    class VerticalScrollBarScrollUpExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            VerticalScrollBar scroll = element.FlaUIElement.AsVerticalScrollBar();

            scroll.ScrollUp();

            return this.JsonResponse();
        }

        #endregion
    }
}
