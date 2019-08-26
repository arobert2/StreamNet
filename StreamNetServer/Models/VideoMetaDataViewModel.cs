using StreamNetServer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StreamNetServer.Models
{
    public class VideoMetaDataViewModel
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
        public byte[] CoverArt { get; set; }
        public ParentalRating ParentalRating { get; set; }
        public bool AvailableToUsers { get; set; }
    }
}
