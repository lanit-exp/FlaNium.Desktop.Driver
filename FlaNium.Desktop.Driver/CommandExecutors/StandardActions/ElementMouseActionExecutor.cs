using System;
using System.Drawing;
using FlaUI.Core.Input;

namespace FlaNium.Desktop.Driver.CommandExecutors.StandardActions {

    class ElementMouseActionExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var elementId = this.ExecutedCommand.Parameters["ID"].ToString();

            var x = Convert.ToInt32(this.ExecutedCommand.Parameters["x"]);
            var y = Convert.ToInt32(this.ExecutedCommand.Parameters["y"]);

            var basePoint = this.ExecutedCommand.Parameters["basePoint"].ToString();
            var action = this.ExecutedCommand.Parameters["action"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(elementId);

            var elX = element.GetRect().X;
            var elY = element.GetRect().Y;
            var elW = element.GetRect().Width;
            var elH = element.GetRect().Height;

            switch (basePoint) {
                case "TOP_LEFT": {
                    x = x + elX;
                    y = y + elY;
                }

                    break;
                case "TOP_RIGHT": {
                    x = x + elX + elW;
                    y = y + elY;
                }

                    break;
                case "BOTTOM_LEFT": {
                    x = x + elX;
                    y = y + elY + elH;
                }

                    break;
                case "BOTTOM_RIGHT": {
                    x = x + elX + elW;
                    y = y + elY + elH;
                }

                    break;
            }

            switch (basePoint) {
                case "CENTER": {
                    x = x + elX + elW / 2;
                    y = y + elY + elH / 2;
                }

                    break;
                case "CENTER_TOP": {
                    x = x + elX + elW / 2;
                    y = y + elY;
                }

                    break;
                case "CENTER_BOTTOM": {
                    x = x + elX + elW / 2;
                    y = y + elY + elH;
                }

                    break;
                case "CENTER_LEFT": {
                    x = x + elX;
                    y = y + elY + elH / 2;
                }

                    break;
                case "CENTER_RIGHT": {
                    x = x + elX + elW;
                    y = y + elY + elH / 2;
                }

                    break;
            }

            switch (action) {
                case "move":
                    Mouse.MoveTo(x, y);

                    break;
                case "click":
                    Mouse.LeftClick(new Point(x, y));

                    break;
                case "rightClick":
                    Mouse.RightClick(new Point(x, y));

                    break;
                case "doubleClick":
                    Mouse.DoubleClick(new Point(x, y));

                    break;
                default:
                    return this.JsonResponse(Common.ResponseStatus.UnknownCommand, "Uknown action value: " + action);
            }


            return this.JsonResponse();
        }

    }

}