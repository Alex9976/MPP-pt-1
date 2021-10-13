using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogBuffer
{
    public class LogBuffer
    {
        private static List<string> _buffer = new List<string>();
        private static object _lock = new object();
        private static object _writeToFileLock = new object();
        private Thread _thread;
        private static int BUFFER_LENGTH = 20;

        public LogBuffer()
        {
            _thread = new Thread(Timer);
            _thread.IsBackground = true;
        }

        ~LogBuffer()
        {
            _thread.IsBackground = false;
        }

        public void Timer()
        {
            while (Thread.CurrentThread.IsBackground)
            {
                Thread.Sleep(2000);
                Flush();
            }
        }

        public void Add(string item)
        {
            lock (_lock)
            {
                _buffer.Add(item);
                if (_buffer.Count >= BUFFER_LENGTH)
                {
                    Flush();
                }
            }
        }

        private void Flush()
        {
            lock (_lock)
            {
                File.AppendAllLines("D:\\Log.txt", _buffer);
                _buffer.Clear();
            }
        }
    }
}
