namespace FlaNium.Desktop.Driver.CommandExecutors.StandardActions {

    internal class ClickElementExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();
            this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey).Click();

            return this.JsonResponse();
        }

    }

}