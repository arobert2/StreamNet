using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StreamNetServer.Data;
using StreamNetServer.Models;

namespace StreamNetServer.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public MoviesController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var movies = _dbContext.Videos;
            var videoViewModel = Mapper.Map<IEnumerable<VideoMetaDataViewModel>>(movies);
            return View(videoViewModel);
        }
    }
}