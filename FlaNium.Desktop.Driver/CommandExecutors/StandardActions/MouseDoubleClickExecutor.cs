using System.Threading;
using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;

namespace FlaNium.Desktop.Driver.CommandExecutors.StandardActions {

    internal class MouseDoubleClickExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            Mouse.Down(MouseButton.Left);
            Mouse.Up(MouseButton.Left);

            Thread.Sleep((int)User32.GetDoubleClickTime() / 2);

            Mouse.Down(MouseButton.Left);
            Mouse.Up(MouseButton.Left);

            return this.JsonResponse();
        }

    }

}