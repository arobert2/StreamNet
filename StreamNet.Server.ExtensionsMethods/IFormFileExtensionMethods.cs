using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StreamNet.ExtensionsMethods
{
    public static class IFormFileExtensionMethods
    {
        public static byte[] ToByteArray(this IFormFile file)
        {
            using(MemoryStream ms = new MemoryStream())
            {
                file.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
