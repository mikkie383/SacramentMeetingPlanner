﻿using System;
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
    public class PlannersController : Controller
    {
        private readonly SacramentContext _context;

        public PlannersController(SacramentContext context)
        {
            _context = context;
        }

        // GET: Planners
        public async Task<IActionResult> Index()
        {
            return View(await _context.Planners.ToListAsync());
        }

        // GET: Planners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planner = await _context.Planners
                .FirstOrDefaultAsync(m => m.PlannerId == id);
            if (planner == null)
            {
                return NotFound();
            }

            return View(planner);
        }

        // GET: Planners/Create
        public IActionResult Create()
        {
            ViewData["Member"] = new SelectList(_context.Members, "MemberId", "FullName");
            ViewData["Bishiopric"] = new SelectList(_context.Members.Where(m => m.MemberId == 1 || m.MemberId == 2 || m.MemberId == 3)
                                                                , "MemberId", "FullName");
            return View();
        }

        /*private void PopulateAssignedCourseData(Planner planner)
        {
            var allMembers = _context.Members;
            var plannerMember = new HashSet<int>(planner.Planner_Members.Select(m => m.MemberId));
            var viewModel = new List<AssignedMemberData>();
            foreach(var member in allMembers)
            {
                viewModel.Add(new AssignedMemberData
                {
                    MemberId = member.MemberId,
                    MemberName = member.FullName
                });
                    
            }
            ViewData["Members"] = viewModel;
        }*/

        // POST: Planners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlannerId,PlannedDate,President,Conducting,OpeningHymn,Invocation,SacramentHymn,Speaker,ClosingHymn,Benediction")] Planner planner, Member member)
        {
            if (ModelState.IsValid)
            {
                planner.President = "Michael Tsao";
                _context.Add(planner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "FullName");
            return View(planner);
        }

        // GET: Planners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planner = await _context.Planners.FindAsync(id);
            if (planner == null)
            {
                return NotFound();
            }
            return View(planner);
        }

        // POST: Planners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlannerId,PlannedDate,President,Conducting,OpeningHymn,Invocation,SacramentHymn,Speaker,ClosingHymn,Benediction")] Planner planner)
        {
            if (id != planner.PlannerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(planner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlannerExists(planner.PlannerId))
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
            return View(planner);
        }

        // GET: Planners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planner = await _context.Planners
                .FirstOrDefaultAsync(m => m.PlannerId == id);
            if (planner == null)
            {
                return NotFound();
            }

            return View(planner);
        }

        // POST: Planners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var planner = await _context.Planners.FindAsync(id);
            _context.Planners.Remove(planner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlannerExists(int id)
        {
            return _context.Planners.Any(e => e.PlannerId == id);
        }
    }
}
