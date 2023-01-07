using FlaNium.Desktop.Driver;
using System;
using System.Runtime.InteropServices;
using System.Threading;


namespace InjectDll
{
    class Injector
    {
        #region dll import
        [DllImport("kernel32")]
        public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, UIntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, out IntPtr lpThreadId);


        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(UInt32 dwDesiredAccess, Int32 bInheritHandle, Int32 dwProcessId);


        [DllImport("kernel32.dll")]
        public static extern Int32 CloseHandle(IntPtr hObject);


        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, UIntPtr dwSize, uint dwFreeType);


        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern UIntPtr GetProcAddress(IntPtr hModule, string procName);


        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);


        [DllImport("kernel32.dll")]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, string lpBuffer, UIntPtr nSize, out IntPtr lpNumberOfBytesWritten);


        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);


        [DllImport("kernel32", SetLastError = true, ExactSpelling = true)]
        internal static extern UInt32 WaitForSingleObject(IntPtr handle, Int32 milliseconds);
        #endregion


        public static bool InjectDLL(Int32 ProcID, String strDLLName)
        {
            Logger.Debug("Inject DLL: \"{0}\" into process: {1}", strDLLName, ProcID);

            IntPtr hProcess = OpenProcess(0x1F0FFF, 1, ProcID);


            if (hProcess == null)
            {
                Logger.Error("OpenProcess Failed!");
                return false;
            }

            Int32 LenWrite = strDLLName.Length + 1;

            // выделение памяти в виртуальном адресном пространстве инжектируемого процесса  
            IntPtr AllocMem = VirtualAllocEx(hProcess, (IntPtr)null, (uint)LenWrite, 0x1000 | 0x2000, 0x04);


            // запись dll в память инжектируемого процесса
            bool writeProcMem = WriteProcessMemory(hProcess, AllocMem, strDLLName, (UIntPtr)LenWrite, out IntPtr bytesout);

            if (!writeProcMem)
            {
                Logger.Error("WriteProcessMemory ERROR!");
                return false;
            }


            // получение адреса
            UIntPtr Injector = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");

            if (Injector == null)
            {
                Logger.Error("GetProcAddress ERROR!");
                return false;
            }


            // создание треда в инжектируемом процессе 
            IntPtr hThread = CreateRemoteThread(hProcess, (IntPtr)null, 0, Injector, AllocMem, 0, out IntPtr bytesout2);

            // проверка валидности треда
            if (hThread == null)
            {
                Logger.Error("CreateRemoteThread ERROR!");
                return false;
            }

            // ожидание завершения инжекта 
            UInt32 Result = WaitForSingleObject(hThread, 3 * 1000);

            // TODO: реализовать возврат dllMain метода

            // ловим по таймауту  
            //if (Result == 0x00000080L || Result == 0x00000102L || Result == 0xFFFFFFFF)
            //{
            //    Logger.Error("Injection Result ERROR: {0}", Result);
            //    if (hThread != null) { CloseHandle(hThread); }
            //    return false;
            //}

            // секундное прерывание  
            Thread.Sleep(1000);

            // освобождение выделенной памяти  
            bool virtFreeExStatus = VirtualFreeEx(hProcess, AllocMem, (UIntPtr)0, 0x8000);

            if (!virtFreeExStatus) Logger.Error("VirtualFreeEx ERROR!");


            // проверка валидности треда предотвращение раннего краша процесса приложения  
            if (hThread != null)
            {
                CloseHandle(hThread);
            }

            Logger.Debug("Injection: SUCSESSFUL!");

            return true;
        }
    }
}