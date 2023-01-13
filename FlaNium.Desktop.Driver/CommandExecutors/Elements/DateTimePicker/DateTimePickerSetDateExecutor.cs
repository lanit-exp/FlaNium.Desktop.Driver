using System;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.DateTimePicker {

    class DateTimePickerSetDateExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var value = this.ExecutedCommand.Parameters["dateTime"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var dateTimePicker = element.FlaUIElement.AsDateTimePicker();

            DateTime date = DateTime.Parse(value);

            dateTimePicker.SelectedDate = date;

            return this.JsonResponse();
        }

    }

}