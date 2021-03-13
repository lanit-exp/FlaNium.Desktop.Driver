
namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.ScrollBar
{
    using global::FlaUI.Core.AutomationElements;
    using FlaNium.Desktop.Driver.Common;

    class ScrollBarBaseLargeChangeExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var scroll = element.FlaUIElement.AsVerticalScrollBar();

            var result = scroll.LargeChange;

            return this.JsonResponse(ResponseStatus.Success, result.ToString());
        }

        #endregion
    }
}
