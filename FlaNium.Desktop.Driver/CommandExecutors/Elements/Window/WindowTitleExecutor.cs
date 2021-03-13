
using FlaUI.Core.AutomationElements;
using FlaNium.Desktop.Driver.Common;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Window
{
    class WindowTitleExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var window = element.FlaUIElement.AsWindow();

            string value = window.Title;

            return this.JsonResponse(ResponseStatus.Success, value);
        }

        #endregion
    }
}
