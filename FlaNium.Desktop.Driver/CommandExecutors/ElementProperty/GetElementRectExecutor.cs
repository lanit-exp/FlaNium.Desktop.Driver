using System.Collections.Generic;
using FlaNium.Desktop.Driver.Common;

namespace FlaNium.Desktop.Driver.CommandExecutors.ElementProperty {

    internal class GetElementRectExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var boundingRect = element.Properties.BoundingRectangle;

            var response = new Dictionary<string, object> {
                { "x", boundingRect.X },
                { "y", boundingRect.Y },
                { "width", boundingRect.Width },
                { "height", boundingRect.Height }
            };

            return this.JsonResponse(ResponseStatus.Success, response);
        }

    }

}