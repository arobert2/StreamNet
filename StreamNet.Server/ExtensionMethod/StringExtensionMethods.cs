using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamNet.Server.ExtensionMethod
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
    }
}
