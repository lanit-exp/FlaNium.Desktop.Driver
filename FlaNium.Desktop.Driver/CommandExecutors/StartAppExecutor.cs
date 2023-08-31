using System;
using System.Threading;
using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.FlaUI;

namespace FlaNium.Desktop.Driver.CommandExecutors {

    internal class StartAppExecutor : CommandExecutorBase {

        protected override JsonResponse DoImpl() {
            string appPath = ExecutedCommand.Parameters["appPath"].ToString();

            appPath = Utils.ReplaceSystemVarsIfPresent(appPath);
            
            string appArguments = ExecutedCommand.Parameters["appArguments"]?.ToString() ?? "";
            int launchDelay = int.Parse(ExecutedCommand.Parameters["launchDelay"].ToString());


            try {
                DriverManager.StartApp(appPath, appArguments);
                DriverManager.ResetRootElement();
                Thread.Sleep(launchDelay);
            }
            catch (Exception e) {
                return JsonResponse(ResponseStatus.UnknownError, e);
            }

            return this.JsonResponse();
        }

    }

}