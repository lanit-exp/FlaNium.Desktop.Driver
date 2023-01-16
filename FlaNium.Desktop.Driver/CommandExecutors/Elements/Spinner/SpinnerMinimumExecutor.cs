
using FlaUI.Core.AutomationElements;
using FlaNium.Desktop.Driver.Common;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Spinner
{
    class SpinnerMinimumExecutor : CommandExecutorBase
    {
       

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var spinner = element.FlaUiElement.AsSpinner();

            double value = spinner.Minimum;

            return this.JsonResponse(ResponseStatus.Success, value);
        }

     
    }
}
