using System;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Calendar {

    class CalendarAddToSelectionExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var value = this.ExecutedCommand.Parameters["dateTime"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var calendar = element.FlaUiElement.AsCalendar();

            DateTime date = DateTime.Parse(value);

            calendar.AddToSelection(date);

            return this.JsonResponse();
        }

    }

}