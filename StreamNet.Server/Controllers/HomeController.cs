using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StreamNet.DomainEntities.Data;
using StreamNet.ExtensionMethod;
using StreamNet.Server.Models;
using System.Collections.Generic;
using System.Linq;

namespace StreamNet.Server.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var genres = _dbContext.Videos.Where((g) => g.Genre != null).Select(v => v.Genre).ToArray();
            var seperatedGenres = new List<string>();
            var moviesByGenre = new Dictionary<string, List<MediaReadViewModel>>();

            foreach (var g in genres)
                seperatedGenres.AddRange(g.Split(','));
            genres = seperatedGenres.ToArray().Unique();
            foreach (var g in genres)
            {
                var movg = _dbContext.Videos.Where((v) => v.Genre.Contains(g) && v.AvailableToUsers).ToList();
                moviesByGenre.Add(g, Mapper.Map<List<MediaReadViewModel>>(movg));
            }
            return View(moviesByGenre);
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
