
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Window
{
    class WindowMoveExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();
           
            var x = this.ExecutedCommand.Parameters["x"].ToString();
            var y = this.ExecutedCommand.Parameters["y"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var window = element.FlaUIElement.AsWindow();

            window.Move(int.Parse(x), int.Parse(y));

            return this.JsonResponse();
        }

        #endregion
    }
}
