﻿using System;
using System.Drawing.Imaging;
using System.IO;
using FlaNium.Desktop.Driver.CommandHelpers;
using FlaNium.Desktop.Driver.Common;
using FlaUI.Core.Capturing;

namespace FlaNium.Desktop.Driver.CommandExecutors.Auxiliary {

    class CustomScreenshotExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            var imageFormatStr = this.ExecutedCommand.Parameters["format"].ToString();


            ImageFormat imageFormat = ImFormat.GetImageFormat(imageFormatStr);

            CaptureImage captureImage = CaptureActions.Screen();
            MemoryStream memoryStream = new MemoryStream();

            captureImage.Bitmap.Save(memoryStream, imageFormat);


            return this.JsonResponse(ResponseStatus.Success, Convert.ToBase64String(memoryStream.ToArray()));
        }

    }

}