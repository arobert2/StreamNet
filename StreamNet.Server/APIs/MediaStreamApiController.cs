using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StreamNet.DomainEntities.Data;
using StreamNet.Server.Models;
using StreamNet.Server.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StreamNet.Server.APIs
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class MediaStreamApiController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly StreamCache _streamCache;

        public MediaStreamApiController(
            ApplicationDbContext dbContext,
            StreamCache streamCache)
        {
            _dbContext = dbContext;
            _streamCache = streamCache;
        }
        [HttpGet("Play/{id}")]
        public FileStreamResult GetVideoStream(Guid id)
        {
            var videoinfo = _dbContext.Videos.FirstOrDefault(v => v.Id == id);
            var video = _streamCache.GetVideo(videoinfo);
            return new FileStreamResult(new MemoryStream(video),videoinfo.MediaType);
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
        [HttpGet("Video/Test")]
        public IActionResult Test()
        {
            return Ok("This is a test");
        }
    }
}