﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Book
    {
        public Guid id { get; set; }

         public string Title { get; set; }
         public string Description { get; set; }

        public string Author { get; set; }
            
    }
}
