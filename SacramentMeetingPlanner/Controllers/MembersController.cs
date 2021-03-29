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
            PopulateAssignedCallingDate(member);
            return View();
        }

        private void PopulateAssignedCallingDate(Member member)
        {
            var allCallings = _context.Callings;
            var memberCallings = new HashSet<int>(member.Calling_Members.Select(c => c.CallingId));
            var viewModel = new List<AssignedMemberData>();
            foreach(var calling  in allCallings)
            {
                viewModel.Add(new AssignedMemberData
                {
                    CallingId = calling.CallingId,
                    CallingName = calling.CallingName
                });
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
            PopulateAssignedCallingDate(member);
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
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberId,MemberFName,MemberLName,Birth")] Member member)
        {
            if (id != member.MemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.MemberId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(member);
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
            PopulateAssignedCallingDate(member);
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
