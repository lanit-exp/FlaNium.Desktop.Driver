
namespace FlaNium.Desktop.Driver.CommandExecutors
{
    
    #region using

    using System.Threading;
    using global::FlaUI.Core.Input;
    using global::FlaUI.Core.WindowsAPI;

    #endregion

    internal class MouseDoubleClickExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            
            Mouse.Down(MouseButton.Left);
            Mouse.Up(MouseButton.Left);

            Thread.Sleep((int)User32.GetDoubleClickTime() / 2);

            Mouse.Down(MouseButton.Left);
            Mouse.Up(MouseButton.Left);

            return this.JsonResponse();

        }

        #endregion
    }
}
