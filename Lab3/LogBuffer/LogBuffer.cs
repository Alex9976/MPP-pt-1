using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogBuffer
{
    public class LogBuffer
    {
        private List<string> _logList = new List<string>();
        private object _lock = new object();
        private object _writeToFileLock = new object();
        private int _listLimit = 20;
        private Timer _timer;
        private string _path = "D:\\Log.txt";

        public LogBuffer()
        {
            _timer = new Timer(new TimerCallback(Flush), null, 0, 2000);
        }

        public LogBuffer(string path, int listlimit, int period)
        {
            _listLimit = listlimit;
            _path = path;
            _timer = new Timer(new TimerCallback(Flush), null, 0, period);
        }

        public void Flush(object state)
        {
            Write();
        }

        public void Add(string item)
        {
            bool isOverflow = false;
            lock (_lock)
            {
                _logList.Add(item);
                isOverflow = _logList.Count >= _listLimit;
            }

            if (isOverflow)
            {
                Write();
            }
        }

        private void Write()
        {
            List<string> _buffer = null;
            lock (_lock)
            {
                _buffer = new List<string>(_logList);
                _logList.Clear();
            }

            lock (_writeToFileLock)
            {
                File.AppendAllLines(_path, _buffer);
            }
        }
    }
}
