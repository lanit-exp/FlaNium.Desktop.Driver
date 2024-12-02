using System;
using System.Drawing;
using FlaUI.Core.Input;

namespace FlaNium.Desktop.Driver.Input {

    public static class CustomInput {

        // Mouse =======================================================================================================

        public static void MouseMove(Point startPoint, int dx, int dy, int duration) {
            Point endPoint = new Point(startPoint.X + dx, startPoint.Y + dy);
            MouseMove(startPoint, endPoint, duration, false);
        }
        
        public static void MouseMove(Point startPoint, Point endPoint, int duration) {
            MouseMove(startPoint, endPoint, duration, false);
        }
        
        public static void MouseMove(Point endPoint, int duration) {
            Point startPoint = Mouse.Position;
            MouseMove(startPoint, endPoint, duration, true);
        }

        public static void MouseMove(int dx, int dy, int duration) {
            Point startPoint = Mouse.Position;
            Point endPoint = new Point(startPoint.X + dx, startPoint.Y + dy);

            MouseMove(startPoint, endPoint, duration, true);
        }
        
        //==============================================================================================================
        
        private static void MouseMove(Point startPoint, Point endPoint, int duration, bool skipInitialPosition) {
            
            Interpolation.Execute(point => { Mouse.Position = new Point(point.X, point.Y); },
                startPoint,
                endPoint,
                TimeSpan.FromMilliseconds(duration),
                Touch.DefaultInterval,
                skipInitialPosition);
        }

    }

}