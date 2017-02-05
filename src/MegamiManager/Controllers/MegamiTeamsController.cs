using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MegamiManager.Data;
using MegamiManager.Models.MegamiModels;
using Microsoft.AspNetCore.Authorization;

namespace MegamiManager.Controllers
{
    [Authorize]
    public class MegamiTeamsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MegamiTeamsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: MegamiTeams
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MegamiTeam.Include(m => m.Megami).Include(m => m.Team);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MegamiTeams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var megamiTeam = await _context.MegamiTeam.SingleOrDefaultAsync(m => m.TeamId == id);
            if (megamiTeam == null)
            {
                return NotFound();
            }

            return View(megamiTeam);
        }

        // GET: MegamiTeams/Create
        public IActionResult Create()
        {
            ViewData["MegamiId"] = new SelectList(_context.Megami, "MegamiId", "Design");
            ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "Name");
            return View();
        }

        // POST: MegamiTeams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamId,MegamiId")] MegamiTeam megamiTeam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(megamiTeam);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["MegamiId"] = new SelectList(_context.Megami, "MegamiId", "Design", megamiTeam.MegamiId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "Name", megamiTeam.TeamId);
            return View(megamiTeam);
        }

        // GET: MegamiTeams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var megamiTeam = await _context.MegamiTeam.SingleOrDefaultAsync(m => m.TeamId == id);
            if (megamiTeam == null)
            {
                return NotFound();
            }
            ViewData["MegamiId"] = new SelectList(_context.Megami, "MegamiId", "Design", megamiTeam.MegamiId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "Name", megamiTeam.TeamId);
            return View(megamiTeam);
        }

        // POST: MegamiTeams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeamId,MegamiId")] MegamiTeam megamiTeam)
        {
            if (id != megamiTeam.TeamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(megamiTeam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MegamiTeamExists(megamiTeam.TeamId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["MegamiId"] = new SelectList(_context.Megami, "MegamiId", "Design", megamiTeam.MegamiId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "Name", megamiTeam.TeamId);
            return View(megamiTeam);
        }

        // GET: MegamiTeams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var megamiTeam = await _context.MegamiTeam.SingleOrDefaultAsync(m => m.TeamId == id);
            if (megamiTeam == null)
            {
                return NotFound();
            }

            return View(megamiTeam);
        }

        // POST: MegamiTeams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var megamiTeam = await _context.MegamiTeam.SingleOrDefaultAsync(m => m.TeamId == id);
            _context.MegamiTeam.Remove(megamiTeam);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MegamiTeamExists(int id)
        {
            return _context.MegamiTeam.Any(e => e.TeamId == id);
        }
    }
}
