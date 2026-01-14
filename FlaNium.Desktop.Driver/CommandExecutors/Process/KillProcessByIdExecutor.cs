using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.FlaUI;

namespace FlaNium.Desktop.Driver.CommandExecutors.Process {

    internal class KillProcessByIdExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            int id = int.Parse(ExecutedCommand.Parameters["id"].ToString());
            
            bool status = DriverManager.KillProcessById(id);

            return JsonResponse(ResponseStatus.Success, status);
        }

    }

}