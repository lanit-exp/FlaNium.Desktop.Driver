namespace FlaNium.Desktop.Driver.CommandExecutors
{
    #region using

    using global::FlaUI.Core.AutomationElements;
    using FlaNium.Desktop.Driver.FlaUI;
    using FlaNium.Desktop.Driver.Common;


    #endregion

    internal class GetElementTagNameExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            FlaUIDriverElement element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            AutomationElement flaUiElement = element.FlaUIElement;

            var value = flaUiElement.Properties.ControlType.ToString();

            return this.JsonResponse(ResponseStatus.Success, value);

        }

        #endregion
    }
}
