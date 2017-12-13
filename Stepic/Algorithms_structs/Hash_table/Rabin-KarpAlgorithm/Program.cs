using System;
using System.Collections.Generic;

namespace Rabin_KarpAlgorithm
{
    class Program
    {
        private static List<int> RabinKarp(string text, string pattern)
        {
            var indices = new List<int>();
            var m = pattern.Length;
            var n = text.Length - m;
            if (n < 0)
            {
                return indices;
            }

            const long x = 263;
            const long modulus = 1000000007;
            long patternHash = pattern[m - 1];
            long subtextHash = text[m - 1];
            long leadingTerm = 1; // x^(n - 1)

            for (var i = 2; i <= m; ++i)
            {
                leadingTerm = leadingTerm * x % modulus;
                patternHash = (patternHash + pattern[m - i] * leadingTerm) % modulus;
                subtextHash = (subtextHash + text[m - i] * leadingTerm) % modulus;
            }
            
            for (var i = 0; i <= n; ++i)
            {
                if (i > 0)
                {
                    subtextHash = ((modulus + subtextHash - leadingTerm * text[i - 1] % modulus) * x + text[i - 1 + m]) % modulus;
                }

                var equals = patternHash == subtextHash;
                for (var j = 0; equals && j < m; ++j)
                {
                    equals = pattern[j] == text[i + j];
                }
                if (equals)
                {
                    indices.Add(i);
                }
            }

            return indices;
        }

        static void Main(string[] args)
        {
            var pattern = Console.ReadLine();
            var text = Console.ReadLine();
            var indices = RabinKarp(text, pattern);

            foreach (var index in indices)
            {
                Console.Write("{0} ", index);
            }
        }
    }
}
