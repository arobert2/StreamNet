using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamNetServer.Models
{
    public class MediaReadViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public TimeSpan Length { get; set; }
        public byte[] CoverARt { get; set; }
    }
}
