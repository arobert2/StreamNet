using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel.DataAnnotations;

namespace StreamNetServer.Entities
{
    public class AudioMetaData : IMediaMetaData
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Artists { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        public string TrackNumber { get; set; }
        [Required]
        public string FileName { get; set; }    
        public byte[] CoverArt { get; set; }
        public TimeSpan Length { get; set; }
        [Required]
        public string MediaType { get; set; }
        public bool AvailableToUsers { get; set; }
    }
}
