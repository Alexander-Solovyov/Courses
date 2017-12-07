// Entry consists of two elements: index and time

using System;
using System.Linq;

namespace Package_Processing
{
    class Program
    {
        private class Queue<T>
        {
            public T Peek() => _top != null ? _top.Value : default(T);

            public void Enqueue(T value)
            {
                ++Count;
                _back = new Node(value, _back);
                if (_top == null)
                {
                    _top = _back;
                }
            }

            public T Dequeue()
            {
                if (_top == null) return default(T);
                --Count;
                var rv = _top.Value;
                if (_top == _back)
                {
                    _back = null;
                }
                _top = _top.Prev;
                return rv;
            }

            public bool Empty() => _top == null;

            public int Count { get; private set; } = 0;

            private Node _top = null;
            private Node _back = null;

            private class Node
            {
                public Node(T value, Node prev)
                {
                    if (prev != null)
                    {
                        prev.Prev = this;
                    }
                    Value = value;
                }

                public Node Prev;
                public readonly T Value;
            }
        }

        static void Main(string[] args)
        {
            var size = 0;
            var n = 0;
            {
                var tmp = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                size = tmp[0];
                n = tmp[1];
            }
            if (n == 0)
            {
                return;
            }

            var queue = new Queue<int>();
            var T = 0;
            var answers = new int[n];
            for (uint i = 0; i < n; ++i)
            {
                var times = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                // Remove elements from queue while they end before current package arrival
                while (!queue.Empty() && queue.Peek() <= times[0])
                {
                    queue.Dequeue();
                }

                if (queue.Count == size)
                {
                    answers[i] = -1;
                    continue;
                }

                answers[i] = Math.Max(times[0], T);
                T = answers[i] + times[1];
                queue.Enqueue(T);
            }

            foreach (var a in answers)
            {
                Console.WriteLine("{0}", a);
            }
        }
    }
}
