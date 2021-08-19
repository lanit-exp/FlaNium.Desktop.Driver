namespace FlaNium.Desktop.Driver.CommandExecutors
{
    using System.Threading;
    using Newtonsoft.Json;
    using FlaNium.Desktop.Driver.Automator;
    using FlaNium.Desktop.Driver.Common;
    using FlaNium.Desktop.Driver.FlaUI;
    using FlaNium.Desktop.Driver.Input;


    internal class NewSessionExecutor : CommandExecutorBase
    {

        protected override string DoImpl()
        {            
            var serializedCapability = JsonConvert.SerializeObject(this.ExecutedCommand.Parameters["desiredCapabilities"]);

            this.Automator.ActualCapabilities = Capabilities.CapabilitiesFromJsonString(serializedCapability);

            this.InitializeApplication();

            FlaNiumKeyboard.SwitchInputLanguageToEng(); // Имеются проблемы ввода текста при активной русской раскладке. Добавлено переключение на английскую раскладку.

            return this.JsonResponse(ResponseStatus.Success, this.Automator.ActualCapabilities);
        }

        private void InitializeApplication()
        {
            var appPath = this.Automator.ActualCapabilities.App;
            var appArguments = this.Automator.ActualCapabilities.Arguments;
            var debugDoNotDeploy = this.Automator.ActualCapabilities.DebugConnectToRunningApp;
            var processName = this.Automator.ActualCapabilities.ProcessName;
            var launchDelay = this.Automator.ActualCapabilities.LaunchDelay;
            

            if (processName.Length == 0)
            {
                DriverManager.StartApp(appPath, appArguments, debugDoNotDeploy);
                Thread.Sleep(launchDelay);
            }
            else
            {
                try
                {
                    DriverManager.StartApp(appPath, appArguments, debugDoNotDeploy);
                    Thread.Sleep(launchDelay);
                }
                catch
                {
                    Thread.Sleep(launchDelay);
                    DriverManager.AttachToProcess(processName);
                }
            }
        }

    }
}
