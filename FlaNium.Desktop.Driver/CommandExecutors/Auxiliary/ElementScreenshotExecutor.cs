﻿using System;
using System.Drawing.Imaging;
using System.IO;
using FlaNium.Desktop.Driver.CommandHelpers;
using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.FlaUI;
using FlaUI.Core.Capturing;

namespace FlaNium.Desktop.Driver.CommandExecutors.Auxiliary {

    internal class ElementScreenshotExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            var elementId = this.ExecutedCommand.Parameters["ID"].ToString();
            var imageFormatStr = this.ExecutedCommand.Parameters["format"]?.ToString() ?? "PNG";
            var foreground = Boolean.Parse(this.ExecutedCommand.Parameters["foreground"]?.ToString() ?? "true");

            FlaUiDriverElement element = this.Automator.ElementsRegistry.GetRegisteredElement(elementId);

            ImageFormat imageFormat = ImFormat.GetImageFormat(imageFormatStr);

            MemoryStream memoryStream = new MemoryStream();
            CaptureImage captureImage;
            
            if (foreground) {
                captureImage = CaptureActions.Element(element.FlaUiElement, WindowsAPIHelpers.GetScale(element.FlaUiElement.Properties.NativeWindowHandle));
            }
            else {
                captureImage = CaptureActions.CaptureImageOfElement(element.FlaUiElement);
            }

            captureImage.Bitmap.Save(memoryStream, imageFormat);

            return this.JsonResponse(ResponseStatus.Success, Convert.ToBase64String(memoryStream.ToArray()));
        }

    }

}