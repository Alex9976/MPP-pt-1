using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class OSHandle : IDisposable
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr handle);

        public IntPtr Handle {  get; set; }
        private readonly Mutex mutex = new Mutex();
        private bool disposed = false;


        public OSHandle(IntPtr Handle)
        {
            this.Handle = Handle;
        }

        ~OSHandle()
        {
            Finalize(false);
        }

        private void Finalize(bool disposing)
        {
            mutex.Lock();

            if (!disposed)
            {
                if (disposing && Handle != IntPtr.Zero)
                {
                    CloseHandle(Handle);
                    Handle = IntPtr.Zero;     
                }

                disposed = true;
            }

            mutex.Unlock();
        }

        public void Dispose()
        {
            Finalize(true);
            GC.SuppressFinalize(this);
        }
    }
}
