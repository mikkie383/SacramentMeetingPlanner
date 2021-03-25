using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SacramentMeetingPlanner.Models
{
    public class Member
    {
        public int MemberId { get; set; }

        public string MemberFName { get; set; }

        public string MemberLName { get; set; }

        public int Age { get; set; }

        public int CallingId { get; set; }
    }
}
