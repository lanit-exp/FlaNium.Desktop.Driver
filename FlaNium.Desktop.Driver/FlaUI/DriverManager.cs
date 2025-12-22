using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
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


        public static void CloseAppSession(bool closeApp) {
            try {
                if (closeApp) {
                    Application?.Close();
                }

                Application?.Dispose();
            }
            catch {
                // ignored
            }

            Application = null;
            _rootElement = null;

            GC.Collect();
        }

        public static int KillAllProcessByName(string name) {
            int count = 0;
            
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
                            count++;
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

            return count;
        }

        public static bool KillProcessById(int id) {
            try {
                Process process = Process.GetProcessById(id);
                process.Kill();

                return true;
            }
            catch {
                return false;
            }
        }

        public static void StartApp(string appPath, string arguments, bool startSecondInstance = false) {
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
                    FileName = appPath,
                    WorkingDirectory = Path.GetDirectoryName(appPath)
                    
                };

                if (!string.IsNullOrEmpty(arguments)) {
                    processStartInfo.Arguments = arguments;
                }

                Application = startSecondInstance ? Application.Launch(processStartInfo) : Application.AttachOrLaunch(processStartInfo);

                Application.WaitWhileBusy(TimeSpan.FromSeconds(10.0));
            }
        }

        public static void AttachToProcess(string processName, int processFindTimeOut) {
            var processes = Retry.While(() => Process.GetProcessesByName(processName),
                p => p.Length == 0,
                TimeSpan.FromMilliseconds(processFindTimeOut),
                TimeSpan.FromMilliseconds(500)).Result;

            if (processes?.Length > 0) {
                Application = Application.Attach(processes[0]);
                ResetRootElement();
            }
            else throw new Exception("Unable to find process with name: " + processName);
        }

        public static int[] GetProcessIdsByName(String processName) {
            Process[] processes = Process.GetProcessesByName(processName);

            return processes.Select(process => process.Id).ToArray();
        }

        public static void AttachToProcessById(int id) {
            try {
                Process process = Process.GetProcessById(id);

                Application = Application.Attach(process);
                ResetRootElement();
            }
            catch (Exception e) {
                throw new Exception($"Unable to attach to process with id {id}: {e.Message}");
            }
        }

        public static bool AttachToProcessIfExist(string processName) {
            var processes = Process.GetProcessesByName(processName);

            if (processes.Length > 0) {
                Application = Application.Attach(processes[0]);
                ResetRootElement();

                return true;
            }

            return false;
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