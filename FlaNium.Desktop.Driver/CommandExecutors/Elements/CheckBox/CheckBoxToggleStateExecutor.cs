
namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.CheckBox
{
    using global::FlaUI.Core.AutomationElements;
    using FlaNium.Desktop.Driver.Common;
    class CheckBoxToggleStateExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            CheckBox checkBox = element.FlaUIElement.AsCheckBox();

            var ToggleState = checkBox.Patterns.Toggle.PatternOrDefault.ToggleState;

            return this.JsonResponse(ResponseStatus.Success, ToggleState.ToString());
        }

        #endregion
    }
}
