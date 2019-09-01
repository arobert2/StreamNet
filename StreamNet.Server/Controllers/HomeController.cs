﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StreamNet.Server.DomainEntities.Data;

namespace StreamNet.Server.Controllers
{
    [Authorize(Roles = "user, administrator")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var genres = _dbContext.Genres;
            return View(genres);
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}