namespace FlaNium.Desktop.Driver.CommandExecutors
{

    #region using
    using System;
    using System.Linq;
    using FlaNium.Desktop.Driver.FlaUI;
    #endregion

    internal class SendKeysToActiveElementExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {               
            var chars = this.ExecutedCommand.Parameters["value"].Select(x => Convert.ToChar(x.ToString()));

            DriverManager.GetActiveWindow().SetForeground();

            this.Automator.FlaNiumKeyboard.SendKeys(chars.ToArray());

            return this.JsonResponse();

        }

        #endregion
    }
}
