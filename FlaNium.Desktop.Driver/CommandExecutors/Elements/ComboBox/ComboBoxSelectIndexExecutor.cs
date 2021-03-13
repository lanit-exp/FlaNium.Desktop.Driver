
namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.ComboBox
{
    using System.Linq;
    using global::FlaUI.Core.AutomationElements;
    using FlaNium.Desktop.Driver.FlaUI;
    using FlaNium.Desktop.Driver.Common;
    using FlaNium.Desktop.Driver.Exceptions;

    class ComboBoxSelectIndexExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var value = this.ExecutedCommand.Parameters["value"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            ComboBox comboBox = element.FlaUIElement.AsComboBox();
            
            ComboBoxItem item;
            
            if (comboBox.Patterns.Selection.IsSupported)
            {
                item = comboBox.Select(int.Parse(value));
                comboBox.Collapse();
            }
            else {
                item = comboBox.Items.ElementAt(int.Parse(value));
                item.Click();
            }

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
