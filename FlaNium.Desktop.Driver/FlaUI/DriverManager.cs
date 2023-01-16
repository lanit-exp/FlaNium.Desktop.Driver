using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FlaNium.Desktop.Driver.Inject;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using FlaUI.UIA3;

namespace FlaNium.Desktop.Driver.FlaUI {

    class DriverManager {

        private static TimeSpan _implicitTimeout = new TimeSpan(0, 0, 30);
        private static Window _currentWindow;

        public static TimeSpan ImplicitTimeout {
            get => _implicitTimeout;
            set {
                _implicitTimeout = value;
                Console.WriteLine($" setImplicitTimeout: {ImplicitTimeout}");
            }
        }

        public static void RefreshImplicitTimeout() => ImplicitTimeout = TimeSpan.FromSeconds(30.0);

        public static string AutomationIdWindowForIgnore { get; } = "ProgressWindowForm";

        public static AutomationBase Automation { get; } = new UIA3Automation();

        public static Application Application { get; private set; }

        public static ClientSocket ClientSocket { get; set; }

        public static void CloseDriver(bool isDebug = false) {
            try {
                if (!isDebug) {
                    Application?.Close();
                }

                Application?.Dispose();
                ClientSocket.FreeSocket();
            }
            catch {
                // ignored
            }

            Application = null;
            _currentWindow = null;
            ClientSocket = null;

            GC.Collect();
        }

        public static void CloseAllApplication(string name) {
            try {
                if (name != null) {
                    string str = name;
                    string[] separator = new string[] {
                        ".exe",
                        ".EXE"
                    };
                    foreach (Process process in Process.GetProcessesByName(
                                 str.Split(separator, StringSplitOptions.None)[0])) {
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

            Application = null;
            _currentWindow = null;
            GC.Collect();
        }

        public static Window[] GetWindows() {
            try {
                Window mainWindow = Application.GetMainWindow(Automation);
                Window[] array = new List<Window>() {
                        mainWindow
                    }.Union(mainWindow.ModalWindows)
                    .Union(_currentWindow.ModalWindows)
                    .Where(x =>
                        x.Properties.AutomationId?.ValueOrDefault != AutomationIdWindowForIgnore).ToArray();

                return (array).Any() ? array : throw new Exception();
            }
            catch {
                return GetAllProcessWindows();
            }
        }

        private static Window[] GetAllProcessWindows() {
            Window[] allTopLevelWindows = Application.GetAllTopLevelWindows(Automation);

            return (allTopLevelWindows)
                .Union((allTopLevelWindows).SelectMany(
                    (w => w.ModalWindows)))
                .Where((x =>
                    x.Properties.AutomationId?.ValueOrDefault != AutomationIdWindowForIgnore)).ToArray();
        }

        public static Window GetActiveWindow() {
            if (_currentWindow == null) {
                _currentWindow = Retry.While((() => Application.GetMainWindow(Automation)),
                    (x => {
                        try {
                            return x.Properties.AutomationId?.ValueOrDefault == AutomationIdWindowForIgnore;
                        }
                        catch {
                            return true;
                        }
                    }), TimeSpan.FromMinutes(5.0)).Result;

                if (_currentWindow.FindAllDescendants().Length == 0) {
                    _currentWindow = Application.GetAllTopLevelWindows(Automation)[0];
                }
            }

            return _currentWindow;
        }

        public static Window StartApp(string appPath, string arguments, bool debugDoNotDeploy = false) {
            appPath = appPath.Replace("\\\\", "\\");
            string name = appPath.Substring(appPath.LastIndexOf('\\') + 1);

            if (!debugDoNotDeploy) {
                CloseDriver();
                CloseAllApplication(name);
            }

            if (!File.Exists(appPath)) {
                // Добавлен механизм запуска приложений из WindowsStore.
                try {
                    Application = Application.LaunchStoreApp(name);
                    Application.WaitWhileBusy(TimeSpan.FromSeconds(5.0));

                    Window activeWindow = GetActiveWindow();
                    activeWindow.FindAllChildren();

                    return activeWindow;
                }

                catch (Win32Exception) {
                    CloseDriver();
                    CloseAllApplication(name);
                }

                catch (Exception) {
                    throw new FileNotFoundException($"Некорректный путь ({appPath})");
                }
            }

            Directory.SetCurrentDirectory(Path.GetDirectoryName(appPath));
            ProcessStartInfo processStartInfo = new ProcessStartInfo() {
                FileName = appPath
            };
            switch (arguments) {
                case "":
                case null:
                    try {
                        Application = debugDoNotDeploy
                            ? Application.AttachOrLaunch(processStartInfo)
                            : Application.Launch(processStartInfo);
                        Application.WaitWhileBusy(TimeSpan.FromSeconds(5.0));
                    }
                    catch (Win32Exception) {
                        CloseDriver();
                        CloseAllApplication(name);
                    }

                    Window activeWindow = GetActiveWindow();
                    activeWindow.FindAllChildren();

                    return activeWindow;
                default:
                    processStartInfo.Arguments = arguments;
                    goto case "";
            }
        }

        public static void Click(Point p) {
            GetActiveWindow().SetForeground();
            Mouse.Position = p;
            Mouse.Click(MouseButton.Left);
        }

        public static async Task<Task<RetryResult<Window>>> FindWindow(
            string title,
            TimeSpan timeout) {
            RetryResult<Window> Function1() =>
                Retry.While((() => (GetAllProcessWindows()).FirstOrDefault((x => x.Title == title))), (x => x == null),
                    timeout);

            RetryResult<Window> Function2() =>
                Retry.While((() => (GetWindows()).FirstOrDefault((x => x.Title == title))), (x => x == null),
                    timeout);

            return await Task.WhenAny(Task.Run(Function1),
                Task.Run(Function2));
        }

        public static void SwitchWindow(string title) {
            long timeMilliseconds = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            Task<RetryResult<Window>> task = null;
            while (timeMilliseconds > DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() -
                   ImplicitTimeout.TotalMilliseconds) {
                if (title == "") {
                    _currentWindow = Application.GetMainWindow(Automation);
                    _currentWindow.SetForeground();

                    return;
                }

                Task<Task<RetryResult<Window>>> window = FindWindow(title, ImplicitTimeout);
                window.Wait();
                task = !window.IsFaulted ? window.Result : throw window.Exception;

                if (task.IsFaulted)
                    Thread.Sleep(1000);
                else
                    break;
            }

            if (task.IsFaulted)
                throw new Exception($"Fail to get Window {title}", task.Exception);
            _currentWindow = task.Result.Result;
            _currentWindow.SetForeground();
        }

        public static string DownloadTempFile(string filename) {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "\\\\").TrimEnd('\\', '/'),
                filename);
            if (!File.Exists(path)) {
                Logger.Error($"File does not exist {path}");

                return null;
            }

            byte[] inArray = File.ReadAllBytes(path);
            File.Delete(path);

            return Convert.ToBase64String(inArray);
        }


        public static void AttachToProcess(string processName) {
            var processes = Process.GetProcessesByName(processName);

            if (processes.Length > 0) {
                Application = Application.Attach(processes[0]);
            }
            else throw new Exception("Unable to find process with name: " + processName);
        }

    }

}