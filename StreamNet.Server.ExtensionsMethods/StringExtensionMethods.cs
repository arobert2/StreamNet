using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamNet.ExtensionMethod
{
    public static class StringExtensionMethods
    {
        public static string ConcatArray(this string[] a)
        {
            var sb = new StringBuilder();
            foreach (var s in a)
                sb.Append(s).Append(", ");
            return sb.ToString();
        }

        public static string[] Unique(this string[] stringArray)
        {
            List<string> unique = new List<string>();
            foreach (var s in stringArray)
                if (!unique.Contains(s))
                    unique.Add(s);
            return unique.ToArray();
        }

        public static string[] DelimiterSplit(this string liststring, char delimiter)
        {
            return liststring.Split(delimiter);
        }
    }
}
