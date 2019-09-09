using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StreamNet.DomainEntities.Data;
using StreamNet.DomainEntities.Entities;
using StreamNet.ExtensionsMethods;
using StreamNet.Server.Models;

namespace StreamNet.Server.Controllers
{
    [Authorize(Roles = "administrator")]
    public class MovieDatabaseController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public MovieDatabaseController(
            ApplicationDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var movies = _dbContext.Videos;
            var videoViewModel = Mapper.Map<IEnumerable<EditVideoMetaDataViewModel>>(movies);
            return View(videoViewModel);
        }

        [HttpGet]
        public IActionResult UpdateVideoContent(Guid id)
        {
            var videoMetaData = _dbContext.Videos.FirstOrDefault(v => v.Id == id);
            if (videoMetaData == null)
                return NotFound();
            var videoMetaDataViewModel = Mapper.Map<EditVideoMetaDataViewModel>(videoMetaData);
            return View(videoMetaDataViewModel);
        }

        [HttpPost]
        public IActionResult UpdateVideoContent([FromForm] EditVideoMetaDataViewModel vmdviewmodel)
        {
            if (!ModelState.IsValid)
                return View(vmdviewmodel);
            var videometadatafromentity = _dbContext.Videos.FirstOrDefault(v => v.Id == vmdviewmodel.Id);
            var updatedatedata = Mapper.Map(vmdviewmodel, videometadatafromentity, typeof(EditVideoMetaDataViewModel), typeof(VideoMetaData));
            _dbContext.Update(videometadatafromentity);
            if (_dbContext.SaveChanges() < 0)
                throw new Exception("Failed to save to database!");
            return RedirectToAction(nameof(Index), "MovieDatabase");
        }
        [HttpGet]
        public IActionResult ChangeCoverArt(Guid id)
        {
            var vmdEntity = _dbContext.Videos.FirstOrDefault(v => v.Id == id);
            var ccam = Mapper.Map<ChangeCoverArtModel>(vmdEntity);
            return View(ccam);
        }
        [HttpPost]
        public IActionResult ChangeCoverArt([FromForm] ChangeCoverArtModel ccam)
        {
            if (!ModelState.IsValid)
                return View(ccam);
            var vmdEntity = _dbContext.Videos.FirstOrDefault(v => v.Id == ccam.Id);
            var updatedEntity = Mapper.Map(ccam, vmdEntity, typeof(ChangeCoverArtModel), typeof(VideoMetaData));
            _dbContext.Update(vmdEntity);
            if (_dbContext.SaveChanges() < 0)
                throw new Exception("Failed to save database!");
            return RedirectToAction(nameof(Index));
        }
    }
}