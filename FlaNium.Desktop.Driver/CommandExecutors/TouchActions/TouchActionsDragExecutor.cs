﻿using System;
using System.Collections.Generic;
using System.Drawing;
using FlaNium.Desktop.Driver.Input;

namespace FlaNium.Desktop.Driver.CommandExecutors.TouchActions {

    internal class TouchActionsDragExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var startEndPoints = this.ExecutedCommand.Parameters["startEndPoints"]
                .ToObject<List<Dictionary<String, Object>>>();

            var duration = Convert.ToInt32(this.ExecutedCommand.Parameters["duration"]);
            var durationHold = Convert.ToInt32(this.ExecutedCommand.Parameters["durationHold"]);

            List<Tuple<Point, Point>> pointsList = new List<Tuple<Point, Point>>();

            startEndPoints.ForEach(p => {
                p.TryGetValue("x1", out object x1);
                p.TryGetValue("y1", out object y1);

                p.TryGetValue("x2", out object x2);
                p.TryGetValue("y2", out object y2);

                Point p1 = new Point(Convert.ToInt32(x1), Convert.ToInt32(y1));
                Point p2 = new Point(Convert.ToInt32(x2), Convert.ToInt32(y2));

                pointsList.Add(Tuple.Create(p1, p2));
            });


            Touch.Drag(TimeSpan.FromMilliseconds(duration), pointsList.ToArray(),
                TimeSpan.FromMilliseconds(durationHold));

            return this.JsonResponse();
        }

    }

}