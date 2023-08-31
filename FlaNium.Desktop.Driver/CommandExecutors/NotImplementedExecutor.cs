using System;
using FlaNium.Desktop.Driver.Common;

namespace FlaNium.Desktop.Driver.CommandExecutors {

    internal class NotImplementedExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            var msg = string.Format("'{0}' is not valid or implemented command.", this.ExecutedCommand.Name);

            throw new NotImplementedException(msg);
        }

    }

}