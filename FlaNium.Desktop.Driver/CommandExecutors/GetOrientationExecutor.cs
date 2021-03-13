namespace FlaNium.Desktop.Driver.CommandExecutors
{
    
    #region using
        
    using FlaNium.Desktop.Driver.Common;
    using global::FlaUI.Core.AutomationElements;
    using global::FlaUI.Core.Definitions;
    using FlaNium.Desktop.Driver.FlaUI;

    #endregion

    internal class GetOrientationExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();
            
            FlaUIDriverElement element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            AutomationElement flaUiElement = element.FlaUIElement;


            OrientationType orientationType = (OrientationType) flaUiElement.Properties.Orientation.ValueOrDefault;

            return this.JsonResponse(ResponseStatus.Success, orientationType.ToString());

        }

        #endregion
    }
}