using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SacramentMeetingPlanner.Models
{
    public class Planner_Member
    {
        public int Id { get; set; }

        public int PlannerId { get; set; }

        public int MemberId { get; set; }

        public Planner Planner { get; set; }

        public Member Member { get; set; }
    }
}
