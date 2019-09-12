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
        [HttpGet]
        public FileStreamResult VideoPlayer(Guid id)
        {
            var videoinfo = _dbContext.Videos.FirstOrDefault(v => v.Id == id);
            var byteArray = _fileRepository.GetVideo(videoinfo);
            return new FileStreamResult(new MemoryStream(byteArray), videoinfo.MediaType);         
        }
    }
}