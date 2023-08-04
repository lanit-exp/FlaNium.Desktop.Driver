using System;
using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.FlaUI;

namespace FlaNium.Desktop.Driver.CommandExecutors {

    internal class CloseExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            DriverManager.CloseAppSession(!this.Automator.ActualCapabilities.ConnectToRunningApp);
            this.Automator.ElementsRegistry.Clear();

            return this.JsonResponse(ResponseStatus.Success, Array.Empty<object>());
        }

    }

}