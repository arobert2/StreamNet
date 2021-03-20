using Microsoft.AspNetCore.Mvc;
using StreamNet.DomainEntities.Data;
using StreamNet.DomainEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamNet.Server.CMS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MediaListController : ControllerBase
    {
        private ApplicationDbContext _dbContext;

        public MediaListController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
 
        public IEnumerable<VideoMetaData> Get()
        {
            return _dbContext.Videos;
        }
    }
}
