using System;
using System.Collections.Generic;
using System.Threading;
using FlaUI.Core.Input;

namespace FlaNium.Desktop.Driver.CommandExecutors.Actions {

    // TODO Доработать и дополнить функционал
    internal static class DefaultWheelAction {

        internal static void PerformAction(Action action, Automator.Automator automator) {
            List<Action.ActionStep> steps = action.Actions;

            foreach (var step in steps) {
                switch (step.Type) {
                    case "scroll":
                        Scroll(step, automator);

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

        private static void Scroll(Action.ActionStep step, Automator.Automator automator) {
            if (step.Origin.HasValues) {
                throw new NotSupportedException(
                    $"Action command '{step.Type}' not implemented. (Report the information to the driver developer.)");
            }

            if (step.DeltaX != 0) Mouse.HorizontalScroll(step.DeltaX);
            if (step.DeltaY != 0) Mouse.Scroll(step.DeltaY);

            Pause(step);
        }

        private static void Pause(Action.ActionStep step) {
            if (step.Duration > 0) {
                Thread.Sleep(step.Duration);
            }
        }

    }

}