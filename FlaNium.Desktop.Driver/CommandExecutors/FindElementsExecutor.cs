namespace FlaNium.Desktop.Driver.CommandExecutors
{
    using System;
    #region using

    using System.Linq;
    using global::FlaUI.Core.AutomationElements;
    using FlaNium.Desktop.Driver.Extensions;
    using FlaNium.Desktop.Driver.FlaUI;
    using FlaNium.Desktop.Driver.Common;
    using FlaNium.Desktop.Driver.Exceptions;

    #endregion

    internal class FindElementsExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var searchValue = this.ExecutedCommand.Parameters["value"].ToString();
            var searchStrategy = this.ExecutedCommand.Parameters["using"].ToString();


            AutomationElement activeWindow = DriverManager.GetActiveWindow();
            AutomationElement[] elements;

            if (searchStrategy.Equals("xpath"))
            {
                if (searchValue.StartsWith("#"))
                {
                    searchValue = searchValue.TrimStart('#');
                    activeWindow = activeWindow.Automation.GetDesktop();
                }

                elements = ByXpath.FindAllByXPath(searchValue, activeWindow);
            }
            else
            {
                var condition = ByHelper.GetStrategy(searchStrategy, searchValue);

                elements = activeWindow.FindAllDescendants(condition);
            }

            if (elements == null)
            {
                throw new AutomationException("Element cannot be found", ResponseStatus.NoSuchElement);
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
