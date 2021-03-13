namespace FlaNium.Desktop.Driver.CommandExecutors
{
    
    #region using

    using global::FlaUI.Core.AutomationElements;
    using FlaNium.Desktop.Driver.Extensions;
    using FlaNium.Desktop.Driver.FlaUI;
    using FlaNium.Desktop.Driver.Common;
    using FlaNium.Desktop.Driver.Exceptions;

    #endregion

    internal class FindElementExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            
            var searchValue = this.ExecutedCommand.Parameters["value"].ToString();
            var searchStrategy = this.ExecutedCommand.Parameters["using"].ToString();

            
            AutomationElement activeWindow = DriverManager.GetActiveWindow();

            AutomationElement element;

            if (searchStrategy.Equals("xpath"))
            {
                if (searchValue.StartsWith("#")) {
                    searchValue = searchValue.TrimStart('#');
                    activeWindow = activeWindow.Automation.GetDesktop();
                }

               element = ByXpath.FindFirstByXPath(searchValue, activeWindow);
            }
            else
            {
                var condition = ByHelper.GetStrategy(searchStrategy, searchValue);

                element = activeWindow.FindFirstDescendant(condition);
            }

            if (element == null)
            {
                throw new AutomationException("Element cannot be found", ResponseStatus.NoSuchElement);
            }

            var registeredKey = this.Automator.ElementsRegistry.RegisterElement(new FlaUIDriverElement(element));
            var registeredObject = new JsonElementContent(registeredKey);
            return this.JsonResponse(ResponseStatus.Success, registeredObject);
        }

        #endregion
    }
}
