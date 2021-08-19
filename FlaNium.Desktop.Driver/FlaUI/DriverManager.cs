using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using FlaUI.UIA3;


using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;

using System.Threading;


namespace FlaNium.Desktop.Driver.FlaUI
{
    class DriverManager
    {
        private static TimeSpan implicitTimeout = new TimeSpan(0, 0, 30);
        private static Window _currentWindow;

        public static TimeSpan ImplicitTimeout
        {
            get => DriverManager.implicitTimeout;
            set
            {
                DriverManager.implicitTimeout = value;
                Console.WriteLine(string.Format(" setImplicitTimeout: {0}", (object)DriverManager.ImplicitTimeout));
            }
        }

        public static void refreshImplicitTimeout() => DriverManager.ImplicitTimeout = TimeSpan.FromSeconds(30.0);

        public static string AutomationIdWindowForIgnore { get; } = "ProgressWindowForm";

        public static AutomationBase Automation { get; } = (AutomationBase)new UIA3Automation();

        public static Application Application { get; private set; }

        public static void CloseDriver(bool isDebug = false)
        {
            try
            {
                if (!isDebug)
                    DriverManager.Application?.Close();
                DriverManager.Application?.Dispose();
            }
            catch
            {
            }
            DriverManager.Application = (Application)null;
            DriverManager._currentWindow = (Window)null;
            GC.Collect();
        }

        public static void CloseAllApplication(string name)
        {
            try
            {
                if (name != null)
                {
                    string str = name;
                    string[] separator = new string[2]
                    {
            ".exe",
            ".EXE"
                    };
                    foreach (Process process in Process.GetProcessesByName(str.Split(separator, StringSplitOptions.None)[0]))
                    {
                        try
                        {
                            process.Kill();
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch
            {
            }
            DriverManager.Application = (Application)null;
            DriverManager._currentWindow = (Window)null;
            GC.Collect();
        }

        public static Window[] GetWindows()
        {
            try
            {
                Window mainWindow = DriverManager.Application.GetMainWindow(DriverManager.Automation);
                Window[] array = new List<Window>()
        {
          mainWindow
        }.Union<Window>((IEnumerable<Window>)mainWindow.ModalWindows).Union<Window>((IEnumerable<Window>)DriverManager._currentWindow.ModalWindows).Where<Window>((Func<Window, bool>)(x => x.Properties.AutomationId?.ValueOrDefault != DriverManager.AutomationIdWindowForIgnore)).ToArray<Window>();
                return ((IEnumerable<Window>)array).Any<Window>() ? array : throw new Exception();
            }
            catch
            {
                return DriverManager.GetAllProcessWindows();
            }
        }

        private static Window[] GetAllProcessWindows()
        {
            Window[] allTopLevelWindows = DriverManager.Application.GetAllTopLevelWindows(DriverManager.Automation);
            return ((IEnumerable<Window>)allTopLevelWindows).Union<Window>(((IEnumerable<Window>)allTopLevelWindows).SelectMany<Window, Window>((Func<Window, IEnumerable<Window>>)(w => (IEnumerable<Window>)w.ModalWindows))).Where<Window>((Func<Window, bool>)(x => x.Properties.AutomationId?.ValueOrDefault != DriverManager.AutomationIdWindowForIgnore)).ToArray<Window>();
        }

        public static Window GetActiveWindow()
        {
            if (DriverManager._currentWindow == null)
            {
                DriverManager._currentWindow = Retry.While<Window>((Func<Window>)(() => DriverManager.Application.GetMainWindow(DriverManager.Automation)), (Func<Window, bool>)(x =>
                {
                    try
                    {
                        return x.Properties.AutomationId?.ValueOrDefault == DriverManager.AutomationIdWindowForIgnore;
                    }
                    catch
                    {
                        return true;
                    }
                }), new TimeSpan?(TimeSpan.FromMinutes(5.0))).Result;

                if (DriverManager._currentWindow.FindAllDescendants().Length == 0)
                {
                    DriverManager._currentWindow = DriverManager.Application.GetAllTopLevelWindows(DriverManager.Automation)[0];
                }

            }
            return DriverManager._currentWindow;
        }

        public static Window StartApp(string appPath, string arguments, bool debugDoNotDeploy = false)
        {
            appPath = appPath.Replace("\\\\", "\\");
            string name = appPath.Substring(appPath.LastIndexOf('\\') + 1);
          
            if (!debugDoNotDeploy)
            {
                DriverManager.CloseDriver();
                DriverManager.CloseAllApplication(name);
            }

            if (!File.Exists(appPath))
            {
                // Добавлен механизм запуска приложений из WindowsStore.
                try
                {
                    DriverManager.Application = Application.LaunchStoreApp(name);
                    DriverManager.Application.WaitWhileBusy(new TimeSpan?(TimeSpan.FromSeconds(5.0)));

                    Window activeWindow = DriverManager.GetActiveWindow();
                    activeWindow.FindAllChildren();
                    return activeWindow;
                }
                
                catch (Win32Exception)
                {
                    DriverManager.CloseDriver();
                    DriverManager.CloseAllApplication(name);
                }

                catch (Exception)
                {
                    throw new FileNotFoundException(string.Format("Некорректный путь ({0})", (object)appPath));
                }

            }

            Directory.SetCurrentDirectory(Path.GetDirectoryName(appPath));
            ProcessStartInfo processStartInfo = new ProcessStartInfo()
            {
                FileName = appPath
            };
            switch (arguments)
            {
                case "":
                case null:
                    try
                    {
                        DriverManager.Application = debugDoNotDeploy ? Application.AttachOrLaunch(processStartInfo) : Application.Launch(processStartInfo);
                        DriverManager.Application.WaitWhileBusy(new TimeSpan?(TimeSpan.FromSeconds(5.0)));
                    }
                    catch (Win32Exception)
                    {
                        DriverManager.CloseDriver();
                        DriverManager.CloseAllApplication(name);
                    }
                    Window activeWindow = DriverManager.GetActiveWindow();
                    activeWindow.FindAllChildren();
                    return activeWindow;
                default:
                    processStartInfo.Arguments = arguments;
                    goto case "";
            }
        }

        public static void Click(Point p)
        {
            DriverManager.GetActiveWindow().SetForeground();
            Mouse.Position = p;
            Mouse.Click(MouseButton.Left);
        }

        public static async Task<Task<RetryResult<Window>>> FindWindow(
          string title,
          TimeSpan timeout)
        {
            Func<RetryResult<Window>> function1 = (Func<RetryResult<Window>>)(() => Retry.While<Window>((Func<Window>)(() => ((IEnumerable<Window>)DriverManager.GetAllProcessWindows()).FirstOrDefault<Window>((Func<Window, bool>)(x => x.Title == title))), (Func<Window, bool>)(x => x == null), new TimeSpan?(timeout)));
            Func<RetryResult<Window>> function2 = (Func<RetryResult<Window>>)(() => Retry.While<Window>((Func<Window>)(() => ((IEnumerable<Window>)DriverManager.GetWindows()).FirstOrDefault<Window>((Func<Window, bool>)(x => x.Title == title))), (Func<Window, bool>)(x => x == null), new TimeSpan?(timeout)));
            return await Task.WhenAny<RetryResult<Window>>(Task.Run<RetryResult<Window>>(function1), Task.Run<RetryResult<Window>>(function2));
        }

        public static void SwitchWindow(string title)
        {
            long timeMilliseconds = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            Task<RetryResult<Window>> task = (Task<RetryResult<Window>>)null;
            while ((double)timeMilliseconds > (double)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - DriverManager.ImplicitTimeout.TotalMilliseconds)
            {
                if (title == "")
                {
                    DriverManager._currentWindow = DriverManager.Application.GetMainWindow(DriverManager.Automation);
                    DriverManager._currentWindow.SetForeground();
                    return;
                }
                Task<Task<RetryResult<Window>>> window = DriverManager.FindWindow(title, DriverManager.ImplicitTimeout);
                window.Wait();
                task = !window.IsFaulted ? window.Result : throw window.Exception;
                if (task.IsFaulted)
                    Thread.Sleep(1000);
                else
                    break;
            }
            if (task.IsFaulted)
                throw new Exception(string.Format("Fail to get Window {0}", (object)title), (Exception)task.Exception);
            DriverManager._currentWindow = task.Result.Result;
            DriverManager._currentWindow.SetForeground();
        }

        public static void PrintTimestamp(string comment)
        {
        }

        public static string DownloadTempFile(string filename)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "\\\\").TrimEnd('\\', '/'), filename);
            if (!File.Exists(path))
            {
                Logger.Error(string.Format("File does not exist {0}", (object)path));
                return (string)null;
            }
            byte[] inArray = File.ReadAllBytes(path);
            File.Delete(path);
            return Convert.ToBase64String(inArray);
        }


        public static void AttachToProcess(string processName)
        {
            DriverManager.Application = Application.Attach(processName);
        }
    }
}
