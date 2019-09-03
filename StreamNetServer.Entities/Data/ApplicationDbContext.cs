using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StreamNet.DomainEntities.Entities;
using System;

namespace StreamNet.DomainEntities.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppIdentityUser,IdentityRole<Guid>,Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        /// <summary>
        /// Represents your video library
        /// </summary>
        public DbSet<VideoMetaData> Videos { get; set; }
        /// <summary>
        /// Genres currently in library
        /// </summary>
        public DbSet<Genre> Genres { get; set; }
    }
}
