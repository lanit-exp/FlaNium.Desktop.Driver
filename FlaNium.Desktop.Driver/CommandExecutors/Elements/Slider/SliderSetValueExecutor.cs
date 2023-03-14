using System;
using System.Globalization;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Slider {

    class SliderSetValueExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var value = this.ExecutedCommand.Parameters["value"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            global::FlaUI.Core.AutomationElements.Slider slider = element.FlaUiElement.AsSlider();

            slider.Value = Convert.ToDouble(value, CultureInfo.InvariantCulture);

            return this.JsonResponse();
        }

    }

}