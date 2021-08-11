
namespace FlaNium.Desktop.Driver.CommandExecutors
{
    using global::FlaUI.Core.Input;
    using global::FlaUI.Core.WindowsAPI;

    class KeyCombinationExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var keyCombination = this.ExecutedCommand.Parameters["value"].ToString();


            switch (keyCombination) 
            {
                case "CTRL_A":
                    {
                        Keyboard.Press(VirtualKeyShort.LCONTROL);
                        Keyboard.Press(VirtualKeyShort.KEY_A);
                        Keyboard.Release(VirtualKeyShort.KEY_A);
                        Keyboard.Release(VirtualKeyShort.LCONTROL);
                    }
                    break;

                case "CTRL_C":
                    {
                        Keyboard.Press(VirtualKeyShort.LCONTROL);
                        Keyboard.Press(VirtualKeyShort.KEY_C);
                        Keyboard.Release(VirtualKeyShort.KEY_C);
                        Keyboard.Release(VirtualKeyShort.LCONTROL);
                    }
                    break;

                case "CTRL_V":
                    {
                        Keyboard.Press(VirtualKeyShort.LCONTROL);
                        Keyboard.Press(VirtualKeyShort.KEY_V);
                        Keyboard.Release(VirtualKeyShort.KEY_V);
                        Keyboard.Release(VirtualKeyShort.LCONTROL);
                    }
                    break;

                case "CTRL_X":
                    {
                        Keyboard.Press(VirtualKeyShort.LCONTROL);
                        Keyboard.Press(VirtualKeyShort.KEY_X);
                        Keyboard.Release(VirtualKeyShort.KEY_X);
                        Keyboard.Release(VirtualKeyShort.LCONTROL);
                    }
                    break;

                case "CTRL_A_DELETE":
                    {
                        Keyboard.Press(VirtualKeyShort.LCONTROL);
                        Keyboard.Press(VirtualKeyShort.KEY_A);
                        Keyboard.Release(VirtualKeyShort.KEY_A);
                        Keyboard.Release(VirtualKeyShort.LCONTROL);
                        Keyboard.Press(VirtualKeyShort.DELETE);
                        Keyboard.Release(VirtualKeyShort.DELETE);
                    }
                    break;

                case "CTRL_A_BACKSPACE":
                    {
                        Keyboard.Press(VirtualKeyShort.LCONTROL);
                        Keyboard.Press(VirtualKeyShort.KEY_A);
                        Keyboard.Release(VirtualKeyShort.KEY_A);
                        Keyboard.Release(VirtualKeyShort.LCONTROL);
                        Keyboard.Press(VirtualKeyShort.BACK);
                        Keyboard.Release(VirtualKeyShort.BACK);
                    }
                    break;

                default: return this.JsonResponse(Common.ResponseStatus.UnknownCommand, "Uknown key combination: " + keyCombination);
            }


            return this.JsonResponse();
        }

    }
}
