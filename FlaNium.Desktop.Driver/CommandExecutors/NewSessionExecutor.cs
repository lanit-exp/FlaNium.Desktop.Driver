using System.Threading;
using FlaNium.Desktop.Driver.Automator;
using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.FlaUI;
using FlaNium.Desktop.Driver.Inject;
using FlaNium.Desktop.Driver.Input;
using InjectDll;
using Newtonsoft.Json;

namespace FlaNium.Desktop.Driver.CommandExecutors {

    internal class NewSessionExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var serializedCapability =
                JsonConvert.SerializeObject(this.ExecutedCommand.Parameters["desiredCapabilities"]);

            this.Automator.ActualCapabilities = Capabilities.CapabilitiesFromJsonString(serializedCapability);

            this.InitializeApplication();

            // Имеются проблемы ввода текста при активной русской раскладке. Добавлено переключение на английскую раскладку.
            FlaNiumKeyboard.SwitchInputLanguageToEng();


            if (this.Automator.ActualCapabilities.InjectionActivate) {
                string appType = this.Automator.ActualCapabilities.AppType;

                if (appType == string.Empty)
                    return this.JsonResponse(ResponseStatus.UnknownCommand,
                        "AppType Capabilities (DesktopOptions) should NOT be EMPTY! (OR injectionActivate should be false)");


                string dllFilePath = DllFilesToInject.GetDllFilePath(appType);

                if (!Injector.InjectDll(DriverManager.Application.ProcessId, dllFilePath)) {
                    return this.JsonResponse(ResponseStatus.SessionNotCreatedException, "Injecting FAILED!");
                }

                DriverManager.ClientSocket = new ClientSocket();
            }

            return this.JsonResponse(ResponseStatus.Success, this.Automator.ActualCapabilities);
        }

        private void InitializeApplication() {
            var appPath = this.Automator.ActualCapabilities.App;
            var appArguments = this.Automator.ActualCapabilities.Arguments;
            var debugDoNotDeploy = this.Automator.ActualCapabilities.DebugConnectToRunningApp;
            var processName = this.Automator.ActualCapabilities.ProcessName;
            var launchDelay = this.Automator.ActualCapabilities.LaunchDelay;


            if (processName.Length == 0) {
                DriverManager.StartApp(appPath, appArguments, debugDoNotDeploy);
                Thread.Sleep(launchDelay);
            }
            else {
                if (!debugDoNotDeploy) {
                    DriverManager.CloseDriver();
                    DriverManager.CloseAllApplication(processName);
                }

                try {
                    DriverManager.StartApp(appPath, appArguments, debugDoNotDeploy);
                }
                catch {
                    // ignored
                }

                Thread.Sleep(launchDelay);
                DriverManager.AttachToProcess(processName);
            }
        }

    }

}