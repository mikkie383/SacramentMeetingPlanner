using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SacramentMeetingPlanner.Models
{
    public class Member
    {
        public int MemberId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Column("FirstName")]
        [Display(Name = "First Name")]
        public string MemberFName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string MemberLName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Birth")]
        public DateTime Birth { get; set; }

        public int Age 
        {
            get
            {
                return DateTime.Today.Year - Birth.Year;
            } 
        }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return MemberLName + ", " + MemberFName;
            }
        }

        public ICollection<Planner_Member> Planner_Members { get; set; }

        public ICollection<Calling_Member> Calling_Members { get; set; }
    }
}
