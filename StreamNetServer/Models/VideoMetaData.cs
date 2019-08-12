using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StreamNetServer.Models
{
    public enum ParentalRating { G, PG, PG13, R, NR }
    public class VideoMetaData : IMediaMetaData
    {        
        public Guid Id { get; set; }
        //Media Description Properties
        [Required]
        [MaxLength(30)]
        public string Title { get; set; }
        [MaxLength(50)]
        public string Subtitle { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
        
        //Video Properties
        public TimeSpan Length { get; set; }
        public int FrameWidth { get; set; }
        public int FrameHeight { get; set; }
        
        //Media Properties
        public string ContributingArtists { get; set; }
        public DateTime Year { get; set; }
        public string Genre { get; set; }

        //Origin Properties
        public string Directors { get; set; }
        public string Producers { get; set; }
        public string Writers { get; set; }
        public string Publisher { get; set; }
        
        //Content Properties
        public ParentalRating ParentalRating { get; set; }
        public string ParentalRatingReason { get; set; }

        //Media File Properties
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(255)]
        [Required]
        public string FolderPath { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public double Size { get; set; }
    }
}
