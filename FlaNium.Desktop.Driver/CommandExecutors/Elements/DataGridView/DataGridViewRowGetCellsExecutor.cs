using System;
using System.Linq;
using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.FlaUI;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.DataGridView {

    class DataGridViewRowGetCellsExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            DataGridViewRow item = (DataGridViewRow)element.FlaUiElement;

            var cells = item.Cells;

            var flaUiDriverElementList = cells
                .Select(
                    (Func<AutomationElement, FlaUiDriverElement>)(x => new FlaUiDriverElement(x)))
                .ToList();

            var registeredKeys = Automator.ElementsRegistry.RegisterElements(flaUiDriverElementList);

            var registeredObjects = registeredKeys.Select(e => new JsonElementContent(e));

            return this.JsonResponse(ResponseStatus.Success, registeredObjects);
        }

    }

}