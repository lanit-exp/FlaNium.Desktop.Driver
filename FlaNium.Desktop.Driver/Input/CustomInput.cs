using System;
using System.Drawing;
using FlaUI.Core.Input;

namespace FlaNium.Desktop.Driver.Input {

    public static class CustomInput {

        // Mouse =======================================================================================================

        public static void MouseMove(Point startPoint, int dx, int dy, int duration) {
            Interpolation.Execute(point => { Mouse.Position = new Point(point.X, point.Y); },
                startPoint,
                new Point(startPoint.X + dx, startPoint.Y + dy),
                TimeSpan.FromMilliseconds(duration),
                Touch.DefaultInterval,
                false);
        }

        public static void MouseMove(int dx, int dy, int duration) {
            Point startPoint = Mouse.Position;

            Interpolation.Execute(point => { Mouse.Position = new Point(point.X, point.Y); },
                startPoint,
                new Point(startPoint.X + dx, startPoint.Y + dy),
                TimeSpan.FromMilliseconds(duration),
                Touch.DefaultInterval,
                true);
        }

    }

}