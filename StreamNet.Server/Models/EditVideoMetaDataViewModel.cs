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
        public int EpisodeNumber { get; set; }
        public string CoverArtBase64 { get; set; }
        public ParentalRating ParentalRating { get; set; }
        public bool AvailableToUsers { get; set; }
        public IFormFile CoverArtFile { get; set; }

    }
}
