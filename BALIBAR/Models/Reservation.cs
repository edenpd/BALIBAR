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

        public string CustomerId { get; set; }

        [Display(Name = "Date & Time")]
        public DateTime DateTime { get; set; }

        [Display(Name = "Bar")]
        public Bar Bar { get; set; }

        public int BarID { get; set; }

        [Display(Name = "Number Of Attendees")]
        public int AttendeesNum { get; set; }
    }
}
