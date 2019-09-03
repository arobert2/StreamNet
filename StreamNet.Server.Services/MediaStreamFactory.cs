using StreamNet.ExtensionMethod;
using StreamNet.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StreamNet.Server.Services
{
    public class MediaStreamFactory
    {
        private readonly FileStoreOptions _fileStoreOptions;

        public MediaStreamFactory(FileStoreOptions options)
        {
            _fileStoreOptions = options;
        }
        /// <summary>
        /// Create a video Read Stream
        /// </summary>
        /// <param name="folder">GUID that video refrences</param>
        /// <param name="filename">Name of file</param>
        /// <returns>FileStream that reads.</returns>
        public MediaReader CreateVideoReadStream(Guid folder, string filename)
        {
            var path = Path.Combine(_fileStoreOptions.VideoPath, folder.ToString());
            path = Path.Combine(path, filename);
            return new MediaReader(path);
        }
        /// <summary>
        /// Create a dump file read stream
        /// </summary>
        /// <param name="filename">name of file</param>
        /// <returns>A dump file read stream.</returns>
        public MediaReader CreateDumpReadStream(string filename)
        {
            return new MediaReader(filename);
        }
        /// <summary>
        /// Delete a file. This doesn't delete database records.
        /// </summary>
        /// <param name="mediastream"></param>
        /// <returns></returns>
        public bool DeleteFile(IMediaStream mediastream)
        {
            var path = mediastream.Path;
            mediastream.Dispose();
            File.Delete(path);
            return !File.Exists(path);
        }

        public MediaWriter CreateVideoWriter(Guid folder, string filename)
        {
            var fullpath = Path.Combine(_fileStoreOptions.VideoPath, folder.ToString());
            var fullfilename = Path.Combine(fullpath, filename);
            if (!Directory.Exists(fullpath))
                Directory.CreateDirectory(fullpath);
            var mediaWriter = new MediaWriter(fullfilename);
            return mediaWriter;
        }
    }
}
