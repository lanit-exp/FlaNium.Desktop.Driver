using System;
using System.Collections.Generic;
using System.Threading;
using FlaNium.Desktop.Driver.Input;


namespace FlaNium.Desktop.Driver.CommandExecutors.Actions {

    internal static class DefaultKeyboardAction {

        internal static void PerformAction(Action action, Automator.Automator automator) {
            List<Action.ActionStep> steps = action.Actions;

            foreach (var step in steps) {
                switch (step.Type) {
                    case "keyDown":
                        KeyDown(step);

                        break;
                    case "keyUp":
                        KeyUp(step);

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


        private static void KeyDown(Action.ActionStep step) {
            CustomInput.KeyboardKeyDown(step.Value);
        }

        private static void KeyUp(Action.ActionStep step) {
            CustomInput.KeyboardKeyUp(step.Value);
        }

        private static void Pause(Action.ActionStep step) {
            if (step.Duration > 0) {
                Thread.Sleep(step.Duration);
            }
        }

    }

}