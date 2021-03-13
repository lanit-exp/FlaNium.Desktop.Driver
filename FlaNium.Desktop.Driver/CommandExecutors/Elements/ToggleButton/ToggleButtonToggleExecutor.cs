
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.ToggleButton
{
    class ToggleButtonToggleExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var toggleButton = element.FlaUIElement.AsToggleButton();

            toggleButton.Toggle();

            return this.JsonResponse();
        }

        #endregion
    }
}
