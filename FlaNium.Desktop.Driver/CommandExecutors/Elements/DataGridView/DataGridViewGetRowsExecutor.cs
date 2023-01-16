using System;
using System.Linq;
using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.FlaUI;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.DataGridView {

    class DataGridViewGetRowsExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            global::FlaUI.Core.AutomationElements.DataGridView item = element.FlaUiElement.AsDataGridView();

            DataGridViewRow[] rows = item.Rows;

            var flaUiDriverElementList = rows
                .Select(
                    (Func<AutomationElement, FlaUiDriverElement>)(x => new FlaUiDriverElement(x)))
                .ToList();

            var registeredKeys = this.Automator.ElementsRegistry.RegisterElements(flaUiDriverElementList);

            var registeredObjects = registeredKeys.Select(e => new JsonElementContent(e));

            return this.JsonResponse(ResponseStatus.Success, registeredObjects);
        }

    }

}