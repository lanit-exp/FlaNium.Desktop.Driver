namespace FlaNium.Desktop.Driver.CommandExecutors
{

    #region using
    
    using System;
    using System.Drawing.Imaging;
    using System.IO;
    using global::FlaUI.Core.Capturing;
    using FlaNium.Desktop.Driver.Common;

    #endregion

    internal class ScreenshotExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {           
            CaptureImage captureImage = Capture.Screen();
            MemoryStream memoryStream = new MemoryStream();

            captureImage.Bitmap.Save((Stream)memoryStream, ImageFormat.Jpeg);

            return this.JsonResponse(ResponseStatus.Success, (object)Convert.ToBase64String(memoryStream.ToArray()));
        }

        #endregion
    }
}
