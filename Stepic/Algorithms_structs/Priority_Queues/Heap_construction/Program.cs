using System;
using System.Linq;
using Pair = System.Tuple<int, int>;
using List = System.Collections.Generic.List<System.Tuple<int, int>>;

namespace Heap_construction
{
    class Program
    {
        private static void Swap(ref int lhs, ref int rhs)
        {
            var tmp = rhs;
            rhs = lhs;
            lhs = tmp;
        }

        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var array = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            var pairs = new List();

            for (var i = n / 2; i > 0; --i)
            {
                var j = i;
                while (2 * j <= n)
                {
                    if (2 * j < n && array[2 * j] < array[2 * j - 1] && array[2 * j] < array[j - 1])
                    {
                        Swap(ref array[j - 1], ref array[2 * j]);
                        pairs.Add(new Pair(j - 1, 2 * j));
                        j = 2 * j + 1;
                    }
                    else if (array[j - 1] > array[2 * j - 1])
                    {
                        Swap(ref array[j - 1], ref array[2 * j - 1]);
                        pairs.Add(new Pair(j - 1, 2 * j - 1));
                        j = 2 * j;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            Console.WriteLine("{0}", pairs.Count);
            foreach (var pair in pairs)
            {
                Console.WriteLine("{0} {1}", pair.Item1, pair.Item2);
            }
        }
    }
}
