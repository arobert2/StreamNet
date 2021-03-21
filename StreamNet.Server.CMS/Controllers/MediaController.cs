using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StreamNet.DomainEntities.Data;
using StreamNet.DomainEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public VideoMetaData Get(Guid id)
        {
            return _dbContext.Videos.FirstOrDefault(m => m.Id == id);
        }

        // POST <MediaController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Post([FromBody] VideoMetaData vmd)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _dbContext.Videos.Add(vmd);
            _dbContext.SaveChanges();
            





            return Created("Media/", vmd);
        }

        // PUT api/<MediaController>/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] VideoMetaData vmd)
        {
            var existingEntity = _dbContext.Videos.FirstOrDefault(v => v.Id == vmd.Id);
            existingEntity = vmd;
            _dbContext.SaveChanges();
        }

        // DELETE api/<MediaController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _dbContext.Videos.Remove(_dbContext.Videos.FirstOrDefault(m => m.Id == id));
            _dbContext.SaveChanges();
        }
    }
}
