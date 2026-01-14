using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.FlaUI;

namespace FlaNium.Desktop.Driver.CommandExecutors.Process {

    internal class ChangeProcessExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            string name = ExecutedCommand.Parameters["name"].ToString();
            int timeOut = int.Parse(ExecutedCommand.Parameters["timeout"].ToString());
            
            DriverManager.AttachToProcess(name, timeOut);

            return JsonResponse();
        }

    }

}