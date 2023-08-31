using System;
using FlaNium.Desktop.Driver.Common;

namespace FlaNium.Desktop.Driver.CommandExecutors.Auxiliary {

    internal class DeleteFileOrDirectoryExecutor : CommandExecutorBase {

        protected override JsonResponse DoImpl() {
            var path = ExecutedCommand.Parameters["path"].ToString();

            path = Utils.ReplaceSystemVarsIfPresent(path);

            try {
                Utils.DeletePath(path);
            }
            catch (Exception e) {
                return JsonResponse(ResponseStatus.UnknownError, e);
            }

            return JsonResponse();
        }

    }

}