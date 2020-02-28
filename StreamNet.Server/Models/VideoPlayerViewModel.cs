using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamNet.Server.Models
{
    public class VideoPlayerViewModel
    {
        public MediaReadViewModel VideData { get; set; }
        public string Video { get; set; }
        public string ContentType { get; set; }
    }
}
