using FlaNium.Desktop.Driver.FlaUI;

namespace FlaNium.Desktop.Driver.CommandExecutors {

    internal class CloseExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            if (!this.Automator.ActualCapabilities.ConnectToRunningApp) {
                DriverManager.CloseAppSession(this.Automator.ActualCapabilities.App.StartsWith("#"));

                this.Automator.ElementsRegistry.Clear();
            }

            return this.JsonResponse();
        }

    }

}