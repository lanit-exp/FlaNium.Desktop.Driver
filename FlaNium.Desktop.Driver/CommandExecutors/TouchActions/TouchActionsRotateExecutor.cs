
namespace FlaNium.Desktop.Driver.CommandExecutors.TouchActions
{
    using FlaNium.Desktop.Driver.Input;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    internal class TouchActionsRotateExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var center = this.ExecutedCommand.Parameters["center"].ToObject<Dictionary<String, Object>>();
            var radius = Convert.ToDouble(this.ExecutedCommand.Parameters["radius"]);
            var startAngle = Convert.ToDouble(this.ExecutedCommand.Parameters["startAngle"]);
            var endAngle = Convert.ToDouble(this.ExecutedCommand.Parameters["endAngle"]);
            var duration = Convert.ToInt32(this.ExecutedCommand.Parameters["duration"]);
            

            center.TryGetValue("x", out object x);
            center.TryGetValue("y", out object y);

            Point centerPoint = new Point(Convert.ToInt32(x), Convert.ToInt32(y));


            Touch.Rotate(centerPoint, radius, startAngle, endAngle, TimeSpan.FromMilliseconds(duration));

            return this.JsonResponse();
        }
    }
}
