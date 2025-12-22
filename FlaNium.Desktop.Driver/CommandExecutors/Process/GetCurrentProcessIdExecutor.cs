using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.FlaUI;

namespace FlaNium.Desktop.Driver.CommandExecutors.Process {

    internal class GetCurrentProcessIdExecutor: CommandExecutorBase {

        protected override JsonResponse  DoImpl() {

            int id = DriverManager.Application?.ProcessId ?? -1;

            return JsonResponse(ResponseStatus.Success, id);
        }

    }

}