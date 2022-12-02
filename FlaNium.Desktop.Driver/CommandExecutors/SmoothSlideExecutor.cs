namespace FlaNium.Desktop.Driver.CommandExecutors
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using global::FlaUI.Core.Input;

    internal class SmoothSlideExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            List<Dictionary<String, Object>> startEndPoints = this.ExecutedCommand.Parameters["startEndPoints"].ToObject<List<Dictionary<String, Object>>>();
            var duration = Convert.ToInt32(this.ExecutedCommand.Parameters["duration"]);

            List<Tuple<Point, Point>> pointsList = new List<Tuple<Point, Point>>();

            startEndPoints.ForEach(p =>
            {
                p.TryGetValue("x1", out object x1);
                p.TryGetValue("y1", out object y1);

                p.TryGetValue("x2", out object x2);
                p.TryGetValue("y2", out object y2);

                Point p1 = new Point(Convert.ToInt32(x1), Convert.ToInt32(y1));
                Point p2 = new Point(Convert.ToInt32(x2), Convert.ToInt32(y2));

                pointsList.Add(Tuple.Create(p1, p2));
            });


            Mouse.Position = new Point(pointsList[0].Item1.X, pointsList[0].Item1.Y);
            Mouse.Down();
            Input.Mouse.Transition(TimeSpan.FromMilliseconds(duration), pointsList.ToArray());
            Mouse.Up();
            
            return this.JsonResponse();
        }
    }
}
