
namespace FlaNium.Desktop.Driver.CommandExecutors.TouchActions
{
    using FlaNium.Desktop.Driver.Input;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    internal class TouchActionsTapExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var points = this.ExecutedCommand.Parameters["points"].ToObject<List<Dictionary<String, Object>>>();

            List<Point> pointsList = new List<Point>();

            points.ForEach(p =>
            {
                p.TryGetValue("x", out object x);
                p.TryGetValue("y", out object y);

                pointsList.Add(new Point(Convert.ToInt32(x), Convert.ToInt32(y)));
            });

            Touch.Tap(pointsList.ToArray());

            return this.JsonResponse();
        }
    }
}
