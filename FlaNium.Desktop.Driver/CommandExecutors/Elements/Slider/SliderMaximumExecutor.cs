using FlaNium.Desktop.Driver.Common;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Slider {

    class SliderMaximumExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            global::FlaUI.Core.AutomationElements.Slider slider = element.FlaUiElement.AsSlider();

            double value = slider.Maximum;

            return this.JsonResponse(ResponseStatus.Success, value);
        }

    }

}