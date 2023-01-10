namespace FlaNium.Desktop.Driver.CommandExecutors
{
    using global::FlaUI.Core.Input;
    using System;
    using System.Drawing;

    class ElementDragAndDropExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var elementId = this.ExecutedCommand.Parameters["ID"].ToString();

            var x = Convert.ToInt32(this.ExecutedCommand.Parameters["x"]);
            var y = Convert.ToInt32(this.ExecutedCommand.Parameters["y"]);
            var dx = Convert.ToInt32(this.ExecutedCommand.Parameters["dx"]);
            var dy = Convert.ToInt32(this.ExecutedCommand.Parameters["dy"]);

            var duration = Convert.ToInt32(this.ExecutedCommand.Parameters["duration"]);

            var basePoint = this.ExecutedCommand.Parameters["basePoint"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(elementId);

            var elX = element.GetRect().X;
            var elY = element.GetRect().Y;
            var elW = element.GetRect().Width;
            var elH = element.GetRect().Height;

            switch (basePoint)
            {
                case "TOP_LEFT":
                {
                    x = x + elX;
                    y = y + elY;
                }
                    break;
                case "TOP_RIGHT":
                {
                    x = x + elX + elW;
                    y = y + elY;
                }
                    break;
                case "BOTTOM_LEFT":
                {
                    x = x + elX;
                    y = y + elY + elH;
                }
                    break;
                case "BOTTOM_RIGHT":
                {
                    x = x + elX + elW;
                    y = y + elY + elH;
                }
                    break;
            }

            switch (basePoint)
            {
                case "CENTER":
                {
                    x = x + elX + elW / 2;
                    y = y + elY + elH / 2;
                }
                    break;
                case "CENTER_TOP":
                {
                    x = x + elX + elW / 2;
                    y = y + elY;
                }
                    break;
                case "CENTER_BOTTOM":
                {
                    x = x + elX + elW / 2;
                    y = y + elY + elH;
                }
                    break;
                case "CENTER_LEFT":
                {
                    x = x + elX;
                    y = y + elY + elH / 2;
                }
                    break;
                case "CENTER_RIGHT":
                {
                    x = x + elX + elW;
                    y = y + elY + elH / 2;
                }
                    break;
            }


            if (duration > 0)
            {
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
            else
            {
                Mouse.Drag(new Point(x, y), dx, dy, MouseButton.Left);
            }

            Wait.UntilInputIsProcessed();


            return this.JsonResponse();
        }
    }
}