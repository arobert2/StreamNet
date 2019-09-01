using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StreamNet.Server.DomainEntities.Entities
{
    public enum ParentalRating { G, PG, PG13, R, NR }
    public class VideoMetaData : IMediaMetaData
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Artists { get; set; }
        public string Genre { get; set; }
        public string Series { get; set; }
        public int EpisodeNumber { get; set; }
        [Required]
        public string FileName { get; set; }
        public byte[] CoverArt { get; set; }
        public TimeSpan Length { get; set; }
        [Required]
        public string MediaType { get; set; }
        public ParentalRating ParentalRating { get; set; }
        public bool AvailableToUsers { get; set; }
    }
}
