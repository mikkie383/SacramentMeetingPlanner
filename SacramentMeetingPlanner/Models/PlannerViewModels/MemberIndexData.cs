using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SacramentMeetingPlanner.Models.PlannerViewModels
{
    public class MemberIndexData
    {
        public IEnumerable<Planner> Planners { get; set; }
        public IEnumerable<Member> Members { get; set; }
        public IEnumerable<Calling> Callings { get; set; }
    }
}
