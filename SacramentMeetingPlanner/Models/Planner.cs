using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SacramentMeetingPlanner.Models
{
    public class Planner
    {
        [Display(Name = "Number")]
        public int PlannerId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Planned Date")]
        public DateTime PlannedDate { get; set; }

        public string President { get; set; }

        [Required]
        [Display(Name = "Opening Hymn")]
        public string OpeningHymn { get; set; }

        [Required]
        [Display(Name = "Sacrament Hymn")]
        public string SacramentHymn { get; set; }

        //public string SacramentPassing1 { get; set; }

        //public string SacramentPassing2 { get; set; }

        [Required]
        [Display(Name = "Closing Hymn")]
        public string ClosingHymn { get; set; }

        public ICollection<Planner_Member> Planner_Members { get; set; }

    }
}
