using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.FlaUI;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;

namespace FlaNium.Desktop.Driver.CommandExecutors.ElementProperty {

    internal class GetOrientationExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            FlaUIDriverElement element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            AutomationElement flaUiElement = element.FlaUIElement;


            OrientationType orientationType = flaUiElement.Properties.Orientation.ValueOrDefault;

            return this.JsonResponse(ResponseStatus.Success, orientationType.ToString());
        }

    }

}