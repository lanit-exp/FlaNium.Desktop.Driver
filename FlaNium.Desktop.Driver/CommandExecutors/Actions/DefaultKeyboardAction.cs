using System;
using System.Collections.Generic;
using System.Threading;
using FlaNium.Desktop.Driver.Input;


namespace FlaNium.Desktop.Driver.CommandExecutors.Actions {

    public static class DefaultKeyboardAction {

        public static void PerformAction(Action action) {
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
            CustomInput.KeyboardKeyDown(KeyboardModifiers.GetVirtualKeyOrChar(step.Value));
        }

        private static void KeyUp(Action.ActionStep step) {
            CustomInput.KeyboardKeyUp(KeyboardModifiers.GetVirtualKeyOrChar(step.Value));
        }

        private static void Pause(Action.ActionStep step) {
            if (step.Duration > 0) {
                Thread.Sleep(step.Duration);
            }
        }

    }

}