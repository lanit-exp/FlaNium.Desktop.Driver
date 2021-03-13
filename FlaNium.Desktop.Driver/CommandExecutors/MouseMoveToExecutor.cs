namespace FlaNium.Desktop.Driver.CommandExecutors
{
    #region using

    using System;
    using global::FlaUI.Core.Input;
    using FlaNium.Desktop.Driver.FlaUI;
    using FlaNium.Desktop.Driver.Common;

    #endregion

    internal class MouseMoveToExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var haveElement = this.ExecutedCommand.Parameters.ContainsKey("element");
            var haveOffset = this.ExecutedCommand.Parameters.ContainsKey("xoffset")
                             && this.ExecutedCommand.Parameters.ContainsKey("yoffset");

            if (!(haveElement || haveOffset))
            {
                // TODO: in the future '400 : invalid argument' will be used
                return this.JsonResponse(ResponseStatus.UnknownError, "WRONG PARAMETERS");
            }

            var resultPoint = Mouse.Position;
            if (haveElement)
            {
                var registeredKey = this.ExecutedCommand.Parameters["element"].ToString();
                FlaUIDriverElement element = this.Automator.ElementsRegistry.GetRegisteredElementOrNull(registeredKey);
                
                if (element != null)
                {
                    var rect = element.Properties.BoundingRectangle;
                    resultPoint.X = rect.Left;
                    resultPoint.Y = rect.Top;
                    if (!haveOffset)
                    {
                        resultPoint.X += rect.Width / 2;
                        resultPoint.Y += rect.Height / 2;
                    }
                }
            }

            if (haveOffset)
            {
                resultPoint.X += Convert.ToInt32(this.ExecutedCommand.Parameters["xoffset"]);
                resultPoint.Y += Convert.ToInt32(this.ExecutedCommand.Parameters["yoffset"]);
            }

            DriverManager.GetActiveWindow();

            Mouse.MoveTo(resultPoint.X, resultPoint.Y);

            return this.JsonResponse();
           
        }

        #endregion
    }
}
