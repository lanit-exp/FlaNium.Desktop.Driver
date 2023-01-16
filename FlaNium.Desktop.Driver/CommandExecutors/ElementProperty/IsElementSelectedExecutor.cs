using FlaNium.Desktop.Driver.Common;
using FlaUI.Core.Definitions;
using FlaUI.Core.Patterns;

namespace FlaNium.Desktop.Driver.CommandExecutors.ElementProperty {

    internal class IsElementSelectedExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey).FlaUiElement;


            bool? nullable1 = element.Patterns.SelectionItem.PatternOrDefault?.IsSelected?.Value;
            ref bool? local = ref nullable1;
            bool? nullable2 = nullable1;
            int num;

            if (!nullable2.HasValue) {
                ITogglePattern patternOrDefault = element.Patterns.Toggle.PatternOrDefault;
                num = patternOrDefault != null ? (patternOrDefault.ToggleState.Value == ToggleState.On ? 1 : 0) : 0;
            }
            else
                num = nullable2.GetValueOrDefault() ? 1 : 0;

            local = new bool?(num != 0);

            return nullable1.HasValue
                ? this.JsonResponse(ResponseStatus.Success, (object)nullable1)
                : this.JsonResponse(ResponseStatus.InvalidElementState, (object)element);
        }

    }

}