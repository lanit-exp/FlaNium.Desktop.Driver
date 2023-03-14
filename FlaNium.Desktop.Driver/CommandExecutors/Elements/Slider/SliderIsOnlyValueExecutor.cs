using FlaNium.Desktop.Driver.Common;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Slider {

    class SliderIsOnlyValueExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            global::FlaUI.Core.AutomationElements.Slider slider = element.FlaUiElement.AsSlider();

            var result = slider.IsOnlyValue;

            return this.JsonResponse(ResponseStatus.Success, result);
        }

    }

}