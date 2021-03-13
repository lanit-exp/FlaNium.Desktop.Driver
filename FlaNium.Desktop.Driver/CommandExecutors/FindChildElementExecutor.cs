   
namespace FlaNium.Desktop.Driver.CommandExecutors
{
    using FlaNium.Desktop.Driver.Extensions;
    #region using

    using FlaNium.Desktop.Driver.FlaUI;
    using FlaNium.Desktop.Driver.Common;
    using FlaNium.Desktop.Driver.Exceptions;
    using global::FlaUI.Core.AutomationElements;

    #endregion

    internal class FindChildElementExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var parentKey = this.ExecutedCommand.Parameters["ID"].ToString();
            var searchValue = this.ExecutedCommand.Parameters["value"].ToString();
            var searchStrategy = this.ExecutedCommand.Parameters["using"].ToString();

            var parent = this.Automator.ElementsRegistry.GetRegisteredElement(parentKey);

            AutomationElement element;

            if (searchStrategy.Equals("xpath"))
            {
                element = ByXpath.FindFirstByXPath(searchValue, parent.FlaUIElement);
            }
            else {
                var condition = ByHelper.GetStrategy(searchStrategy, searchValue);

                element = parent.FlaUIElement.FindFirstDescendant(condition);
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
