using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StreamNet.DomainEntities.Entities
{
    public enum ErrorStatus { info, warning, error}
    public class AdminMessage
    {
        public Guid Id { get; set; }
        [Required]
        public string Body { get; set; }
        public bool Read { get; set; }
        [Required]
        public DateTime TimeStamp { get; set; }
        public ErrorStatus Status { get; set; }
    }
}
