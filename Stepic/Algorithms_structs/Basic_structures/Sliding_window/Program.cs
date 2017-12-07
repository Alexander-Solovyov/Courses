using System;
using System.Linq;

// For this task first int is value, second one is maximum on stack
using Entry = System.Tuple<int, int>;

namespace Sliding_window
{
    class Program
    {
        private class Stack<T>
        {
            public Stack()
            {
                _top = null;
            }

            public void Push(T item)
            {
                _top = new Node(item, _top);
            }

            public T Pop()
            {
                if (_top == null) return default(T);
                var rv = _top.Data;
                _top = _top.Next;
                return rv;
            }

            public T Peek()
            {
                return _top == null ? default(T) : _top.Data;
            }

            public bool Empty() => _top == null;

            private class Node
            {
                public Node(T data, Node next = null)
                {
                    Data = data;
                    Next = next;
                }
                public readonly T Data;
                public readonly Node Next;
            }

            private Node _top;
        }
        
        // Instances of this class should be initialized with
        // array, which capacity is at least n.
        // It will store not the values but the
        private class QueueOnStack
        {
            public QueueOnStack(int n, int[] array)
            {
                for (var i = (n - 1); i >= 0; --i)
                {
                    PushMaximum(_stackOut, array[i]);
                }
            }

            public int Maximum()
            {
                if (_stackOut.Empty())
                {
                    return _stackIn.Peek().Item2;
                }
                else if (_stackIn.Empty())
                {
                    return _stackOut.Peek().Item2;
                }
                else
                {
                    return Math.Max(_stackIn.Peek().Item2, _stackOut.Peek().Item2);
                }
            }

            public void Enqueue(int value)
            {
                if (_stackOut.Empty())
                {
                    while (!_stackIn.Empty())
                    {
                        PushMaximum(_stackOut, _stackIn.Pop().Item1);
                    }
                }

                PushMaximum(_stackIn, value);
                _stackOut.Pop();
            }

            private static void PushMaximum(Stack<Entry> stack, int value)
            {
                stack.Push(stack.Empty()
                    ? new Entry(value, value)
                    : new Entry(value, Math.Max(stack.Peek().Item2, value)));
            }

            private Stack<Entry> _stackIn = new Stack<Entry>();
            private Stack<Entry> _stackOut = new Stack<Entry>();
        }

        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var numbers = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            var m = int.Parse(Console.ReadLine());

            QueueOnStack q = new QueueOnStack(m, numbers);
            
            Console.Write("{0}", q.Maximum());

            for (var j = m; j < n; ++j)
            {
                q.Enqueue(numbers[j]);
                Console.Write(" {0}", q.Maximum());
            }
        }
    }
}
