using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;

namespace RingBuffer
{
    public class RingBuffer<T>
    {
        private T[] _table;

        private int _readPointer;

        private int _writePointer;

        private int _capacity;

        private int _count;

        public T LastElement
        {
            get
            {
                if (_count == 0)
                {
                    throw new InvalidOperationException("Collection is empty.");
                }

                return _table[_readPointer];
            }
        }

        public bool IsEmpty => _count == 0;

        public RingBuffer(int capacity)
        {
            _capacity = capacity;
            _table = new T[_capacity];
            _readPointer = 0;
            _writePointer = 0;
        }

        public void Enqueue(T value)
        {
            _table[_writePointer] = value;
            _writePointer = ++ _writePointer % _capacity;
            _count++;
            if (_writePointer == _readPointer)
            {
                _readPointer = ++_readPointer%_capacity;
            }
        }

        public T Dequeue()
        {
            if (_count == 0)
            {
                throw new InvalidOperationException("Collection is empty.");
            }

            _count--;
            var element = _table[_readPointer];
            _readPointer = ++_readPointer%_capacity;

            return element;
        }

        public void Clear()
        {
            _writePointer = 0;
            _readPointer = 0;
            _count = 0;
            for (var i = 0; i < _table.Length; i++)
            {
                _table[i] = default(T);
            }
        }
    }
}
