using System;
using System.Linq;
using FlaNium.Desktop.Driver.FlaUI;

namespace FlaNium.Desktop.Driver.CommandExecutors.StandardActions {

    internal class SendKeysToActiveElementExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var chars = this.ExecutedCommand.Parameters["value"].Select(x => Convert.ToChar(x.ToString()));

            DriverManager.GetRootElement().SetForeground();

            this.Automator.FlaNiumKeyboard.SendKeys(chars.ToArray());

            return this.JsonResponse();
        }

    }

}