using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.Input;

namespace FlaNium.Desktop.Driver.CommandExecutors.Auxiliary {

    class SetKeyboardLayoutExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            var layoutValue = this.ExecutedCommand.Parameters["value"].ToString();

            KeyboardLayout.SwitchInputLanguage(layoutValue);

            return this.JsonResponse();
        }

    }

}