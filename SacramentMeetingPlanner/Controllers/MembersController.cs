using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SacramentMeetingPlanner.Data;
using SacramentMeetingPlanner.Models;
using SacramentMeetingPlanner.Models.PlannerViewModels;

namespace SacramentMeetingPlanner.Controllers
{
    public class MembersController : Controller
    {
        private readonly SacramentContext _context;

        public MembersController(SacramentContext context)
        {
            _context = context;
        }

        // GET: Members
        public async Task<IActionResult> Index(int? id)
        {
            var viewModel = new MemberIndexData();
            viewModel.Members = await _context.Members
                .Include(m => m.Calling_Members)
                    .ThenInclude(m => m.Calling)
                .AsNoTracking()
                .ToListAsync();

            if(id != null)
            {
                ViewData["MemberId"] = id.Value;
                Member member = viewModel.Members
                                .Where(m => m.MemberId == id.Value).Single();
                viewModel.Callings = member.Calling_Members.Select(s => s.Calling);
            }
            return View(viewModel);
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .Include(m => m.Calling_Members)
                    .ThenInclude(m => m.Calling)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            //ViewData["Calling"] = new SelectList(_context.Callings, "CallingId", "CallingName");
            var member = new Member();
            member.Calling_Members = new List<Calling_Member>();
            PopulateAssignedCallingData(member);
            return View();
        }

        private void PopulateAssignedCallingData(Member member)
        {
            var allCallings = _context.Callings;
            var memberCallings = new HashSet<int>(member.Calling_Members.Select(c => c.CallingId));
            var viewModel = new List<AssignedMemberData>();
            foreach(var calling  in allCallings)
            {
                viewModel.Add(new AssignedMemberData
                {
                    CallingId = calling.CallingId,
                    CallingName = calling.CallingName,
                    Assigned = memberCallings.Contains(calling.CallingId)
                }) ;
            }
            ViewData["Callings"] = viewModel;
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberId,MemberFName,MemberLName,Birth")] Member member, int[] selectedCallings)
        {
            if (selectedCallings.Length > 0)
            {
                member.Calling_Members = new List<Calling_Member>();
                foreach(var calling in selectedCallings)
                {
                    var callingToAdd = new Calling_Member { MemberId = member.MemberId, CallingId = calling };
                    member.Calling_Members.Add(callingToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateAssignedCallingData(member);
            return View(member);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["Calling"] = new SelectList(_context.Callings, "CallingId", "CallingName");
            var member = await _context.Members
                                .Include(m => m.Calling_Members)
                                    .ThenInclude(m => m.Calling)
                                .AsNoTracking()
                                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }
            PopulateAssignedCallingData(member);
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedCallings)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberToUpdate = await _context.Members
                .Include(m => m.Calling_Members)
                    .ThenInclude(i => i.Calling)
                .FirstOrDefaultAsync(s => s.MemberId == id);

            if (await TryUpdateModelAsync<Member>(memberToUpdate, "", 
                i => i.MemberFName, i => i.MemberLName))
            {
                UpdateMemberCallings(selectedCallings, memberToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateMemberCallings(selectedCallings, memberToUpdate);
            PopulateAssignedCallingData(memberToUpdate);
            return View(memberToUpdate);
        }

        private void UpdateMemberCallings(string[] selectedCallings, Member memberToUpdate)
        {
            if(selectedCallings == null)
            {
                memberToUpdate.Calling_Members = new List<Calling_Member>();
                return;
            }

            var selectedCallingsHS = new HashSet<string>(selectedCallings);
            var memberCallings = new HashSet<int>(memberToUpdate.Calling_Members.Select(c => c.Calling.CallingId));

            foreach(var calling in _context.Callings)
            {
                if (selectedCallingsHS.Contains(calling.CallingId.ToString()))
                {
                    if (!memberCallings.Contains(calling.CallingId))
                    {
                        memberToUpdate.Calling_Members.Add(new Calling_Member { MemberId = memberToUpdate.MemberId, CallingId = calling.CallingId });
                    }
                }
                else
                {
                    if (memberCallings.Contains(calling.CallingId))
                    {
                        Calling_Member callingToRemove = memberToUpdate.Calling_Members.FirstOrDefault(i => i.CallingId == calling.CallingId);
                        _context.Remove(callingToRemove);
                    }
                }
            }
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .Include(m => m.Calling_Members)
                    .ThenInclude(m => m.Calling)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }
            PopulateAssignedCallingData(member);
            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = await _context.Members
                .Include(c => c.Calling_Members)
                .SingleAsync(i => i.MemberId == id);

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.MemberId == id);
        }
    }
}
