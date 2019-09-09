using Microsoft.AspNetCore.Http;
using StreamNet.DomainEntities.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace StreamNet.Server.Models
{
    public class EditVideoMetaDataViewModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Artists { get; set; }
        public string Genre { get; set; }
        public string Series { get; set; }
        [Display(Name = "Episode Number")]
        public int EpisodeNumber { get; set; }
        public string CoverArtBase64 { get; set; }
        [Display(Name = "Parental Rating")]
        public ParentalRating ParentalRating { get; set; }
        [Display(Name = "Available To  User")]
        public bool AvailableToUsers { get; set; }

    }
}
