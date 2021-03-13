
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.ToggleButton
{
    class ToggleButtonSetToggleStateExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();
            
            var value = this.ExecutedCommand.Parameters["value"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var toggleButton = element.FlaUIElement.AsToggleButton();

            ToggleState state;

            switch (value) {
                case "On": state = ToggleState.On;
                    break;
                case "Off":
                    state = ToggleState.Off;
                    break;
                case "Indeterminate":
                    state = ToggleState.Indeterminate;
                    break;
                default: return this.JsonResponse();
            }

            toggleButton.ToggleState = state;

            return this.JsonResponse();
        }

        #endregion
    }
}
