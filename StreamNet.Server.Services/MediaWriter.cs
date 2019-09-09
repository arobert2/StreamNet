using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StreamNet.Server.Services
{
    public class MediaWriter : IMediaStream
    {
        public string Path { get; set; }
        public FileStream FileStream { get; private set; }

        public MediaWriter(string filePath)
        {
            Path = filePath;
        }

        public void WriteMedia(Stream stream)
        {
            if (File.Exists(Path))
                throw new FileNotFoundException("File was already found on server.");
            FileStream = new FileStream(Path, FileMode.Create, FileAccess.Write);
        }

        public void Dispose()
        {
            FileStream.Dispose();
        }
    }
}
