using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.FlaUI;
using FlaUI.Core.Definitions;

namespace FlaNium.Desktop.Driver.CommandExecutors.StandardActions {

    class MaximizeWindowExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            DriverManager.GetRootElement().Patterns.Window.Pattern.SetWindowVisualState(WindowVisualState.Maximized);

            return this.JsonResponse();
        }

    }

}