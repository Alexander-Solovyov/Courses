using System;
using System.Collections.Generic;
using System.Linq;

namespace HashingWithChains
{
    class Program
    {
        private class Set<TKey> where TKey : IComparable<TKey>
        {
            public Set(Func<TKey, long> hashFunction,
                int modulus = 37633) // Picked random prime number
            {
                if (modulus <= 0) throw new ArgumentOutOfRangeException(nameof(modulus));
                _table = new LinkedList<TKey>[modulus];
                for (var i = 0; i < modulus; ++i)
                {
                    _table[i] = new LinkedList<TKey>();
                }
                _hashFunction = hashFunction;
                _modulus = modulus;
            }

            public void Add(TKey key)
            {
                if (Find(key)) return;
                var hash = _hashFunction(key) % _modulus;
                _table[hash].AddFirst(key);
            }

            public void Remove(TKey key)
            {
                var iterator = FindNode(key);
                iterator?.List.Remove(iterator);
            }

            public bool Find(TKey key) => FindNode(key) != null;

            public LinkedList<TKey> this[int index] => _table[index];

            // Every other function needs to find value 
            private LinkedListNode<TKey> FindNode(TKey key)
            {
                var hash = _hashFunction(key) % _modulus;
                var iterator = _table[hash].First;
                for (; iterator != null; iterator = iterator.Next)
                {
                    if (iterator.Value.CompareTo(key) == 0)
                    {
                        break;
                    }
                }

                return iterator;
            }

            private readonly LinkedList<TKey>[] _table;
            private readonly Func<TKey, long> _hashFunction;
            private readonly int _modulus;
        }

        static void Main()
        {
            const long x = 263;
            const long p = 1000000007;
            Func<string, long> hashFunction = str =>
            {
                long multiplier = 1;
                long accumulator = 0;
                foreach (var symbol in str)
                {
                    accumulator = (accumulator + symbol * multiplier) % p;
                    multiplier = multiplier * x % p;
                }
                return accumulator;
            };
            var set = new Set<string>(hashFunction, int.Parse(Console.ReadLine()));

            var n = int.Parse(Console.ReadLine());
            while (n-- > 0)
            {
                var input = Console.ReadLine().Split(' ');

                if (input[0] == "add")
                {
                    set.Add(input[1]);
                }
                else if (input[0] == "del")
                {
                    set.Remove(input[1]);
                }
                else if (input[0] == "find")
                {
                    Console.WriteLine(set.Find(input[1]) ? "yes" : "no");
                }
                else // input[0] == "check"
                {
                    Console.WriteLine(string.Join(" ", set[int.Parse(input[1])].ToArray()));
                }
            }
        }
    }
}
