using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.FlaUI;

namespace FlaNium.Desktop.Driver.CommandExecutors.Process {

    internal class GetProcessIdByNameExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            string name = ExecutedCommand.Parameters["name"].ToString();
            
            int[] ids = DriverManager.GetProcessIdsByName(name);

            return JsonResponse(ResponseStatus.Success, ids);
        }

    }

}