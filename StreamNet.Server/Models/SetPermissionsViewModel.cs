using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StreamNet.Server.Models
{
    public class SetPermissionsViewModel
    {
        [Required]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string ProfilePicture { get; set; }
        public string Roles { get; set; }
        [Display(Name = "Admin Access")]
        public bool Administrator { get; set; }
        [Display(Name = "Lock Account")]
        public bool Locked { get; set; }
    }
}
