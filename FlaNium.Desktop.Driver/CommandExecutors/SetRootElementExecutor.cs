using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.FlaUI;

namespace FlaNium.Desktop.Driver.CommandExecutors {

    internal class SetRootElementExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            string type = this.ExecutedCommand.Parameters["type"].ToString();

            switch (type) {
                case "process":
                    DriverManager.ResetRootElement();

                    break;
                case "desktop":
                    DriverManager.SetDesktopAsRootElement();

                    break;
                case "element": {
                    string id = this.ExecutedCommand.Parameters["id"].ToString();
                    var element = this.Automator.ElementsRegistry.GetRegisteredElement(id);
                    
                    DriverManager.SetRootElement(element.FlaUiElement);
                }

                    break;
                default:
                    return this.JsonResponse(ResponseStatus.UnknownCommand,
                        $"Incorrect parameter 'type' = '{type}'. Value must be one of: process, desktop, element.");
            }

            return this.JsonResponse();
        }

    }

}