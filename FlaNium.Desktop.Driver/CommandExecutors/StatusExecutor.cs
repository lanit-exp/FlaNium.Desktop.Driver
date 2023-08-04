using System.Collections.Generic;
using FlaNium.Desktop.Driver.CommandHelpers;
using FlaNium.Desktop.Driver.Common;

namespace FlaNium.Desktop.Driver.CommandExecutors {

    #region using

    #endregion

    internal class StatusExecutor : CommandExecutorBase {

        protected override JsonResponse DoImpl() {
            var response = new Dictionary<string, object> { { "build", new BuildInfo() }, { "os", new OSInfo() } };

            return this.JsonResponse(ResponseStatus.Success, response);
        }

    }

}