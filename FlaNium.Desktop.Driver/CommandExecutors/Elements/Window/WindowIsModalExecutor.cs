
using FlaUI.Core.AutomationElements;
using FlaNium.Desktop.Driver.Common;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Window
{
    class WindowIsModalExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var window = element.FlaUIElement.AsWindow();

            bool value = window.IsModal;

            return this.JsonResponse(ResponseStatus.Success, value.ToString());
        }

        #endregion
    }
}
