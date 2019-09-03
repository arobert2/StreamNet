using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StreamNet.DomainEntities.Entities
{
    interface IMediaMetaData
    {
        //Media Description Properties
        string Title { get; set; }
        string Description { get; set; }
        string Artists { get; set; }
        TimeSpan Length { get; set;  }
        string MediaType { get; set; }
    }
}
