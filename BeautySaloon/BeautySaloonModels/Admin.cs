﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySaloonModels
{
    public class Admin
    {
        public int Id { get; set; }

        [Required]
        public string AdminFIO { get; set; }

        [Required]
        public string AdminLogin { get; set; }

        [Required]
        public string AdminPassword { get; set; }

        [ForeignKey("AdminId")]
        public virtual List<Request> Requests { get; set; }
    }
}
