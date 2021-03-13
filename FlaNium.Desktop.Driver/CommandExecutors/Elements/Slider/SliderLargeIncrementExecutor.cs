
namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Slider
{
    using global::FlaUI.Core.AutomationElements;
    class SliderLargeIncrementExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            Slider slider = element.FlaUIElement.AsSlider();

            slider.LargeIncrement();

            return this.JsonResponse();
        }

        #endregion
    }
}
