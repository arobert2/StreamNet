using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StreamNet.DomainEntities.Data;
using StreamNet.Server.Models;
using StreamNet.Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamNet.Server.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly MediaStreamFactory _mediaReaderFactory;
        
        public MoviesController(ApplicationDbContext dbContext, MediaStreamFactory mediaStreamFactory)
        {
            _dbContext = dbContext;
            _mediaReaderFactory = mediaStreamFactory;
        }
        [Authorize(Roles = "administrator"]
        [HttpGet]
        public IActionResult Index()
        {
            var movies = _dbContext.Videos;
            var videoViewModel = Mapper.Map<IEnumerable<EditVideoMetaDataViewModel>>(movies);
            return View(videoViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> VideoPlayer(Guid id)
        {
            var videoinfo = _dbContext.Videos.FirstOrDefault(v => v.Id == id);
            var mediaReader = _mediaReaderFactory.CreateVideoReadStream(videoinfo.Id, videoinfo.FileName);
            await mediaReader.ReadMedia();
            var mediaStream = mediaReader.MemoryStream;
            return new FileStreamResult(mediaReader.MemoryStream, videoinfo.MediaType);
        }
    }
}