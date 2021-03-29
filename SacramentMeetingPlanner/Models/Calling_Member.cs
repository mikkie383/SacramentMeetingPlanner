using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SacramentMeetingPlanner.Models
{
    public class Calling_Member
    {
        public int Id { get; set; }

        public int CallingId { get; set; }

        public int MemberId { get; set; }

        public Calling Calling { get; set; }

        public Member Member { get; set; }
    }
}
