using FlaNium.Desktop.Driver.Common;
using WindowsInput.Native;

namespace FlaNium.Desktop.Driver.CommandExecutors.StandardActions {

    class KeyCombinationExecutor : CommandExecutorBase {

        protected override JsonResponse DoImpl() {
            var keyCombination = this.ExecutedCommand.Parameters["value"].ToString();


            switch (keyCombination) {
                case "CTRL_A": {
                    this.Automator.InputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LCONTROL, VirtualKeyCode.VK_A);
                }

                    break;

                case "CTRL_C": {
                    this.Automator.InputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LCONTROL, VirtualKeyCode.VK_C);
                }

                    break;

                case "CTRL_V": {
                    this.Automator.InputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LCONTROL, VirtualKeyCode.VK_V);
                }

                    break;

                case "CTRL_X": {
                    this.Automator.InputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LCONTROL, VirtualKeyCode.VK_X);
                }

                    break;

                case "CTRL_A_DELETE": {
                    this.Automator.InputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LCONTROL, VirtualKeyCode.VK_A);
                    this.Automator.InputSimulator.Keyboard.KeyPress(VirtualKeyCode.DELETE);
                }

                    break;

                case "CTRL_A_BACKSPACE": {
                    this.Automator.InputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LCONTROL, VirtualKeyCode.VK_A);
                    this.Automator.InputSimulator.Keyboard.KeyPress(VirtualKeyCode.BACK);
                }

                    break;

                default:
                    return this.JsonResponse(Common.ResponseStatus.UnknownCommand,
                        "Unknown key combination: " + keyCombination);
            }


            return this.JsonResponse();
        }

    }

}