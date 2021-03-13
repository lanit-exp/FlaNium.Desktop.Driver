
using FlaNium.Desktop.Driver.FlaUI;
using FlaUI.Core.Definitions;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Window
{
    class MaximizeWindowExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            DriverManager.GetActiveWindow().Patterns.Window.Pattern.SetWindowVisualState(WindowVisualState.Maximized);

            return this.JsonResponse();
        }

        #endregion
    }
}
