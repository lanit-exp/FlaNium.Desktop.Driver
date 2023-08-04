using FlaNium.Desktop.Driver.Common;

namespace FlaNium.Desktop.Driver.CommandExecutors {

    internal class QuitExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            if (!this.Automator.ActualCapabilities.ConnectToRunningApp) {
                this.Automator.ElementsRegistry.Clear();
            }

            return this.JsonResponse();
        }

    }

}