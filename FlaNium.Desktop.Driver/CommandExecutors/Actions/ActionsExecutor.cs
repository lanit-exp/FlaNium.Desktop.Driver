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
                switch (action.id)
                {
                    case "default keyboard":
                        DefaultKeyboardAction.performAction(action);
                        break;
                    case "default mouse":
                        DefaultMouseAction.performAction(action);
                        break;
                    default:
                        return this.JsonResponse(ResponseStatus.UnknownCommand, "Unknown action: " + action.id);
                }
            }

            return this.JsonResponse();
        }
    }
}