namespace FlaNium.Desktop.Driver.CommandExecutors.StandardActions {

    internal class ClearElementExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);
            element.Clear();

            return this.JsonResponse();
        }

    }

}