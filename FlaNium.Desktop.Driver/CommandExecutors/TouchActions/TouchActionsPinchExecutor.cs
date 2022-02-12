
namespace FlaNium.Desktop.Driver.CommandExecutors.TouchActions
{
    using FlaNium.Desktop.Driver.Input;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    internal class TouchActionsPinchExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var center = this.ExecutedCommand.Parameters["center"].ToObject<Dictionary<String, Object>>();
            var startRadius = Convert.ToDouble(this.ExecutedCommand.Parameters["startRadius"]);
            var endRadius = Convert.ToDouble(this.ExecutedCommand.Parameters["endRadius"]);
            var duration = Convert.ToInt32(this.ExecutedCommand.Parameters["duration"]);
            var angle = Convert.ToDouble(this.ExecutedCommand.Parameters["angle"]);


            center.TryGetValue("x", out object x);
            center.TryGetValue("y", out object y);

            Point centerPoint = new Point(Convert.ToInt32(x), Convert.ToInt32(y));
           

            Touch.Pinch(centerPoint, startRadius, endRadius, TimeSpan.FromMilliseconds(duration), angle);

            return this.JsonResponse();
        }
    }
}
