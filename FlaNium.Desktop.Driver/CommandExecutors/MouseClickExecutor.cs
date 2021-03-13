namespace FlaNium.Desktop.Driver.CommandExecutors
{
    #region using

    using System;
    using global::FlaUI.Core.Input;
    using FlaNium.Desktop.Driver.FlaUI;
    using FlaNium.Desktop.Driver.Common;

    #endregion

    internal class MouseClickExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var buttonId = Convert.ToInt32(this.ExecutedCommand.Parameters["button"]);

            DriverManager.GetActiveWindow().SetForeground();

            switch ((MouseButton)buttonId)
            {
                case MouseButton.Left:
                    Mouse.LeftClick();
                    break;

                case MouseButton.Right:
                    Mouse.RightClick();
                    break;

                default:
                    return this.JsonResponse(ResponseStatus.UnknownCommand, "Mouse button behavior is not implemented");
            }

            return this.JsonResponse();

        }

        #endregion
    }
}
