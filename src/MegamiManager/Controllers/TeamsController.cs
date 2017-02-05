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
using MegamiManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace MegamiManager.Controllers
{
    public class TeamsController : AbstractController
    {
        private readonly ApplicationDbContext _context;

        public TeamsController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            ILoggerFactory loggerFactory)
            : base(userManager, loggerFactory)
        {
            _context = context;
        }

        // GET: Teams
        public async Task<IActionResult> Index()
        {
            return View(await _context.Teams
                .Include(x => x.Owner)
                .ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> MyIndex()
        {
            var user = await GetCurrentUserAsync();
            return View(await _context.Teams
                .Include(x => x.Owner)
                .Where(x => x.OwnerId == user.Id)
                .ToListAsync());
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .Include(x => x.Owner)
                .Include(x => x.Members)
                .ThenInclude(x => x.Megami)
                .SingleOrDefaultAsync(m => m.TeamId == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Team team)
        {
            if (ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();
                team.OwnerId = user.Id;
                _context.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(team);
        }

        // GET: Teams/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .Include(x => x.Owner)
                .Include(x => x.Members)
                .ThenInclude(x => x.Megami)
                .SingleOrDefaultAsync(m => m.TeamId == id);
            if (team == null)
            {
                return NotFound();
            }
            var user = await GetCurrentUserAsync();
            team.AssertOwn(user);
            // XXX Ç»ÇÒÇ©Ç¢Ç¢ä¥Ç∂Ç…Ç±ÇÒÇ€Å[ÇÀÇ∆Ç…ÇµÇΩÇ¢Ç»Çü
            ViewBag.Megamis = (await _context.Megami.Where(x => x.OwnerId == user.Id).ToListAsync())
                .Where(x => !team.Members.Any(y => y.MegamiId == x.MegamiId)).ToList();
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Team team)
        {
            if (id != team.TeamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var teamExist = await _context.Teams.SingleOrDefaultAsync(m => m.TeamId == id);
                teamExist.AssertOwn(await GetCurrentUserAsync());
                try
                {
                    // XXX ãlÇﬂë÷Ç¶Ç‡ÇµÇ≠ÇÕÇ‡Ç¡Ç∆óvóÃÇÊÇ≠åüèÿ
                    teamExist.Name = team.Name;
                    teamExist.Description = team.Description;
                    teamExist.Comment = team.Comment;
                    _context.Update(teamExist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.TeamId))
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
            return View(team);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMember(int? id, int? megamiId)
        {
            var team = await _context.Teams.SingleOrDefaultAsync(m => m.TeamId == id);
            if (team == null)
            {
                return NotFound();
            }
            var megami = await _context.Megami.SingleOrDefaultAsync(m => m.MegamiId == megamiId);
            if (megami == null)
            {
                return NotFound();
            }
            var user = await GetCurrentUserAsync();

            team.AssertOwn(user);
            megami.AssertOwn(user);

            _context.Add(new MegamiTeam()
            {
                TeamId = id.Value,
                MegamiId = megamiId.Value
            });
            await _context.SaveChangesAsync();
            return RedirectToAction("Edit", new { id = id });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveMember(int? id, int? megamiId)
        {
            var megamiTeam = await _context.MegamiTeam
                .Include(x => x.Team)
                .Include(x => x.Megami)
                .SingleOrDefaultAsync(m => m.TeamId == id && m.MegamiId == megamiId);
            if (megamiTeam == null)
            {
                return NotFound();
            }
            var user = await GetCurrentUserAsync();

            megamiTeam.Team.AssertOwn(user);
            megamiTeam.Megami.AssertOwn(user);

            _context.Remove(megamiTeam);
            await _context.SaveChangesAsync();
            return RedirectToAction("Edit", new { id = id });
        }

        // GET: Teams/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams.SingleOrDefaultAsync(m => m.TeamId == id);
            if (team == null)
            {
                return NotFound();
            }
            team.AssertOwn(await GetCurrentUserAsync());

            return View(team);
        }

        // POST: Teams/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team = await _context.Teams.SingleOrDefaultAsync(m => m.TeamId == id);
            team.AssertOwn(await GetCurrentUserAsync());
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.TeamId == id);
        }
    }
}
