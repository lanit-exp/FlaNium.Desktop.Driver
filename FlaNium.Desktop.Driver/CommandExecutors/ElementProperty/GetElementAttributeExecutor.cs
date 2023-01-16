using System;
using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.FlaUI;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;

namespace FlaNium.Desktop.Driver.CommandExecutors.ElementProperty {

    internal class GetElementAttributeExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();
            var propertyName = this.ExecutedCommand.Parameters["NAME"].ToString();

            FlaUiDriverElement element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            AutomationElement flaUiElement = element.FlaUiElement;


            if (propertyName == "ToggleState") {
                var toggleState = flaUiElement.Patterns.Toggle.PatternOrDefault.ToggleState;

                return this.JsonResponse(ResponseStatus.Success, toggleState.ToString());
            }


            try {
                var propertyObject = flaUiElement.Properties.GetType().GetProperty(propertyName)
                    .GetValue(flaUiElement.Properties, null);

                var property = propertyObject.GetType().GetProperty("ValueOrDefault").GetValue(propertyObject);

                return this.JsonResponse(ResponseStatus.Success, PrepareValueToSerialize(property));
            }
            catch (Exception) {
                return this.JsonResponse();
            }
        }


        /* Known types:
         * string, bool, int - should be as plain text
         * System.Windows.Automation.ControlType - should be used `ProgrammaticName` property
         * System.Window.Rect, System.Window.Point - overrides `ToString()` method, can serialize
         */
        private static object PrepareValueToSerialize(object obj) {
            if (obj == null) {
                return null;
            }

            if (obj.GetType().IsPrimitive) {
                return obj.ToString();
            }

            return obj is ControlType controlType ? controlType.ToString() : obj;
        }

    }

}