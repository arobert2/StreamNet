using System;
using System.Collections.Generic;
using System.Text;

namespace StreamNet.Server.ContentScanner
{
    public class UnsupportedFileException : Exception
    {
        public string MediaType { get; set; }
        public string FileName { get; set; }

        public UnsupportedFileException(string filename, string mediaType, string message) : base(message)
        {
            MediaType = mediaType;
            FileName = filename;
        }
    }
}
