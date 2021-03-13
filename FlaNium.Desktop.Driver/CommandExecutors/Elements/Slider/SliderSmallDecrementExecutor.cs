
namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Slider
{
    using global::FlaUI.Core.AutomationElements;
    class SliderSmallDecrementExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            Slider slider = element.FlaUIElement.AsSlider();

            slider.SetForeground();

            slider.SmallDecrement();

            return this.JsonResponse();
        }

        #endregion
    }
}
