
namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.DataGridView
{
    using global::FlaUI.Core.AutomationElements;
    class DataGridViewCellSetValueExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();
            
            var value = this.ExecutedCommand.Parameters["value"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            DataGridViewCell item = (DataGridViewCell)element.FlaUIElement;

            item.Value = value;

            return this.JsonResponse();
        }

        #endregion
    }
}
