
namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.ComboBox
{
    using global::FlaUI.Core.AutomationElements;
    using FlaNium.Desktop.Driver.FlaUI;
    using FlaNium.Desktop.Driver.Common;
    using FlaNium.Desktop.Driver.Exceptions;

    class ComboBoxSelectedItemExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            ComboBox comboBox = element.FlaUIElement.AsComboBox();

            ComboBoxItem item = comboBox.SelectedItem;

            if (item == null)
            {
                throw new AutomationException("Element cannot be found", ResponseStatus.NoSuchElement);
            }

            var itemRegisteredKey = this.Automator.ElementsRegistry.RegisterElement(new FlaUIDriverElement(item));
            
            var registeredObject = new JsonElementContent(itemRegisteredKey);
            
            return this.JsonResponse(ResponseStatus.Success, registeredObject);
        }

        #endregion
    }
}
