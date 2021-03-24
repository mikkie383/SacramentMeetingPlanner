using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SacramentMeetingPlanner.Models
{
    public class Planner
    {
        public int PlannerId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Planned Date")]
        public DateTime PlannedDate { get; set; }

        public string President { get; set; }

        public string Conducting { get; set; }

        public string OpeningHymn { get; set; }

        public string Invocation { get; set; }

        public string SacramentHymn { get; set; }

        //public string SacramentPassing1 { get; set; }

        //public string SacramentPassing2 { get; set; }

        public string Speaker { get; set; }

        public string ClosingHymn { get; set; }

        public string Benediction { get; set; }

        public ICollection<Member> Members { get; set; }

        public ICollection<Hymn> Hymns { get; set; }
    }
}
