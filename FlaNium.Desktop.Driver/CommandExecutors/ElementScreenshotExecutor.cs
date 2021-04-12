namespace FlaNium.Desktop.Driver.CommandExecutors
{

    using System;
    using System.Drawing.Imaging;
    using System.IO;
    using global::FlaUI.Core.Capturing;
    using FlaNium.Desktop.Driver.Common;

    internal class ElementScreenshotExecutor : CommandExecutorBase
    {
       
        protected override string DoImpl()
        {
            var elementId = this.ExecutedCommand.Parameters["ID"].ToString();
            var imageFormatStr = this.ExecutedCommand.Parameters["format"].ToString();


            var element = this.Automator.ElementsRegistry.GetRegisteredElement(elementId);

            ImageFormat imageFormat = ImFormat.GetImageFormat(imageFormatStr);

            CaptureImage captureImage = Capture.Element(element.FlaUIElement);

            MemoryStream memoryStream = new MemoryStream();

            captureImage.Bitmap.Save((Stream)memoryStream, imageFormat);
            

            return this.JsonResponse(ResponseStatus.Success, (object)Convert.ToBase64String(memoryStream.ToArray()));
        }


    }
}
