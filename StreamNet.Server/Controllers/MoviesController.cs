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
        private readonly MediaStreamFactory _mediaReaderFactory;
        private readonly FileRepository _fileRepository;
        
        public MoviesController(ApplicationDbContext dbContext, MediaStreamFactory mediaStreamFactory, FileRepository fileRepo)
        {
            _dbContext = dbContext;
            _mediaReaderFactory = mediaStreamFactory;
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