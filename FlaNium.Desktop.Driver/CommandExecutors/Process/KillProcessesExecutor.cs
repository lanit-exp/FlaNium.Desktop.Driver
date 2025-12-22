using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.FlaUI;

namespace FlaNium.Desktop.Driver.CommandExecutors.Process {

    internal class KillProcessesExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            string name = ExecutedCommand.Parameters["name"].ToString();
            
            int processesCount = DriverManager.KillAllProcessByName(name);

            return JsonResponse(ResponseStatus.Success, processesCount);
        }

    }

}