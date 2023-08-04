using System;
using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.FlaUI;
using FlaUI.Core.Input;

namespace FlaNium.Desktop.Driver.CommandExecutors.StandardActions {

    internal class MouseMoveToExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            var haveElement = this.ExecutedCommand.Parameters.ContainsKey("element");
            var haveOffset = this.ExecutedCommand.Parameters.ContainsKey("xoffset")
                             && this.ExecutedCommand.Parameters.ContainsKey("yoffset");

            if (!(haveElement || haveOffset)) {
                return this.JsonResponse(ResponseStatus.UnknownError, "WRONG PARAMETERS");
            }

            var resultPoint = Mouse.Position;
            if (haveElement) {
                var registeredKey = this.ExecutedCommand.Parameters["element"].ToString();
                FlaUiDriverElement element = this.Automator.ElementsRegistry.GetRegisteredElementOrNull(registeredKey);

                if (element != null) {
                    var rect = element.Properties.BoundingRectangle;
                    resultPoint.X = rect.Left;
                    resultPoint.Y = rect.Top;
                    if (!haveOffset) {
                        resultPoint.X += rect.Width / 2;
                        resultPoint.Y += rect.Height / 2;
                    }
                }
            }

            if (haveOffset) {
                resultPoint.X += Convert.ToInt32(this.ExecutedCommand.Parameters["xoffset"]);
                resultPoint.Y += Convert.ToInt32(this.ExecutedCommand.Parameters["yoffset"]);
            }

            Mouse.MoveTo(resultPoint.X, resultPoint.Y);

            return this.JsonResponse();
        }

    }

}