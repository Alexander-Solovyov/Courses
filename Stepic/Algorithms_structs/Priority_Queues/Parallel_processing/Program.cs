using System;
using System.Collections.Generic;
using System.Linq;

namespace Parallel_processing
{
    class Program
    {
        private class Pair : IComparable<Pair>
        {
            public Pair(uint index, ulong time = 0)
            {
                Index = index;
                Time = time;
            }

            public int CompareTo(Pair other)
            {
                return Time == other.Time ? Index.CompareTo(other.Index) : Time.CompareTo(other.Time);
            }

            public uint Index { get; }
            public ulong Time { get; }
        }
        
        private class Heap<T> where T : IComparable<T>
        {
            public Heap()
            {
                _elements = new List<T>();
            }

            private void Swap(int i, int j)
            {
                T tmp = _elements[i];
                _elements[i] = _elements[j];
                _elements[j] = tmp;
            }

            private void SiftDown(int index)
            {
                ++index;
                while (2 * index <= Count())
                {
                    if (2 * index < Count() && 
                        _elements[2 * index].CompareTo(_elements[2 * index - 1]) < 0 && 
                        _elements[2 * index].CompareTo(_elements[index - 1]) < 0)
                    {
                        Swap(index - 1, 2 * index);
                        index = 2 * index + 1;
                    }
                    else if (_elements[index - 1].CompareTo(_elements[2 * index - 1]) > 0)
                    {
                        Swap(index - 1, 2 * index - 1);
                        index *= 2;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            private void SiftUp(int index)
            {
                ++index;
                while (index != 1 && _elements[index - 1].CompareTo(_elements[index / 2 - 1]) < 0)
                {
                    Swap(index - 1, index / 2 - 1);
                    index /= 2;
                }
            }

            public void Push(T value)
            {
                _elements.Add(value);
                SiftUp(Count() - 1);
            }

            public T Pop()
            {
                if (Empty())
                {
                    return default(T);
                }

                var value = _elements.First();
                Swap(0, Count() - 1);
                _elements.RemoveAt(Count() - 1);
                SiftDown(0);
                return value;
            }

            public T Peek() => Empty() ? default(T) : _elements.First();

            public int Count() => _elements.Count;
            public bool Empty() => _elements.Count == 0;

            private readonly List<T> _elements;
        }

        static void Main(string[] args)
        {
            var procNumber = Console.ReadLine().Split(' ').Select(int.Parse).First();
            var processingTime = Console.ReadLine().Split(' ').Select(ulong.Parse).ToArray();
            var heap = new Heap<Pair>();

            for (uint i = 0; i < procNumber; ++i)
            {
                heap.Push(new Pair(i));
            }

            foreach (var time in processingTime)
            {
                var p = heap.Pop();
                Console.WriteLine("{0} {1}", p.Index, p.Time);
                heap.Push(new Pair(p.Index, p.Time + time));
            }
        }
    }
}
