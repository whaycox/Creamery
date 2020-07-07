﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Cheddar.Domain
{
    public class Organization
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
