using System;
using FlaNium.Desktop.Driver.Common;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.DateTimePicker {

    class DateTimePickerSetDateExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var value = this.ExecutedCommand.Parameters["dateTime"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var dateTimePicker = element.FlaUiElement.AsDateTimePicker();

            DateTime date = DateTime.Parse(value);

            dateTimePicker.SelectedDate = date;

            return this.JsonResponse();
        }

    }

}