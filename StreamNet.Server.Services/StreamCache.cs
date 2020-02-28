using StreamNet.DomainEntities.Data;
using StreamNet.DomainEntities.Entities;
using StreamNet.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;

namespace StreamNet.Server.Services
{
    public class StreamCache
    {
        private ConcurrentDictionary<Guid, byte[]> _cachedVideos { get; set; }      //Cached Video content.
        private ConcurrentDictionary<Guid, Timer> _cacheTimeout { get; set; }                 //Cached timers
        private readonly FileStoreOptions _fileStoreOptions;                        //File reading options
        private readonly VideoRepositoryOptions _videoRepositoryOptions;            //Video repostiroy settings

        /// <summary>
        /// Caching system for streaming video files.
        /// </summary>
        /// <param name="fso">FileStoreOptions</param>
        /// <param name="vro">VideoRepositoryOptions</param>
        public StreamCache(FileStoreOptions fso, VideoRepositoryOptions vro)
        {
            _fileStoreOptions = fso;
            _videoRepositoryOptions = vro;
            _cachedVideos = new ConcurrentDictionary<Guid, byte[]>();
            _cacheTimeout = new ConcurrentDictionary<Guid, Timer>();
        }

        public byte[] GetVideo(VideoMetaData vmd)
        {
            //Path to file
            var path = Path.Combine(_fileStoreOptions.VideoPath, vmd.Id.ToString(), vmd.FileName);
            //Add or get file from cache
            var videoFile = _cachedVideos.GetOrAdd(vmd.Id, GetNewFile(path));
            //Update or add timer.
            _cacheTimeout.AddOrUpdate(vmd.Id, new Timer(_videoRepositoryOptions.CacheTimeout), (timerKey, timer) =>
            {
                timer = new Timer(_videoRepositoryOptions.CacheTimeout);
                timer.Elapsed += (o, args) => ElapseEvent(vmd.Id);
                timer.Start();
                return timer;
            });

            //return video byte[]
            return videoFile;


            /*
            var path = Path.Combine(_fileStoreOptions.VideoPath, vmd.Id.ToString(), vmd.FileName);
            var newFile = GetNewFile(path);
            try
            {
                
                _cachedVideos.Add(vmd.Id, newFile);
                _cacheTimeout.Add(vmd.Id, new Timer(_videoRepositoryOptions.CacheTimeout));
                _cacheTimeout[vmd.Id].Elapsed += (o, args) => ElapseEvent(vmd.Id);
                _cacheTimeout[vmd.Id].Start();
                return newFile;

            }
            catch(ArgumentException e)
            {

            }
            finally
            {

            }
            if (_cachedVideos.Keys.Contains(vmd.Id))
            {
                _cacheTimeout[vmd.Id] = new Timer(_videoRepositoryOptions.CacheTimeout);
                _cacheTimeout[vmd.Id].Elapsed += (o, args) => ElapseEvent(vmd.Id);
                _cacheTimeout[vmd.Id].Start();
                return _cachedVideos[vmd.Id];
            }
            else
            {
                
             
                    
                _cacheTimeout.Add(vmd.Id, new Timer(_videoRepositoryOptions.CacheTimeout));
                _cacheTimeout[vmd.Id].Elapsed += (o, args) => ElapseEvent(vmd.Id);
                _cacheTimeout[vmd.Id].Start();
                return newFile;
            }*/
        }
        /// <summary>
        /// Load video file into memory
        /// </summary>
        /// <param name="path">path to file.</param>
        /// <returns>Byte array of file data.</returns>
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
            _cachedVideos.Remove<Guid, byte[]>(id, out var myByteArray);
            _cacheTimeout[id].Stop();
        }
    }
}
