namespace FlaNium.Desktop.Driver.CommandExecutors {

    internal class QuitExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            if (!this.Automator.ActualCapabilities.ConnectToRunningApp) {
                this.Automator.ElementsRegistry.Clear();
            }

            return this.JsonResponse();
        }

    }

}