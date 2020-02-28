using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StreamNet.DomainEntities.Data;
using StreamNet.Server.Models;
using StreamNet.Server.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Threading.Tasks;

namespace StreamNet.Server.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly StreamCache _fileRepository;
        
        public MoviesController(ApplicationDbContext dbContext, StreamCache fileRepo)
        {
            _dbContext = dbContext;
            _fileRepository = fileRepo;
        }
        public IActionResult VideoPlayer(Guid id)
        {
            var videoInfo = _dbContext.Videos.FirstOrDefault(v => v.Id == id);
            return View(videoInfo);
        }

        [HttpGet]
        public ActionResult Video(Guid id)
        {
            var videoinfo = _dbContext.Videos.FirstOrDefault(v => v.Id == id);
            var byteArray = _fileRepository.GetVideo(videoinfo);
            //var videoPlayerViewModel = new VideoPlayerViewModel();
            //videoPlayerViewModel.VideData = Mapper.Map<MediaReadViewModel>(videoinfo);
            //videoPlayerViewModel.Video = string.Format("data:{0};base64,{1}", "application/octet-stream", Convert.ToBase64String(byteArray));
            //return new FileStreamResult(new MemoryStream(byteArray), "application/octet-stream"); 
            //return View(videoPlayerViewModel);
            return new FileStreamResult(new MemoryStream(byteArray), videoinfo.MediaType);
        }
    }
}