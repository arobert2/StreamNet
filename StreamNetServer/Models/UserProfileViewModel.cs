using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamNetServer.Models
{
    public class UserProfileViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string ProfilePicture { get; set; }
        public string Roles { get; set; }
    }
}
