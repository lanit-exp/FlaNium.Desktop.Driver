using System.Linq;
using FlaNium.Desktop.Driver.FlaUI;

namespace FlaNium.Desktop.Driver.CommandExecutors.StandardActions {

    internal class SendCharsToActiveElementExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var chars = this.ExecutedCommand.Parameters["value"].ToString();

            DriverManager.GetActiveWindow().SetForeground();

            this.Automator.FlaNiumKeyboard.SendKeys(chars.ToArray());

            return this.JsonResponse();
        }

    }

}