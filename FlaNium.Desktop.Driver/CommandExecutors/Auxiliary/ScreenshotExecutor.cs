using System;
using System.Drawing.Imaging;
using System.IO;
using FlaNium.Desktop.Driver.Common;
using FlaUI.Core.Capturing;

namespace FlaNium.Desktop.Driver.CommandExecutors.Auxiliary {

    internal class ScreenshotExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            CaptureImage captureImage = Capture.Screen();
            MemoryStream memoryStream = new MemoryStream();

            captureImage.Bitmap.Save((Stream)memoryStream, ImageFormat.Jpeg);

            return this.JsonResponse(ResponseStatus.Success, (object)Convert.ToBase64String(memoryStream.ToArray()));
        }

    }

}