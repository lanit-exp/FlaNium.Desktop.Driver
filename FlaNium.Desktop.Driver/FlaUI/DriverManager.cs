using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using FlaNium.Desktop.Driver.Inject;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using FlaUI.UIA3;

namespace FlaNium.Desktop.Driver.FlaUI {

    class DriverManager {

        private static TimeSpan _implicitTimeout = new TimeSpan(0, 0, 30);
        private static AutomationBase _automation = new UIA3Automation();
        private static string _automationIdWindowForIgnore = "ProgressWindowForm";

        public static TimeSpan ImplicitTimeout {
            get => _implicitTimeout;
            set {
                _implicitTimeout = value;
                Console.WriteLine($" setImplicitTimeout: {ImplicitTimeout}");
            }
        }

        public static void RefreshImplicitTimeout() => ImplicitTimeout = TimeSpan.FromSeconds(30.0);


        public static Application Application { get; private set; }

        private static AutomationElement _rootElement;

        // Используется для обмена данными с инжектируемой dll
        public static ClientSocket ClientSocket { get; set; }


        public static void CloseAppSession(bool closeApp) {
            try {
                if (closeApp) {
                    Application?.Close();
                }

                Application?.Dispose();
                ClientSocket?.FreeSocket();
            }
            catch {
                // ignored
            }

            Application = null;
            _rootElement = null;
            ClientSocket = null;

            GC.Collect();
        }

        public static void KillAllProcessByName(string name) {
            string[] separator = {
                ".exe",
                ".EXE"
            };

            try {
                if (name != null) {
                    string processName = Path.GetFileName(name).Split(separator, StringSplitOptions.None)[0];

                    foreach (Process process in Process.GetProcessesByName(processName)) {
                        try {
                            process.Kill();
                        }
                        catch {
                            // ignored
                        }
                    }
                }
            }
            catch {
                // ignored
            }
        }

        public static void StartApp(string appPath, string arguments) {
            appPath = appPath.Replace("\\\\", "\\");

            if (!File.Exists(appPath)) {
                // Добавлен механизм запуска приложений из WindowsStore.
                try {
                    Application = Application.LaunchStoreApp(appPath);
                    Application.WaitWhileBusy(TimeSpan.FromSeconds(10.0));
                }
                catch (Exception) {
                    CloseAppSession(true);

                    throw new FileNotFoundException($"Application not found: '{appPath}'");
                }
            }
            else {
                ProcessStartInfo processStartInfo = new ProcessStartInfo() {
                    FileName = appPath
                };

                if (!string.IsNullOrEmpty(arguments)) {
                    processStartInfo.Arguments = arguments;
                }

                Application = Application.AttachOrLaunch(processStartInfo);

                Application.WaitWhileBusy(TimeSpan.FromSeconds(10.0));
            }
        }

        public static void AttachToProcess(string processName, int processFindTimeOut = 30000) {
            var processes = Retry.While(() => Process.GetProcessesByName(processName),
                p => p.Length == 0,
                TimeSpan.FromMilliseconds(processFindTimeOut),
                TimeSpan.FromMilliseconds(500)).Result;

            if (processes.Length > 0) {
                Application = Application.Attach(processes[0]);
            }
            else throw new Exception("Unable to find process with name: " + processName);
        }


        public static AutomationElement GetRootElement() {
            if (_rootElement == null) {
                _rootElement = Retry.While(() => Application.GetMainWindow(_automation),
                    x => {
                        try {
                            return x.Properties.AutomationId?.ValueOrDefault == _automationIdWindowForIgnore;
                        }
                        catch {
                            return true;
                        }
                    }, TimeSpan.FromMinutes(2.0)).Result;

                if (_rootElement.FindAllDescendants().Length == 0) {
                    _rootElement = Application.GetAllTopLevelWindows(_automation)[0];
                }
            }

            return _rootElement;
        }


        public static void SetDesktopAsRootElement() {
            AutomationElement desktop = _automation.GetDesktop();
            if (desktop != null) {
                _rootElement = desktop;
            }
            else {
                throw new Exception("Cannot find Desktop automation element");
            }
        }

        public static void SetRootElement(AutomationElement automationElement) {
            _rootElement = automationElement;
        }

        public static void ResetRootElement() {
            _rootElement = null;
        }

        public static void Click(Point p) {
            GetRootElement().SetForeground();
            Mouse.Position = p;
            Mouse.Click(MouseButton.Left);
        }

    }

}