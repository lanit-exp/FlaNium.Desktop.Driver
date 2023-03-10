using FlaNium.Desktop.Driver.FlaUI;

namespace FlaNium.Desktop.Driver.CommandExecutors {

    internal class ChangeProcessExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            string name = this.ExecutedCommand.Parameters["name"].ToString();
            int timeOut = int.Parse(this.ExecutedCommand.Parameters["timeout"].ToString());
            
            DriverManager.AttachToProcess(name, timeOut);

            return this.JsonResponse();
        }

    }

}