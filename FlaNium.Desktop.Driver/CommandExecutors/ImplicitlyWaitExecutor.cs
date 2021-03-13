namespace FlaNium.Desktop.Driver.CommandExecutors
{
    #region using

    using System;
    using FlaNium.Desktop.Driver.FlaUI;
    

    #endregion

    internal class ImplicitlyWaitExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            if (this.ExecutedCommand.Parameters.ContainsKey("ms"))
            {
                int parameter = (int)this.ExecutedCommand.Parameters["ms"];
                if (parameter > 5000)
                    DriverManager.ImplicitTimeout = TimeSpan.FromMilliseconds((double)parameter);
            }
            else if (this.ExecutedCommand.Parameters.ContainsKey("s"))
                DriverManager.ImplicitTimeout = TimeSpan.FromSeconds((double)this.ExecutedCommand.Parameters["s"]);
            else if (this.ExecutedCommand.Parameters.ContainsKey("m"))
                DriverManager.ImplicitTimeout = TimeSpan.FromMinutes((double)this.ExecutedCommand.Parameters["m"]);
            else
                Logger.Error("Unknown time unit!");
            return this.JsonResponse();
        }

        #endregion
    }
}
