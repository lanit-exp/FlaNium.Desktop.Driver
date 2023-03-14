using System;

namespace FlaNium.Desktop.Driver.CommandExecutors.StandardActions {

    internal class SetOrientationExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var msg = string.Format("'{0}' is not valid or implemented command.", this.ExecutedCommand.Name);

            throw new NotImplementedException(msg);
        }

    }

}