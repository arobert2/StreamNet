using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StreamNet.Server.Services
{
    public class MediaWriter : IDisposable
    {
        private FileStream _fileStream;
        public string Path { get; set; }
        public MemoryStream MemoryStream { get; private set; }

        public MediaWriter(string filePath)
        {
            Path = filePath;
        }

        public async Task WriteMedia(Stream stream)
        {
            if (!File.Exists(Path))
                throw new FileNotFoundException("File was already found on server.");
            _fileStream = new FileStream(Path, FileMode.Create, FileAccess.Write);
            await stream.CopyToAsync(_fileStream);
        }

        ~MediaWriter()
        {
            Dispose();
        }

        public void Dispose()
        {
            _fileStream.Dispose();
            MemoryStream.Dispose();
        }
    }
}
