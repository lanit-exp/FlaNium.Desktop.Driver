namespace FlaNium.Desktop.Driver.CommandExecutors
{
    using System.Threading;
    using Newtonsoft.Json;
    using FlaNium.Desktop.Driver.Automator;
    using FlaNium.Desktop.Driver.Common;
    using FlaNium.Desktop.Driver.FlaUI;
    using static FlaNium.Desktop.Driver.Inject.DLLFilesToInject;
    using InjectDll;
    using FlaNium.Desktop.Driver.Inject;
    using System.IO;
    using FlaNium.Desktop.Driver.Input;

    internal class NewSessionExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {            
            var serializedCapability = JsonConvert.SerializeObject(this.ExecutedCommand.Parameters["desiredCapabilities"]);

            this.Automator.ActualCapabilities = Capabilities.CapabilitiesFromJsonString(serializedCapability);

            this.InitializeApplication();

            FlaNiumKeyboard.SwitchInputLanguageToEng(); // Имеются проблемы ввода текста при активной русской раскладке. Добавлено переключение на английскую раскладку.


            if (this.Automator.ActualCapabilities.InjectionActivate) {

                string appType = this.Automator.ActualCapabilities.AppType;

                if (appType == string.Empty) return this.JsonResponse(ResponseStatus.UnknownCommand, "AppType Capabilities (DesktopOptions) should NOT be EMPTY!");

                try
                {
                    DLLFile dllFile = DLLFilesToInject.GetDLLFile(appType);

                    if (!InjectDll(dllFile))
                    {
                        return this.JsonResponse(ResponseStatus.SessionNotCreatedException, "Injecting FAILED!");
                    }


                }
                catch (InvalidDataException) {
                    return this.JsonResponse(ResponseStatus.UnknownCommand, "Not correct AppType Capabilities (DesktopOptions)!");
                }


                DriverManager.ClientSocket = new ClientSocket();

            }

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
                if (!debugDoNotDeploy)
                {
                    DriverManager.CloseDriver();
                    DriverManager.CloseAllApplication(processName);
                }

                try
                {
                    DriverManager.StartApp(appPath, appArguments, debugDoNotDeploy);
                }
                catch
                {
                    
                }

                Thread.Sleep(launchDelay);
                DriverManager.AttachToProcess(processName);
                       
            }               
        }

        private bool InjectDll(DLLFile dLLFile) {

            string dllPath = getFullDllPath(dLLFile);

            if (dllPath == null) return false;

            int procID = DriverManager.Application.ProcessId;

            if (!Injector.InjectDLL(procID, dllPath)) return false;

            return true;
        }

    }
}
