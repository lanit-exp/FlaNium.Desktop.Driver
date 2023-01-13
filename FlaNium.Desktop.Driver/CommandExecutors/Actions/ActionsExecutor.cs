using System.Collections.Generic;
using FlaNium.Desktop.Driver.Common;
using Newtonsoft.Json;

namespace FlaNium.Desktop.Driver.CommandExecutors.Actions
{
    class ActionsExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            List<Action> actions =
                JsonConvert.DeserializeObject<List<Action>>(this.ExecutedCommand.Parameters["actions"]
                    .ToString());


            foreach (Action action in actions)
            {
                switch (action.Id)
                {
                    case "default keyboard":
                        DefaultKeyboardAction.PerformAction(action);
                        break;
                    case "default mouse":
                        DefaultMouseAction.PerformAction(action);
                        break;
                    default:
                        return this.JsonResponse(ResponseStatus.UnknownCommand, "Unknown action: " + action.Id);
                }
            }

            return this.JsonResponse();
        }
    }
}