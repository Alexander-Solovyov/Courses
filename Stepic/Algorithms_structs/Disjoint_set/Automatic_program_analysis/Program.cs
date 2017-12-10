using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Automatic_program_analysis
{
    class Program
    {
        // Straightforward copy-paste from "Table_join" project
        private class DisjointSet
        {
            public DisjointSet(int n)
            {
                _parents = new int[n];
                _rank = new int[n];

                for (var i = 0; i < n; ++i)
                {
                    _parents[i] = i;
                }
            }

            public void Unite(int x, int y)
            {
                x = Find(x);
                y = Find(y);
                if (x == y)
                {
                    return;
                }

                if (_rank[x] == _rank[y])
                {
                    _parents[x] = y;
                    ++_rank[y];
                }
                else if (_rank[x] > _rank[y])
                {
                    _parents[y] = x;
                }
                else
                {
                    _parents[x] = y;
                }
            }

            public int Find(int x)
            {
                if (_parents[x] != x)
                {
                    _parents[x] = Find(_parents[x]);
                }
                return _parents[x];
            }

            private readonly int[] _parents;
            private int[] _rank; // height of the tree
        }

        static void Main(string[] args)
        {
            var ned = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            var quotientSet = new DisjointSet(ned[0]);

            Func<int[]> readIndices = () =>
                Console.ReadLine().Split(' ').Select(t => int.Parse(t) - 1).ToArray();
            for (; ned[1] > 0; --ned[1])
            {
                var indices = readIndices();
                quotientSet.Unite(indices[0], indices[1]);
            }

            var ans = 1;
            for (; ned[2] > 0; --ned[2])
            {
                var indices = readIndices();
                if (quotientSet.Find(indices[0]) == quotientSet.Find(indices[1]))
                {
                    ans = 0;
                }
            }

            Console.WriteLine("{0}", ans);
        }
    }
}
