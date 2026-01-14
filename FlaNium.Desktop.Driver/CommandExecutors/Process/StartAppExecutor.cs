using System;
using System.Threading;
using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.FlaUI;

namespace FlaNium.Desktop.Driver.CommandExecutors.Process {

    internal class StartAppExecutor : CommandExecutorBase {

        protected override JsonResponse DoImpl() {
            string appPath = ExecutedCommand.Parameters["appPath"].ToString();

            appPath = Utils.ReplaceSystemVarsIfPresent(appPath);
            
            string appArguments = ExecutedCommand.Parameters["appArguments"]?.ToString() ?? "";
            int launchDelay = int.Parse(ExecutedCommand.Parameters["launchDelay"].ToString());
            bool startSecondInstance = bool.Parse(ExecutedCommand.Parameters["startSecondInstance"].ToString());

            try {
                DriverManager.StartApp(appPath, appArguments, startSecondInstance);
                DriverManager.ResetRootElement();
                Thread.Sleep(launchDelay);
            }
            catch (Exception e) {
                return JsonResponse(ResponseStatus.UnknownError, e);
            }

            int id = DriverManager.Application?.ProcessId ?? -1;
            
            return JsonResponse(ResponseStatus.Success, id);
        }

    }

}