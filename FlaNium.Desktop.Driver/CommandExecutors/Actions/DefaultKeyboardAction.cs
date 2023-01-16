using System;
using System.Collections.Generic;
using System.Threading;
using FlaNium.Desktop.Driver.Input;
using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;

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
            var key = KeyboardModifiers.GetVirtualKeyShort(step.Value);
            if (key == 0) {
                short num = User32.VkKeyScan(step.Value[0]);
                Keyboard.Press((VirtualKeyShort)num);
            }
            else {
                Keyboard.Press(key);
            }
        }

        private static void KeyUp(Action.ActionStep step) {
            var key = KeyboardModifiers.GetVirtualKeyShort(step.Value);
            if (key == 0) {
                short num = User32.VkKeyScan(step.Value[0]);
                Keyboard.Release((VirtualKeyShort)num);
            }
            else {
                Keyboard.Release(key);
            }
        }

        private static void Pause(Action.ActionStep step) {
            if (step.Duration > 0) {
                Thread.Sleep(step.Duration);
            }
        }

    }

}