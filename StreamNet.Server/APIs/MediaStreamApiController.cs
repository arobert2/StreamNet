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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "user, administrator")]
    public class MediaStreamApiController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public MediaStreamApiController(
            ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("Video/{id}")]
        public IActionResult GetVideoStream(Guid id)
        {
            var videoinfo = _dbContext.Videos.FirstOrDefault(v => v.Id == id);
            return Ok();//FileStreamResult(mediaReader.MemoryStream, videoinfo.MediaType);
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