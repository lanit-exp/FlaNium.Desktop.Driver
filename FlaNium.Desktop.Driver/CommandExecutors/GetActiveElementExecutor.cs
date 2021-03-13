namespace FlaNium.Desktop.Driver.CommandExecutors
{
    #region using

    using FlaNium.Desktop.Driver.Common;
    using FlaUI;
    using FlaNium.Desktop.Driver.Exceptions;
    using global::FlaUI.Core.AutomationElements;
    #endregion

    internal class GetActiveElementExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            AutomationElement uiItem = DriverManager.GetActiveWindow().Automation.FocusedElement();

            if (uiItem == null)
                throw new AutomationException("Active cannot be found ", ResponseStatus.NoSuchElement);

            var registeredKey = this.Automator.ElementsRegistry.RegisterElement(new FlaUIDriverElement(uiItem));
            var registeredObject = new JsonElementContent(registeredKey);
           
            return this.JsonResponse(ResponseStatus.Success, registeredObject);
                        
        }

        #endregion
    }
}
