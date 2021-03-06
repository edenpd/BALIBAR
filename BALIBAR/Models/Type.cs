﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace BALIBAR.Models
{
    public class Type
    {
        public int Id { get; set; }

        [Display(Name = "Type Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Music type")]
        [Required]
        public string MusicType { get; set; }

    }
}
