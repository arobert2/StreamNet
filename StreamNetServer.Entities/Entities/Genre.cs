﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamNet.DomainEntities.Entities
{
    public class Genre
    {
        public Guid Id { get; set; }
        public string GenreName { get; set; }
        public string Description { get; set; }
    }
}
