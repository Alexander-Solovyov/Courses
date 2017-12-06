using System;

using Entry = System.Tuple<char, int>;

namespace Brackets
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
            var bracketStack = new Stack<Entry>();
            var brackets = Console.ReadLine();

            for (var i = 0; i < brackets.Length; ++i)
            {
                var current = brackets[i];
                if (current == '(' || current == '[' || current == '{')
                {
                    bracketStack.Push(new Entry(current, i));
                }
                else if (current == ')' || current == ']' || current == '}')
                {
                    char? top = null;
                    if (!bracketStack.Empty())
                    {
                        top = bracketStack.Pop().Item1;
                    }
                    if (top == '(' && current == ')' || top == '[' && current == ']' ||
                        top == '{' && current == '}') continue;
                    Console.WriteLine("{0}", i + 1);
                    return;
                }
            }

            if (bracketStack.Empty())
            {
                Console.WriteLine("Success");
            }
            else
            {
                Console.WriteLine("{0}", bracketStack.Pop().Item2 + 1);
            }
        }
    }
}
