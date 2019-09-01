using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StreamNet.Server.Services
{
    public class MediaReader : IDisposable
    {
        private FileStream _fileStream;
        public string Path { get; set; }
        public MemoryStream MemoryStream { get; private set; }

        public MediaReader(string filePath)
        {
            Path = filePath;
        }

        public async Task ReadMedia()
        {
            if (!File.Exists(Path))
                throw new FileNotFoundException("File was not found at path");
            _fileStream = new FileStream(Path, FileMode.Open, FileAccess.Read);
            await _fileStream.CopyToAsync(MemoryStream);
        }

        ~MediaReader()
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
