namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.ComboBox {

    using global::FlaUI.Core.AutomationElements;

    class ComboBoxCollapseExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            ComboBox comboBox = element.FlaUiElement.AsComboBox();

            comboBox.Collapse();

            return this.JsonResponse();
        }

    }

}