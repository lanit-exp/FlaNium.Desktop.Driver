using System;
using System.Drawing.Imaging;
using System.IO;
using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.FlaUI;
using FlaUI.Core.Capturing;

namespace FlaNium.Desktop.Driver.CommandExecutors.Auxiliary {

    internal class ElementScreenshotExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var elementId = this.ExecutedCommand.Parameters["ID"].ToString();
            var imageFormatStr = this.ExecutedCommand.Parameters["format"].ToString();
            var foreground = Boolean.Parse(this.ExecutedCommand.Parameters["foreground"].ToString());

            FlaUiDriverElement element = this.Automator.ElementsRegistry.GetRegisteredElement(elementId);

            ImageFormat imageFormat = ImFormat.GetImageFormat(imageFormatStr);

            MemoryStream memoryStream = new MemoryStream();
            CaptureImage captureImage;

            if (foreground) {
                captureImage = Capture.Element(element.FlaUiElement);
            }
            else {
                captureImage = ElementCapture.CaptureImageOfElement(element.FlaUiElement);
            }

            captureImage.Bitmap.Save(memoryStream, imageFormat);

            return this.JsonResponse(ResponseStatus.Success, Convert.ToBase64String(memoryStream.ToArray()));
        }

    }

}