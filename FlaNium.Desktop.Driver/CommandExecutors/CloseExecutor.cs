using FlaNium.Desktop.Driver.FlaUI;

namespace FlaNium.Desktop.Driver.CommandExecutors {

    internal class CloseExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            DriverManager.CloseAppSession(!this.Automator.ActualCapabilities.ConnectToRunningApp);
            this.Automator.ElementsRegistry.Clear();

            return this.JsonResponse();
        }

    }

}