namespace FlaNium.Desktop.Driver.CommandExecutors {

    internal class QuitExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            if (!this.Automator.ActualCapabilities.DebugConnectToRunningApp) {
                //TODO нужен ли ?
                this.Automator.ElementsRegistry.Clear();
            }

            return this.JsonResponse();
        }

    }

}