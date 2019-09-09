using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StreamNet.Server.Models
{
    public class ChangeCoverArtModel
    {
        [Required]
        public Guid Id { get; set; }
        public string Image { get; set; }
        [Required]
        public IFormFile NewImage { get; set; }
    }
}
