using System;
using System.Collections.Generic;
using System.Threading;

namespace FlaNium.Desktop.Driver.CommandExecutors.Actions {

    public static class DefaultMouseAction {

        public static void PerformAction(Action action) {
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
                        PointerMove(step);

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

        private static void PointerMove(Action.ActionStep step) {
            throw new NotImplementedException();
        }

        private static void PointerUp(Action.ActionStep step) {
            throw new NotImplementedException();
        }

        private static void PointerDown(Action.ActionStep step) {
            throw new NotImplementedException();
        }


        private static void Pause(Action.ActionStep step) {
            if (step.Duration > 0) {
                Thread.Sleep(step.Duration);
            }
        }

    }

}