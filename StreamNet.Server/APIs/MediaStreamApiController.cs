using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StreamNet.Server.DomainEntities.Data;
using StreamNet.Server.Models;
using StreamNet.Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamNet.Server.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "user, administrator")]
    public class MediaStreamApiController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly MediaStreamFactory _mediaReaderFactory;

        public MediaStreamApiController(
            ApplicationDbContext dbContext,
            MediaStreamFactory mediaReaderFactory)
        {
            _mediaReaderFactory = mediaReaderFactory;
            _dbContext = dbContext;
        }
        [HttpGet("Video/{id}")]
        public async Task<FileStreamResult> GetVideoStream(Guid id)
        {
            var videoinfo = _dbContext.Videos.FirstOrDefault(v => v.Id == id);
            var mediaReader = _mediaReaderFactory.CreateVideoReadStream(videoinfo.Id,videoinfo.FileName);
            await mediaReader.ReadMedia();
            var mediaStream = mediaReader.MemoryStream;
            return new FileStreamResult(mediaReader.MemoryStream, videoinfo.MediaType);
        }
        [HttpGet("Videos/{genre}")]
        public IActionResult GetVideosByGenre(string genre)
        {
            var videos = _dbContext.Videos.Where(v => v.Genre.Contains(genre));
            if (videos == null)
                return NotFound();
            var videoViewModels = Mapper.Map<IEnumerable<MediaReadViewModel>>(videos);
            return Ok(videoViewModels);
        }
    }
}