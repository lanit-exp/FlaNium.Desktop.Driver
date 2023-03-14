using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Window {

    class WindowMoveExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var x = this.ExecutedCommand.Parameters["x"].ToString();
            var y = this.ExecutedCommand.Parameters["y"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var window = element.FlaUiElement.AsWindow();

            window.Move(int.Parse(x), int.Parse(y));

            return this.JsonResponse();
        }

    }

}