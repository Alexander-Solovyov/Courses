using System;
using System.Linq;
using gen = System.Collections.Generic;

namespace Tree_height
{
    class Program
    {
        private class Node
        {
            public Node()
            {
                _children = new gen.List<Node>();
            }

            public void addChildren(Node node)
            {
                _children.Add(node);
            }

            public uint Height()
            {
                if (_height == 0)
                {
                    _height = _children.Count == 0 ? 1 : _children.Select(t => t.Height()).Max() + 1;
                }
                return _height;
            }

            private uint _height;
            private readonly gen.List<Node> _children;
        }

        static void Main(string[] args)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            var n = int.Parse(Console.ReadLine());
            var nodesParents = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            var nodes = (new Node[n]).Select(t => new Node()).ToArray();
            Node root = null;
            for (var i = 0; i < n; ++i)
            {
                if (nodesParents[i] == -1)
                {
                    root = nodes[i];
                }
                else
                {
                    nodes[nodesParents[i]].addChildren(nodes[i]);
                }
            }

            Console.WriteLine("{0}", root.Height());
        }
    }
}
