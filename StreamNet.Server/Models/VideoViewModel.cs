using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamNet.Server.Models
{
    public class MediaReadViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public TimeSpan Length { get; set; }
        public string CoverArtBase64 { get; set; }
    }
}
