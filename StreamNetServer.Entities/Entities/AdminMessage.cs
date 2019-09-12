using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamNet.DomainEntities.Entities
{
    public class AdminMessage
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public bool Read { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
