﻿using FlaNium.Desktop.Driver.Common;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Grid {

    class GridRowCountExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            global::FlaUI.Core.AutomationElements.Grid grid = element.FlaUiElement.AsGrid();

            var result = grid.RowCount;

            return this.JsonResponse(ResponseStatus.Success, result);
        }

    }

}