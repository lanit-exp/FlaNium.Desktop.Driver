using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.FlaUI;

namespace FlaNium.Desktop.Driver.CommandExecutors {

    internal class SetElementFocusExecutor: CommandExecutorBase {

        protected override JsonResponse  DoImpl() {

            string id = this.ExecutedCommand.Parameters["id"].ToString();
            FlaUiDriverElement element = this.Automator.ElementsRegistry.GetRegisteredElement(id);

            element.FlaUiElement.SetForeground();

            return this.JsonResponse();
        }

    }

}