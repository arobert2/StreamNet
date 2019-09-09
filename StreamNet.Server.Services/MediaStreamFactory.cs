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
        public static Dictionary<string, MediaReader> StreamCache { get; set; }

        public MediaStreamFactory(FileStoreOptions options)
        {
            _fileStoreOptions = options;
            StreamCache = new Dictionary<string, MediaReader>();
        }
        /// <summary>
        /// Create a video Read Stream
        /// </summary>
        /// <param name="folder">GUID that video refrences</param>
        /// <param name="filename">Name of file</param>
        /// <returns>FileStream that reads.</returns>
        public MediaReader GetReadStream(Guid folder, string filename)
        {
            var path = Path.Combine(_fileStoreOptions.VideoPath, folder.ToString());
            path = Path.Combine(path, filename);
            if (StreamCache.Keys.Contains(filename))
                return StreamCache[filename];
            else
            {
                var mediaReader = new MediaReader(path);
                mediaReader.ReadMedia();
                StreamCache.Add(filename, mediaReader);
                return new MediaReader(path);
            }
        }
        /// <summary>
        /// Create a dump file read stream
        /// </summary>
        /// <param name="filename">name of file</param>
        /// <returns>A dump file read stream.</returns>
        public MediaReader GetDumpReadStream(string filename)
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
