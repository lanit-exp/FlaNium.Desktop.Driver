namespace FlaNium.Desktop.Driver.CommandExecutors
{
    #region using

    using System.Threading;

    using Newtonsoft.Json;

    
    using FlaNium.Desktop.Driver.Automator;
    using FlaNium.Desktop.Driver.Common;
    using FlaNium.Desktop.Driver.FlaUI;

    #endregion

    internal class NewSessionExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {            
            var serializedCapability =
                JsonConvert.SerializeObject(this.ExecutedCommand.Parameters["desiredCapabilities"]);
            this.Automator.ActualCapabilities = Capabilities.CapabilitiesFromJsonString(serializedCapability);

            this.InitializeApplication(this.Automator.ActualCapabilities.DebugConnectToRunningApp);
                      
            Thread.Sleep(this.Automator.ActualCapabilities.LaunchDelay);

            return this.JsonResponse(ResponseStatus.Success, this.Automator.ActualCapabilities);
           
        }

        private void InitializeApplication(bool debugDoNotDeploy = false)
        {
            var appPath = this.Automator.ActualCapabilities.App;
            var appArguments = this.Automator.ActualCapabilities.Arguments;
                    
            DriverManager.StartApp(appPath, appArguments, debugDoNotDeploy);
        }

        #endregion
    }
}
