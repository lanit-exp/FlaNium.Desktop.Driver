
namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Slider
{
    using System;
    using System.Globalization;
    using global::FlaUI.Core.AutomationElements;

    class SliderSetValueExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var value = this.ExecutedCommand.Parameters["value"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            Slider slider = element.FlaUIElement.AsSlider();

            slider.Value = Convert.ToDouble(value, CultureInfo.InvariantCulture);

            return this.JsonResponse();
        }

        #endregion
    }
}
