using FlaNium.Desktop.Driver.Common;

namespace FlaNium.Desktop.Driver.CommandExecutors.Auxiliary {

    internal class FileOrDirectoryExistsExecutor : CommandExecutorBase {

        protected override JsonResponse DoImpl() {
            var path = ExecutedCommand.Parameters["path"].ToString();

            path = Utils.ReplaceSystemVarsIfPresent(path);

            return JsonResponse(ResponseStatus.Success, Utils.PathExists(path));
        }

    }

}