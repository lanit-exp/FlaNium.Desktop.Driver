using System.Collections.Generic;
using System.Threading;
using FlaNium.Desktop.Driver.Automator;
using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.FlaUI;
using FlaNium.Desktop.Driver.Inject;
using FlaNium.Desktop.Driver.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FlaNium.Desktop.Driver.CommandExecutors {

    internal class NewSessionExecutor : CommandExecutorBase {

        protected override JsonResponse DoImpl() {
            JToken flCapabilities = JObject.FromObject(this.ExecutedCommand.Parameters["capabilities"])
                .SelectToken("$..flanium:capabilities");

            if (flCapabilities == null)
                return this.JsonResponse(ResponseStatus.UnknownCommand, "'flanium:capabilities' needed");

            this.Automator.ActualCapabilities =
                Capabilities.CapabilitiesFromJsonString(JsonConvert.SerializeObject(flCapabilities));

            if (!string.IsNullOrWhiteSpace(Automator.ActualCapabilities.App)) {
                InitializeApplication();

                // todo добавить возможность возобновления сессии в режиме дебага
                if (this.Automator.ActualCapabilities.InjectionActivate) {
                    string dllName = this.Automator.ActualCapabilities.InjectionDllType;

                    if (dllName == string.Empty)
                        return this.JsonResponse(ResponseStatus.UnknownCommand,
                            "InjectionDllType Capabilities (DesktopOptions) should NOT be EMPTY! (OR injectionActivate should be false)");


                    string dllFilePath = DllFilesToInject.GetDllFilePath(dllName);

                    if (!Injector.InjectDll(DriverManager.Application.ProcessId, dllFilePath)) {
                        return this.JsonResponse(ResponseStatus.SessionNotCreatedException, "Injecting FAILED!");
                    }

                }
            }
            else {
                DriverManager.SetDesktopAsRootElement();
            }

            // Имеются проблемы ввода текста при активной русской раскладке. Добавлено переключение на английскую раскладку.
            KeyboardLayout.SwitchInputLanguageToEng();

            Dictionary<string, object> startSessionResponse = new Dictionary<string, object>();
            startSessionResponse.Add("sessionId", this.Automator.Session);
            startSessionResponse.Add("capabilities", this.Automator.ActualCapabilities);

            return this.JsonResponse(ResponseStatus.Success, startSessionResponse);
        }

        private void InitializeApplication() {
            var appPath = this.Automator.ActualCapabilities.App;
            var appArguments = this.Automator.ActualCapabilities.Arguments;
            var connectToRunningApp = this.Automator.ActualCapabilities.ConnectToRunningApp;
            var processName = this.Automator.ActualCapabilities.ProcessName;
            var launchDelay = this.Automator.ActualCapabilities.LaunchDelay;
            var processFindTimeOut = this.Automator.ActualCapabilities.ProcessFindTimeOut;

            DriverManager.CloseAppSession(!connectToRunningApp);

            if (!connectToRunningApp) {
                DriverManager.KillAllProcessByName(appPath);
                DriverManager.KillAllProcessByName(processName);
            }

            if (connectToRunningApp && !string.IsNullOrEmpty(processName)) {
                if (DriverManager.AttachToProcessIfExist(processName)) return;
            }

            DriverManager.StartApp(appPath, appArguments);
            Thread.Sleep(launchDelay);

            if (!string.IsNullOrEmpty(processName)) {
                DriverManager.AttachToProcess(processName, processFindTimeOut);
            }
        }

    }

}