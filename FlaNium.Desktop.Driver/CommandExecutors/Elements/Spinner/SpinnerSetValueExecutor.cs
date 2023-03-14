using System;
using System.Globalization;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Spinner {

    class SpinnerSetValueExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var value = this.ExecutedCommand.Parameters["value"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var spinner = element.FlaUiElement.AsSpinner();

            spinner.Value = Convert.ToDouble(value, CultureInfo.InvariantCulture);

            return this.JsonResponse();
        }

    }

}