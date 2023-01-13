using System;
using System.Drawing;
using FlaUI.Core.Input;

namespace FlaNium.Desktop.Driver.CommandExecutors.StandardActions {

    class DragAndDropExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var x = Convert.ToInt32(this.ExecutedCommand.Parameters["x"]);
            var y = Convert.ToInt32(this.ExecutedCommand.Parameters["y"]);
            var dx = Convert.ToInt32(this.ExecutedCommand.Parameters["dx"]);
            var dy = Convert.ToInt32(this.ExecutedCommand.Parameters["dy"]);

            var duration = Convert.ToInt32(this.ExecutedCommand.Parameters["duration"]);


            if (duration > 0) {
                Mouse.Position = new Point(x, y);
                Mouse.Down();

                Interpolation.Execute(point => { Mouse.Position = new Point(point.X, point.Y); },
                    new Point(x, y),
                    new Point(x + dx, y + dy),
                    TimeSpan.FromMilliseconds(duration),
                    Touch.DefaultInterval,
                    true);

                Mouse.Up();
            }
            else {
                Mouse.Drag(new Point(x, y), dx, dy, MouseButton.Left);
            }

            Wait.UntilInputIsProcessed();

            return this.JsonResponse();
        }

    }

}