using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamNet.Server.DomainEntities.Entities
{ 
    public class AppIdentityUser : IdentityUser<Guid>
    {
        public byte[] UserProfilePicture { get; set; }
        public string Website { get; set; }
        public string About { get; set; }
    }
}
