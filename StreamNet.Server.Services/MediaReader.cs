using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StreamNet.Server.Services
{
    public class MediaReader : IMediaStream
    {
        public string Path { get; set; }
        //public MemoryStream MemoryStream { get; private set; }

        public MediaReader(string filePath)
        {
            Path = filePath;

        }

        public MemoryStream ReadMedia()
        {
            if (!File.Exists(Path))
                throw new FileNotFoundException("File was not found at path");
            using (var _fileStream = new NetworkFileStream(Path, FileMode.Open, FileAccess.Read))
            {
                using (var _memoryStream = new MemoryStream())
                {
                    _fileStream.CopyTo(_memoryStream);
                    return _memoryStream;
                }
            }
        }

        public void Dispose()
        {
            //MemoryStream.Dispose();
        }
    }
}
