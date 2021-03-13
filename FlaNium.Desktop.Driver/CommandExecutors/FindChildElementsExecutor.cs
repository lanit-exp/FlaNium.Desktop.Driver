namespace FlaNium.Desktop.Driver.CommandExecutors
{
    #region using

    using System.Linq;
    using global::FlaUI.Core.AutomationElements;
    using FlaNium.Desktop.Driver.Extensions;
    using FlaNium.Desktop.Driver.Common;
    using FlaUI;
    using System;

    #endregion

    internal class FindChildElementsExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var parentKey = this.ExecutedCommand.Parameters["ID"].ToString();
            var searchValue = this.ExecutedCommand.Parameters["value"].ToString();
            var searchStrategy = this.ExecutedCommand.Parameters["using"].ToString();

            var parent = this.Automator.ElementsRegistry.GetRegisteredElement(parentKey);

            AutomationElement[] elements;

            if (searchStrategy.Equals("xpath"))
            {
                elements = ByXpath.FindAllByXPath(searchValue, parent.FlaUIElement);
            }
            else
            {
                var condition = ByHelper.GetStrategy(searchStrategy, searchValue);

                elements = parent.FlaUIElement.FindAllDescendants(condition);
            }

            var flaUiDriverElementList = elements
                .Select<AutomationElement, FlaUIDriverElement>((Func<AutomationElement, FlaUIDriverElement>)(x => new FlaUIDriverElement(x)))
                .ToList<FlaUIDriverElement>();

            var registeredKeys = this.Automator.ElementsRegistry.RegisterElements(flaUiDriverElementList);

            var registeredObjects = registeredKeys.Select(e => new JsonElementContent(e));
            return this.JsonResponse(ResponseStatus.Success, registeredObjects);
               
        }
        
        #endregion
    }
}
