
namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.DataGridView
{
    using System;
    using System.Linq;
    using global::FlaUI.Core.AutomationElements;
    using FlaNium.Desktop.Driver.FlaUI;
    using FlaNium.Desktop.Driver.Common;

    class DataGridViewGetRowsExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            DataGridView item = element.FlaUIElement.AsDataGridView();

            DataGridViewRow[] rows = item.Rows;

            var flaUiDriverElementList = rows
                .Select<AutomationElement, FlaUIDriverElement>((Func<AutomationElement, FlaUIDriverElement>)(x => new FlaUIDriverElement(x)))
                .ToList<FlaUIDriverElement>();

            var registeredKeys = this.Automator.ElementsRegistry.RegisterElements(flaUiDriverElementList);

            var registeredObjects = registeredKeys.Select(e => new JsonElementContent(e));

            return this.JsonResponse(ResponseStatus.Success, registeredObjects);
        }

        #endregion
    }
}
