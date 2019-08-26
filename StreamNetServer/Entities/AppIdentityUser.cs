using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamNetServer.Entities
{
    public class AppIdentityUser : IdentityUser<Guid>
    {
        public byte[] UserProfilePicture { get; set; }

    }
}
