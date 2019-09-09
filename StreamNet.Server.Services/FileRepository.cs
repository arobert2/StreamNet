using StreamNet.DomainEntities.Data;
using StreamNet.DomainEntities.Entities;
using StreamNet.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;

namespace StreamNet.Server.Services
{
    public class FileRepository
    {
        private Dictionary<Guid, byte[]> _cachedVideos { get; set; }
        private Dictionary<Guid, Timer> _cacheTimeout { get; set; }
        private readonly FileStoreOptions _fileStoreOptions;
        private readonly VideoRepositoryOptions _videoRepositoryOptions;

        public FileRepository(FileStoreOptions fso, VideoRepositoryOptions vro)
        {
            _fileStoreOptions = fso;
            _videoRepositoryOptions = vro;
            _cachedVideos = new Dictionary<Guid, byte[]>();
            _cacheTimeout = new Dictionary<Guid, Timer>();
        }

        public byte[] GetVideo(VideoMetaData vmd)
        {
            var path = Path.Combine(_fileStoreOptions.VideoPath, vmd.Id.ToString(), vmd.FileName);
            if(_cachedVideos.Keys.Contains(vmd.Id))
            {
                _cacheTimeout[vmd.Id] = new Timer(_videoRepositoryOptions.CacheTimeout);
                _cacheTimeout[vmd.Id].Elapsed += (o, args) => ElapseEvent(vmd.Id);
                _cacheTimeout[vmd.Id].Start();
                return _cachedVideos[vmd.Id];
            }
            else
            {
                var newFile = GetNewFile(path);
                _cachedVideos.Add(vmd.Id, newFile);
                _cacheTimeout.Add(vmd.Id, new Timer(_videoRepositoryOptions.CacheTimeout));
                _cacheTimeout[vmd.Id].Elapsed += (o, args) => ElapseEvent(vmd.Id);
                _cacheTimeout[vmd.Id].Start();
                return newFile;
            }
        }

        private byte[] GetNewFile(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var memstream = new MemoryStream();
                fs.CopyTo(memstream);
                return memstream.ToArray();
            }
        } 
        
        private void ElapseEvent(Guid id)
        {
            _cachedVideos.Remove(id);
            _cacheTimeout.Remove(id);
        }
    }
}
