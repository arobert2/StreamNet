using Microsoft.AspNetCore.Mvc;
using StreamNet.DomainEntities.Data;
using StreamNet.DomainEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StreamNet.Server.CMS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private ApplicationDbContext _dbContext;

        public MediaController(ApplicationDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        // GET <MediaController>/5
        [HttpGet("{id}")]
        public VideoMetaData Get(Guid id)
        {
            return _dbContext.Videos.FirstOrDefault(m => m.Id == id);
        }

        // POST <MediaController>
        [HttpPost]
        public void Post([FromBody] VideoMetaData vmd)
        {
            _dbContext.Videos.Add(vmd);
        }

        // PUT api/<MediaController>/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] VideoMetaData vmd)
        {
        }

        // DELETE api/<MediaController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
        }
    }
}
