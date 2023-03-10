using FlaNium.Desktop.Driver.FlaUI;

namespace FlaNium.Desktop.Driver.CommandExecutors {

    internal class KillProcessesExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            string name = this.ExecutedCommand.Parameters["name"].ToString();
            
            DriverManager.KillAllProcessByName(name);

            return this.JsonResponse();
        }

    }

}