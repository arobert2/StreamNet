using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StreamNetServer.Models
{
    public class AudioMetaData : IMediaMetaData
    {
        public Guid Id { get; set; }
        //Description Properties
        [Required]
        [MaxLength(30)]
        public string Title { get; set; }
        [MaxLength(50)]
        public string Subtitle { get; set; }
        public int Rating { get; set; }
        [MaxLength(255)]
        public string Comments { get; set; }

        //Media Properties
        [MaxLength(255)]
        public string ContributingArtists { get; set; }
        [MaxLength(30)]
        public string AlbumArtist { get; set; }
        [MaxLength(50)]
        public string Album { get; set; }
        public DateTime Year { get; set; }
        [MaxLength(25)]
        public string Genre { get; set; }
        public TimeSpan Length { get; set; }

        //File Info Properties
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        [MaxLength(255)]
        public string FolderPath { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public double Size { get; set; }
    }
}
