using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace FlaNium.Desktop.Driver.Inject {

    class Injector {

        [DllImport("kernel32")]
        public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize,
                                                       UIntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags,
                                                       out IntPtr lpThreadId);


        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(UInt32 dwDesiredAccess, Int32 bInheritHandle, Int32 dwProcessId);


        [DllImport("kernel32.dll")]
        public static extern Int32 CloseHandle(IntPtr hObject);


        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, UIntPtr dwSize, uint dwFreeType);


        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern UIntPtr GetProcAddress(IntPtr hModule, string procName);


        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType,
                                            uint flProtect);


        [DllImport("kernel32.dll")]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, string lpBuffer, UIntPtr nSize,
                                              out IntPtr lpNumberOfBytesWritten);


        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);


        [DllImport("kernel32", SetLastError = true, ExactSpelling = true)]
        internal static extern UInt32 WaitForSingleObject(IntPtr handle, Int32 milliseconds);


        public static bool InjectDll(Int32 procId, String filePath) {
            Logger.Debug("Inject DLL: \"{0}\" into process: {1}", filePath, procId);

            IntPtr hProcess = OpenProcess(0x1F0FFF, 1, procId);


            if (hProcess == IntPtr.Zero) {
                Logger.Error("OpenProcess Failed!");

                return false;
            }

            Int32 lenWrite = filePath.Length + 1;

            // выделение памяти в виртуальном адресном пространстве инжектируемого процесса  
            IntPtr allocMem = VirtualAllocEx(hProcess, (IntPtr)null, (uint)lenWrite, 0x1000 | 0x2000, 0x04);


            // запись dll в память инжектируемого процесса
            bool writeProcMem =
                WriteProcessMemory(hProcess, allocMem, filePath, (UIntPtr)lenWrite, out IntPtr bytesOut);

            if (!writeProcMem) {
                Logger.Error("WriteProcessMemory ERROR!");

                return false;
            }


            // получение адреса
            UIntPtr injector = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");

            if (injector == UIntPtr.Zero) {
                Logger.Error("GetProcAddress ERROR!");

                return false;
            }


            // создание треда в инжектируемом процессе 
            IntPtr hThread = CreateRemoteThread(hProcess, (IntPtr)null, 0, injector, allocMem, 0, out IntPtr bytesOut2);

            // проверка валидности треда
            if (hThread == IntPtr.Zero) {
                Logger.Error("CreateRemoteThread ERROR!");

                return false;
            }

            // ожидание завершения инжекта 
            UInt32 result = WaitForSingleObject(hThread, 30 * 1000);

            // ловим по таймауту  
            if (result == 0x00000080L || result == 0x00000102L || result == 0xFFFFFFFF) {
                Logger.Error("Injection Result ERROR: {0}", result);
                CloseHandle(hThread);

                return false;
            }

            // Ожидание на запуск всех процессов инжектируемой библиотеки   
            Thread.Sleep(3000);

            // освобождение выделенной памяти  
            bool virtFreeExStatus = VirtualFreeEx(hProcess, allocMem, (UIntPtr)0, 0x8000);

            if (!virtFreeExStatus) Logger.Error("VirtualFreeEx ERROR!");

            CloseHandle(hThread);

            Logger.Debug("Injection: SUCSESSFUL!");

            return true;
        }

    }

}