using System;
using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.FlaUI;
using FlaUI.Core.Input;

namespace FlaNium.Desktop.Driver.CommandExecutors.StandardActions {

    internal class MouseClickExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var buttonId = Convert.ToInt32(this.ExecutedCommand.Parameters["button"]);

            DriverManager.GetActiveWindow().SetForeground();

            switch ((MouseButton)buttonId) {
                case MouseButton.Left:
                    Mouse.LeftClick();

                    break;

                case MouseButton.Right:
                    Mouse.RightClick();

                    break;

                default:
                    return this.JsonResponse(ResponseStatus.UnknownCommand, "Mouse button behavior is not implemented");
            }

            return this.JsonResponse();
        }

    }

}