using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StreamNet.DomainEntities.Data;
using StreamNet.DomainEntities.Entities;
using StreamNet.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace StreamNet.Server.CMS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StreamContent : ControllerBase
    {
        private ApplicationDbContext _dbContext;
        private FileStoreOptions _fileStore;

        public StreamContent(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _fileStore = OptionsFactory.GetFileStoreOptions();
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public HttpResponseMessage Get(Guid id)
        {
            var vid = _dbContext.Videos.FirstOrDefault(e => e.Id == id);
            var filePath = Path.Combine(_fileStore.VideoPath, vid.FileName);
            var httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
            var streamContentWriter = new StreamContentWriter(filePath);
            httpResponse.Content = new PushStreamContent(streamContentWriter.WriteContent);
            return httpResponse;
        }

        private class StreamContentWriter
        {
            private string _videoPath;

            public StreamContentWriter(string path)
            {
                _videoPath = path;
            }

            public async void WriteContent(Stream stream, HttpContent httpResponse, TransportContext transportContext)
            {
                int bufferSize = 1000;
                byte[] buffer = new byte[bufferSize];
                using(var fs = new FileStream(_videoPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    int totalSize = (int)fs.Length;
                    while (totalSize > 0)
                    {
                        int count = totalSize > bufferSize ? bufferSize : totalSize;
                        int sizeOfReadedBuffer = fs.Read(buffer, 0, count); 
                        await stream.WriteAsync(buffer, 0, sizeOfReadedBuffer);  
                        totalSize -= sizeOfReadedBuffer;
                    }
                }
            }
        }
    }
}
