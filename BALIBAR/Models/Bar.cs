using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace BALIBAR.Models
{
    public class Bar
    {

        public int Id { get; set; }

        [StringLength(30, MinimumLength = 1)]
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [MinLength(2)]
        [MaxLength(200)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Range(1, 1000)]
        [Display(Name = "Max participants")]
        public int MaxParticipants { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "In/Out")]
        public string InOut { get; set; }

        [Display(Name = "Kosher")]
        public bool Kosher { get; set; }

        [Display(Name = "Accessible")]
        public bool Accessible { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm tt}")]
        [DataType(DataType.Time)]
        [Display(Name = "Opening time")]
        public DateTime OpeningTime { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm tt}")]
        [DataType(DataType.Time)]
        [Display(Name = "Closing time")]
        public DateTime ClosingTime { get; set; }

        [Range(16, 120)]
        [Display(Name = "Min Age")]
        public int MinAge { get; set; }

        [DataType(DataType.Url)]
        public string ImgUrl { get; set; }

        [Required]
        [Display(Name = "Type")]
        public Type Type { get; set; }

    }
}
