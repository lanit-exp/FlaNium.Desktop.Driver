using System;
using System.Drawing.Imaging;
using System.IO;
using FlaNium.Desktop.Driver.CommandHelpers;
using FlaNium.Desktop.Driver.Common;
using FlaUI.Core.Capturing;

namespace FlaNium.Desktop.Driver.CommandExecutors.Auxiliary {

    internal class ScreenshotExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            CaptureImage captureImage = CaptureActions.Screen();
            MemoryStream memoryStream = new MemoryStream();

            captureImage.Bitmap.Save(memoryStream, ImageFormat.Jpeg);

            return this.JsonResponse(ResponseStatus.Success, Convert.ToBase64String(memoryStream.ToArray()));
        }

    }

}