using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;

// ReSharper disable once CheckNamespace
namespace FlaNium.Desktop.Driver.CommandExecutors.StandartActions {

    internal class SubmitElementExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            element.FlaUiElement.Focus();

            Keyboard.Press(VirtualKeyShort.ENTER);

            return this.JsonResponse();
        }

    }

}