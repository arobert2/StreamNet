using Microsoft.AspNetCore.Mvc;
using StreamNet.DomainEntities.Entities;
using StreamNet.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamNet.Server.Controllers
{
    public class TestController : Controller
    {
        public IActionResult MovieInfoBox()
        {
            var editMovieViewModel = new EditVideoMetaDataViewModel()
            {
                Artists = "Billy TestData, Sandy Starlet, Unknown Rando",
                Description = "In a world gone action film. Action McGoodMurders must save the day before the bad guy does a super villian.",
                EpisodeNumber = 1,
                Genre = "Action, Romance",
                Id = Guid.NewGuid(),
                AvailableToUsers = false,
                ParentalRating = ParentalRating.R,
                Series = "Action McGoodMurders Wild Good Guy Killing Spree",
                Title = "Action McGoodMurders Murders The Bad Guys!",
                CoverArtBase64 = @"https://memeguy.com/photos/images/had-to-make-a-fake-movie-poster-for-school-50514.png"
            };

            return View(editMovieViewModel);
        }
    }
}
