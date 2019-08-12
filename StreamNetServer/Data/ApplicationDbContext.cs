using Microsoft.EntityFrameworkCore;
using StreamNetServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamNetServer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        /// <summary>
        /// Represents your video library
        /// </summary>
        public DbSet<VideoMetaData> Videos { get; set; }
        /// <summary>
        /// Represents your music library
        /// </summary>
        public DbSet<AudioMetaData> Music { get; set; }
    }
}
