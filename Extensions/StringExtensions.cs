using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenJKLoader.Extensions
{
    public static class StringExtensions
    {
        public static IEnumerable<string> SplitByChunks(this string str, int chunkSize)
        {
            if (str.Length < chunkSize)
            {
                return [str];
            }
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }
    }
}
