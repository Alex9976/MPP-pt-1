using System.Runtime.InteropServices;

namespace Lab2
{
    class Program
    {
        public const int STD_OUTPUT_HANDLE = -11;

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        static void Main(string[] args)
        {
            Thread thread = new Thread(PrintThreadInfo);
            thread.Start();

            OSHandle handle = new OSHandle(GetStdHandle(STD_OUTPUT_HANDLE));
            CloseHandle(handle);
        }

        static async void CloseHandle(OSHandle handle)
        {
            await Task.Delay(2000);
            Console.WriteLine($"Closing handle: {handle.Handle}");
            handle.Dispose();
        }

        static void PrintThreadInfo()
        {
            Mutex mutex = new Mutex();
            while (true)
            {
                try
                {
                    mutex.Lock();
                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is alive");
                    mutex.Unlock();
                    Thread.Sleep(250);
                }
                catch
                {
                    mutex.Unlock();
                    break;
                }
            }
        }
    }
}