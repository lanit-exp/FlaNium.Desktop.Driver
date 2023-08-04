using FlaNium.Desktop.Driver.Common;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.ProgressBar {

    class ProgressBarMaximumExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var progressBar = element.FlaUiElement.AsProgressBar();

            var result = progressBar.Maximum;

            return this.JsonResponse(ResponseStatus.Success, result.ToString());
        }

    }

}