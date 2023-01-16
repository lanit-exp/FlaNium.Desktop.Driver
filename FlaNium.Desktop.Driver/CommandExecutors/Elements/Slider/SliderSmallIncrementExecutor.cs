using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Slider {

    class SliderSmallIncrementExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            global::FlaUI.Core.AutomationElements.Slider slider = element.FlaUiElement.AsSlider();

            slider.SetForeground();

            slider.SmallIncrement();

            return this.JsonResponse();
        }

    }

}