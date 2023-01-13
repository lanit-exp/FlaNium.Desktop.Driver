using FlaNium.Desktop.Driver.Common;

namespace FlaNium.Desktop.Driver.CommandExecutors.ElementProperty {

    internal class GetElementTextExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);


            if (element.FlaUIElement.Patterns.Value.IsSupported)
                return this.JsonResponse(ResponseStatus.Success,
                    element.FlaUIElement.Patterns.Value.Pattern.Value.ToString());

            if (element.FlaUIElement.Patterns.Text.IsSupported)
                return this.JsonResponse(ResponseStatus.Success,
                    element.FlaUIElement.Patterns.Text.Pattern.DocumentRange.GetText(int.MaxValue));

            if (element.FlaUIElement.Patterns.Text2.IsSupported)
                return this.JsonResponse(ResponseStatus.Success,
                    element.FlaUIElement.Patterns.Text2.Pattern.DocumentRange.GetText(int.MaxValue));

            return element.FlaUIElement.Patterns.LegacyIAccessible.IsSupported
                ? this.JsonResponse(ResponseStatus.Success,
                    element.FlaUIElement.Patterns.LegacyIAccessible.Pattern.Value.ValueOrDefault)
                : this.JsonResponse(ResponseStatus.Success, element.Text);
        }

    }

}