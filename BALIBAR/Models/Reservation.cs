using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BALIBAR.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Display(Name = "Customer")]
        public ApplicationUser Customer { get; set; }

        [Display(Name = "Date & Time")]
        public DateTime DateTime { get; set; }

        [Display(Name = "Music Bar")]
        public Bar Bar { get; set; }

        [Display(Name = "Number Of Attendees")]
        public int AttendeesNum { get; set; }
    }
}
