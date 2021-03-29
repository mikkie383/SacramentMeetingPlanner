using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SacramentMeetingPlanner.Models
{
    public class Calling
    {
        public int CallingId { get; set; }

        [Required]
        public string CallingName { get; set; }

        public ICollection<Calling_Member> Calling_Members { get; set; }
    }
}
