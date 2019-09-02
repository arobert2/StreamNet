using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace StreamNet.Server.ExtensionMethod
{ 
    public static class FileTypeExtensionMethods
    {
        private static Dictionary<string, string> _mimeTypo = new Dictionary<string, string>{
            {".flv", "video/x-flv" },
            {".mp4", "video/mp4" },
            {".m3u8","application/x-mpegURL" },
            {".ts","video/MP2T" },
            {".3gp","video/3gpp" },
            {".mov", "video/quicktime" },
            {".avi", "video/x-msvideo" },
            {".wmv", "video/x-ms-wmv" },
            {".mp3", "audio/mpeg" }
        };

        public static bool CompatibilityCheck(this string filepath)
        {
            var ext = Path.GetExtension(filepath);
            return _mimeTypo.Keys.Contains(ext);
        }

        public static string GetMimeTypeFromExtension(this string filepath)
        {
            var ext = Path.GetExtension(filepath);
            return _mimeTypo[ext];
        }

        public static string ReplaceUserProfileVariable(this string path)
        {
            var newpath = path.ToLower().Replace("%userprofile%", Environment.GetEnvironmentVariable("USERPROFILE"));
            return newpath;

        }

        public static IEnumerable<string> GetFileNames(this IEnumerable<string> paths)
        {
            List<string> filenames = new List<string>();
            foreach (var p in paths)
                filenames.Add(Path.GetFileName(p));
            return filenames.ToArray();
        }

        public static string GetMd5(this string path)
        {
            var md5data = new byte[0];
            using (var md5 = MD5.Create())
            {
                using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    md5data = md5.ComputeHash(fs);
                }
            }
            return BitConverter.ToString(md5data).Replace("-", "").ToLowerInvariant();
        }
    }
}
