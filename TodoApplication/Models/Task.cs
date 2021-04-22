﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApplication.Models
{
    public class Task
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
