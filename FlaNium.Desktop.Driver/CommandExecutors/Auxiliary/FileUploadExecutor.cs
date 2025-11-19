using System;
using System.IO;
using FlaNium.Desktop.Driver.Common;

namespace FlaNium.Desktop.Driver.CommandExecutors.Auxiliary {

    internal class FileUploadExecutor : CommandExecutorBase {

        protected override JsonResponse DoImpl() {
            var path = ExecutedCommand.Parameters["path"].ToString();
            var base64Bytes = ExecutedCommand.Parameters["bytes"].ToString();

            path = Utils.ReplaceSystemVarsIfPresent(path);

            byte[] bytes = Convert.FromBase64String(base64Bytes);


            if (File.Exists(path)) {
                return JsonResponse(ResponseStatus.UnknownError, $"File already exists: '{path}'");
            }

            string directory = Path.GetDirectoryName(path);

            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory)) {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllBytes(path, bytes);

            return JsonResponse();
        }

    }

}