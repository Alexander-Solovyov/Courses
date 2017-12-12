using System;
using System.Collections.Generic;

namespace PhoneBook
{
    class Program
    {
        private class Map<Key, Value> where Key : IComparable<Key>
        {
            public Map(Func<Key, long> hashFunction,
                Value defaultValue = default(Value),
                int modulus = 37633) // Picked random prime number
            {
                if (modulus <= 0) throw new ArgumentOutOfRangeException(nameof(modulus));
                _table = new LinkedList<Tuple<Key, Value>>[modulus];
                for (var i = 0; i < modulus; ++i)
                {
                    _table[i] = new LinkedList<Tuple<Key, Value>>();
                }
                _hashFunction = hashFunction;
                _modulus = modulus;
                _defaultValue = defaultValue;
            }

            public Value this[Key key]
            {
                get
                {
                    var hash = _hashFunction(key) % _modulus;
                    foreach (var pair in _table[hash])
                    {
                        if (pair.Item1.CompareTo(key) == 0)
                        {
                            return pair.Item2;
                        }
                    }
                    return _defaultValue;
                }

                set
                {
                    var hash = _hashFunction(key) % _modulus;
                    var pair = new Tuple<Key, Value>(key, value);
                    for (var iterator = _table[hash].First; iterator != null; iterator = iterator.Next)
                    {
                        if (iterator.Value.Item1.CompareTo(key) != 0) continue;
                        iterator.Value = pair;
                        return;
                    }
                    _table[hash].AddFirst(pair);
                }
            }

            public void RemoveKey(Key key)
            {
                var hash = _hashFunction(key) % _modulus;
                var iterator = _table[hash].First;
                for (; iterator != null; iterator = iterator.Next)
                {
                    if (iterator.Value.Item1.CompareTo(key) == 0)
                    {
                        break;
                    }
                }

                if (iterator == null)
                {
                    return;
                }

                _table[hash].Remove(iterator);
            }

            private readonly LinkedList<Tuple<Key, Value>>[] _table;
            private readonly Func<Key, long> _hashFunction;
            private readonly int _modulus;
            private readonly Value _defaultValue;
        }

        static void Main(string[] args)
        {
            // Worst hash function ever, yet it should be just enough for the task
            var phoneBook = new Map<int, string>(i => i, "not found");
            var n = int.Parse(Console.ReadLine());
            while (n-- > 0)
            {
                var input = Console.ReadLine().Split(' ');
                var number = int.Parse(input[1]);
                if (input[0] == "add")
                {
                    phoneBook[number] = input[2];
                }
                else if (input[0] == "del")
                {
                    phoneBook.RemoveKey(number);
                }
                else if (input[0] == "find")
                {
                    Console.WriteLine("{0}", phoneBook[number]);
                }
            }
        }
    }
}
