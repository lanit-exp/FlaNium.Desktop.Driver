
using FlaUI.Core.AutomationElements;
using FlaNium.Desktop.Driver.Common;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.ToggleButton
{
    class ToggleButtonGetToggleStateExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var toggleButton = element.FlaUIElement.AsToggleButton();

            var result = toggleButton.ToggleState;

            return this.JsonResponse(ResponseStatus.Success, result.ToString());
        }

        #endregion
    }
}
