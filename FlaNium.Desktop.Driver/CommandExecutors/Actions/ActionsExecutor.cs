using System;
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
            
            if (actions.Count == 0) return this.JsonResponse(ResponseStatus.UnknownCommand, "Actions list is empty");
            
            // Поскольку Selenium не сохраняет порядок действий, добавлен костыль устанавливающий действия мыши раньше чем клавиатура.
            actions.Sort((action, action1) => String.Compare(action1.Id, action.Id, StringComparison.Ordinal));

            foreach (Action action in actions)
            {
                switch (action.Id)
                {
                    case "default keyboard":
                        DefaultKeyboardAction.PerformAction(action, this.Automator);
                        break;
                    case "default mouse":
                        DefaultMouseAction.PerformAction(action, this.Automator);
                        break;
                    case "default wheel":
                        DefaultWheelAction.PerformAction(action, this.Automator);
                        break;
                    default:
                        return this.JsonResponse(ResponseStatus.UnknownCommand, $"Unknown action: {action.Id}");
                }
            }

            return this.JsonResponse();
        }
    }
}