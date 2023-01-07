using System;
using System.Drawing;

namespace FlaNium.Desktop.Driver.Input
{
    /// <summary>
    /// Mouse class to simulate Mouse actions.
    /// </summary>
    internal class Mouse
    {
        /// <summary>
        /// Transitions all the points from the start point to the end points.
        /// </summary>
        /// <param name="duration">The duration for the action.</param>
        /// <param name="startEndPoints">The list of start/end point tuples.</param>
        public static void Transition(TimeSpan duration, params Tuple<Point, Point>[] startEndPoints)
        {
            Interpolation.Execute(points =>
            {
                global::FlaUI.Core.Input.Mouse.Position = new Point(points[0].X, points[0].Y);
            },
            startEndPoints, duration, Touch.DefaultInterval, true);
            global::FlaUI.Core.Input.Wait.UntilInputIsProcessed();
        }
    }
}
