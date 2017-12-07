using System;

namespace Stack_with_maximums
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

        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var stack = new Stack<int>();
            for (; n > 0; --n)
            {
                var input = Console.ReadLine().Split(' ');

                if (input[0] == "push")
                {
                    var v = int.Parse(input[1]);
                    if (stack.Empty() || stack.Peek() < v)
                    {
                        stack.Push(v);
                    }
                    else
                    {
                        stack.Push(stack.Peek());
                    }
                }
                else if (input[0] == "pop")
                {
                    stack.Pop();
                }
                else if (input[0] == "max")
                {
                    Console.WriteLine("{0}", stack.Peek());
                }
            }
        }
    }
}
