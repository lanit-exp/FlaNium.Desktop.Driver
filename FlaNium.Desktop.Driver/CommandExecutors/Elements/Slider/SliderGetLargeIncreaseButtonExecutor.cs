using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.Exceptions;
using FlaNium.Desktop.Driver.FlaUI;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Slider {

    class SliderGetLargeIncreaseButtonExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            global::FlaUI.Core.AutomationElements.Slider slider = element.FlaUiElement.AsSlider();

            global::FlaUI.Core.AutomationElements.Button button = slider.LargeIncreaseButton;

            if (button == null) {
                throw new AutomationException("Element cannot be found", ResponseStatus.NoSuchElement);
            }

            var itemRegisteredKey = this.Automator.ElementsRegistry.RegisterElement(new FlaUiDriverElement(button));

            var registeredObject = new JsonElementContent(itemRegisteredKey);

            return this.JsonResponse(ResponseStatus.Success, registeredObject);
        }

    }

}