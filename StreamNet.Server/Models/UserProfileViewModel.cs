using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StreamNet.Server.Models
{
    public class UserProfileViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string UserProfilePictureBase64 { get; set; }
        public List<string> Roles { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string About { get; set; }
    }
}
