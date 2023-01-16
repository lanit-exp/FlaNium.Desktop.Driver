using FlaNium.Desktop.Driver.Common;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.DateTimePicker {

    class DateTimePickerGetDateExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var dateTimePicker = element.FlaUiElement.AsDateTimePicker();

            var result = dateTimePicker.SelectedDate;

            return this.JsonResponse(ResponseStatus.Success, result.ToString());
        }

    }

}