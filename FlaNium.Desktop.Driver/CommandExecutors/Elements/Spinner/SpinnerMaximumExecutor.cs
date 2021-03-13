
using FlaUI.Core.AutomationElements;
using FlaNium.Desktop.Driver.Common;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Spinner
{
    class SpinnerMaximumExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var spinner = element.FlaUIElement.AsSpinner();

            double value = spinner.Maximum;

            return this.JsonResponse(ResponseStatus.Success, value.ToString());
        }

        #endregion
    }
}
