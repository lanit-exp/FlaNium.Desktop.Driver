using System;
using System.IO;
using FlaNium.Desktop.Driver.Common;

namespace FlaNium.Desktop.Driver.CommandExecutors.Auxiliary {

    internal class FileDownloadExecutor : CommandExecutorBase {

        protected override JsonResponse DoImpl() {
            var path = ExecutedCommand.Parameters["path"].ToString();

            path = Utils.ReplaceSystemVarsIfPresent(path);

            if (!File.Exists(path)) {
                return JsonResponse(ResponseStatus.NoSuchElement, $"File not found: '{path}'");
            }

            return JsonResponse(ResponseStatus.Success, Convert.ToBase64String(File.ReadAllBytes(path)));
        }

    }

}