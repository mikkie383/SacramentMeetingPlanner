using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SacramentMeetingPlanner.Models;

namespace SacramentMeetingPlanner.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SacramentContext context)
        {
            context.Database.EnsureCreated();

            //looks for members before intitalizing db
            if (context.Members.Any())
            {
                return;
            }
            
            var members = new Member[]
            {
                //do I add CallingID here?
                new Member{MemberFName="John",MemberLName="Doe",Age=37},
                new Member{MemberFName="Susan",MemberLName="Surandon",Age=20},
                new Member{MemberFName="Alex",MemberLName="Wiggles",Age=27},
                new Member{MemberFName="Helen",MemberLName="Parr",Age=42},
                new Member{MemberFName="Jimmeny",MemberLName="Cricket",Age=53}


            };
            //add contents of members to db
            foreach (Member m in members)
            {
                context.Members.Add(m);
            }
            context.SaveChanges();

            var planners = new Planner[]
            {
                new Planner{PlannedDate=DateTime.Parse("2021-03-29"),
                    President="John Doe",
                    Conducting="Susan Surandon",
                    OpeningHymn="204",
                    Invocation="Alex Wiggles",
                    SacramentHymn="205",
                    Speaker="Helen Parr",
                    ClosingHymn="206",
                    Benediction="Jimmeny Cricket"}
            };
            foreach (Planner p in planners)
            {
                context.Planners.Add(p);
            }
            context.SaveChanges();
        }
    }
}
