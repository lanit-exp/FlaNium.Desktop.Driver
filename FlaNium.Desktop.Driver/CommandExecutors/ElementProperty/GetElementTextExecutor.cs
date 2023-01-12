using FlaNium.Desktop.Driver.Common;

namespace FlaNium.Desktop.Driver.CommandExecutors.ElementProperty {

    internal class GetElementTextExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);


            if (element.FlaUIElement.Patterns.Value.IsSupported)
                return this.JsonResponse(ResponseStatus.Success,
                    (object)element.FlaUIElement.Patterns.Value.Pattern.Value.ToString());

            if (element.FlaUIElement.Patterns.Text.IsSupported)
                return this.JsonResponse(ResponseStatus.Success,
                    (object)element.FlaUIElement.Patterns.Text.Pattern.DocumentRange.GetText(int.MaxValue).ToString());

            if (element.FlaUIElement.Patterns.Text2.IsSupported)
                return this.JsonResponse(ResponseStatus.Success,
                    (object)element.FlaUIElement.Patterns.Text2.Pattern.DocumentRange.GetText(int.MaxValue).ToString());

            return element.FlaUIElement.Patterns.LegacyIAccessible.IsSupported
                ? this.JsonResponse(ResponseStatus.Success,
                    (object)element.FlaUIElement.Patterns.LegacyIAccessible.Pattern.Value.ValueOrDefault.ToString())
                : this.JsonResponse(ResponseStatus.Success, (object)element.Text);
        }

    }

}