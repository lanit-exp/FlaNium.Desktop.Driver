using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Spinner {

    class SpinnerDecrementExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var spinner = element.FlaUiElement.AsSpinner();

            spinner.Decrement();

            return this.JsonResponse();
        }

    }

}