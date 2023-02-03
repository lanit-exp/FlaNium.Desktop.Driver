using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using FlaNium.Desktop.Driver.Input;
using FlaUI.Core.Input;

namespace FlaNium.Desktop.Driver.CommandExecutors.Actions {

    internal static class DefaultMouseAction {

        internal static void PerformAction(Action action, Automator.Automator automator) {
            List<Action.ActionStep> steps = action.Actions;

            foreach (var step in steps) {
                switch (step.Type) {
                    case "pointerDown":
                        PointerDown(step);

                        break;
                    case "pointerUp":
                        PointerUp(step);

                        break;
                    case "pointerMove":
                        PointerMove(step, automator);

                        break;
                    case "pause":
                        Pause(step);

                        break;
                    default:
                        throw new NotSupportedException(
                            $"Action command '{step.Type}' not implemented. (Report the information to the driver developer.)");
                }
            }
        }

        private static void PointerMove(Action.ActionStep step, Automator.Automator automator) {
            if (step.Origin.HasValues){
                var registeredKey = step.Origin["ELEMENT"]?.ToString();
                
                var element = automator.ElementsRegistry.GetRegisteredElement(registeredKey);
                
                var rect = element.Properties.BoundingRectangle;
                
                CustomInput.MouseMove(new Point(rect.X + rect.Width/2, rect.Y + rect.Height/2) ,step.X, step.Y, step.Duration);
            }
            else {
                CustomInput.MouseMove(step.X, step.Y, step.Duration);
            }
        }

        private static void PointerUp(Action.ActionStep step) {
            Mouse.Up(GetMouseButton(step.Button));
        }

        private static void PointerDown(Action.ActionStep step) {
            Mouse.Down(GetMouseButton(step.Button));
        }


        private static void Pause(Action.ActionStep step) {
            if (step.Duration > 0) {
                Thread.Sleep(step.Duration);
            }
        }


        private static MouseButton GetMouseButton(int button) {
            switch (button) {
                case 0: return MouseButton.Left;
                case 1: return MouseButton.Middle;
                case 2: return MouseButton.Right;
                case 3: return MouseButton.XButton1;
                case 4: return MouseButton.XButton2;
                default: throw new NotSupportedException($"Mouse button '{button}' not implemented.");
            }
        }

    }

}