using System;
using System.Linq;

namespace Table_join
{
    class Program
    {
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
            var m = int.Parse(Console.ReadLine().Split(' ')[1]);
            var records = Console.ReadLine().Split(' ').Select(long.Parse).ToArray();
            var maximum = records.Max();
            var set = new DisjointSet(records.Length);

            while (m > 0)
            {
                --m;
                var to = 0;
                var from = 0;

                {
                    var toFrom = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                    to = toFrom[0] - 1;
                    from = toFrom[1] - 1;
                }
                to = set.Find(to);
                from = set.Find(from);
                var recordsNumber = from == to ? records[from] : records[from] + records[to];
                maximum = Math.Max(maximum, recordsNumber);
                Console.WriteLine("{0}", maximum);

                set.Unite(to, from);
                records[set.Find(to)] = recordsNumber;
            }
        }
    }
}
