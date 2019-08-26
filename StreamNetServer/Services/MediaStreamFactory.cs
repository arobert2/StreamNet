using StreamNetServer.Services.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StreamNetServer.Services
{
    public class MediaStreamFactory
    {
        private readonly FileStoreOptions _fileStoreOptions;

        public MediaStreamFactory(FileStoreOptions options)
        {
            _fileStoreOptions = options;
        }

        public MediaReader CreateVideoReadStream(Guid folder, string filename)
        {
            var path = Path.Combine(_fileStoreOptions.VideoPath, folder.ToString());
            path = Path.Combine(path, filename);
            return new MediaReader(path);
        }

        public MediaReader CreateAudioReadStream(Guid folder, string filename)
        {
            var path = Path.Combine(_fileStoreOptions.AudioPath, folder.ToString());
            path = Path.Combine(path, filename);
            return new MediaReader(path);
        }

        public MediaReader CreateDumpReadStream(string path)
        {
            return new MediaReader(path);
        }

        public MediaWriter CreateAudioriterStream(Guid folder, string filename)
        {
            var path = Path.Combine(_fileStoreOptions.AudioPath, folder.ToString());
            path = Path.Combine(path, filename);
            return new MediaWriter(path);
        }
    }
}
