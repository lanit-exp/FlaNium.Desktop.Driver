using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.FlaUI;

namespace FlaNium.Desktop.Driver.CommandExecutors.Process {

    internal class ChangeProcessByIdExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            int id = int.Parse(ExecutedCommand.Parameters["id"].ToString());
            
            DriverManager.AttachToProcessById(id);

            return JsonResponse();
        }

    }

}