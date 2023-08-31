﻿using System;
using System.Collections.Generic;
using FlaUI.Core.AutomationElements;
using FlaNium.Desktop.Driver.Common;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Calendar {

    class CalendarSelectedDatesExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var calendar = element.FlaUiElement.AsCalendar();

            var items = calendar.SelectedDates;

            var result = new List<DateTime>(items);

            return this.JsonResponse(ResponseStatus.Success, result);
        }

    }

}