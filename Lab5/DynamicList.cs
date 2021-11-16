using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public class DynamicList<T> : IEnumerable<T>
    {
        private T[] _innerArray = new T[0];
        private int _currentLength = 0;
        private int _innerArrayLength = 0;
        private object _lock = new object();
        public int Count { get { return _currentLength; } }

        public DynamicList() { }

        public DynamicList(int size)
        {
            if (size < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size), "Size can't be less than zero");
            }
            else if (size > 0)
            {
                _innerArray = new T[size];
                _innerArrayLength = size;
            }
        }

        public void Add(T element)
        {
            if (_currentLength == _innerArrayLength)
            {
                _innerArrayLength = _innerArrayLength == 0 ? 4 : _innerArrayLength * 2;
                T[] copy = new T[_innerArrayLength];
                Array.Copy(_innerArray, copy, _currentLength);
                _innerArray = copy;
            }

            lock (_lock)
            {
                _innerArray[_currentLength++] = element;
            }
        }

        public bool Remove(T element)
        {
            for (int i = 0; i < _currentLength; i++)
            {
                if (_innerArray[i].Equals(element))
                {
                    lock (_lock)
                    {
                        _innerArray = _innerArray.Where((val, idx) => idx != i).ToArray();
                        _innerArrayLength--;
                        _currentLength--;
                    }
                    return true;
                }
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _currentLength)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            lock (_lock)
            {
                _innerArray = _innerArray.Where((val, idx) => idx != index).ToArray();
                _innerArrayLength--;
                _currentLength--;
            }
        }

        public void Clear()
        {
            lock (_lock)
            {
                _innerArray = new T[0];
            }
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _currentLength)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), $"Index {index} out of range");
                }
                else
                {
                    return _innerArray[index];
                }
            }
            set
            {
                if (index < 0 || index >= _currentLength)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), $"Index {index} out of range");
                }
                else
                {
                    _innerArray[index] = value;
                }
            }
        }

        public override string ToString()
        {
            string output = string.Empty;
            for (int i = 0; i < _currentLength - 1; i++)
                output += _innerArray[i] + ", ";

            return output + _innerArray[_currentLength - 1];
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new DynamicListEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        class DynamicListEnumerator : IEnumerator<T>
        {
            private DynamicList<T> _list;
            private int position = 0;
            private T current = default;
            public DynamicListEnumerator(DynamicList<T> list)
            {
                _list = list;
            }

            public T Current
            {
                get
                {
                    if (position < 0 || position >= _list.Count)
                        throw new InvalidOperationException();
                    return _list[position];
                }
            }
            T IEnumerator<T>.Current { get => current; }
            object IEnumerator.Current
            {
                get
                {
                    if (position == 0 || position == _list.Count + 1)
                    {
                        throw new InvalidOperationException("position");
                    }

                    return Current;
                }
            }

            public bool MoveNext()
            {
                if (position < _list.Count)
                {
                    current = _list[position];
                    position++;
                    return true;
                }
                else
                {
                    position = _list.Count + 1;
                    current = default;
                    return false;
                }
            }

            public void Reset()
            {
                position = 0;
                current = default;
            }
            public void Dispose() { }
        }
    }
}
