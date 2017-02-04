using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MegamiManager.Data;
using MegamiManager.Models.MegamiModels;

namespace MegamiManager.Controllers
{
    public class MegamisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MegamisController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Megamis
        public async Task<IActionResult> Index()
        {
            return View(await _context.Megami.ToListAsync());
        }

        // GET: Megamis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var megami = await _context.Megami.SingleOrDefaultAsync(m => m.MegamiId == id);
            if (megami == null)
            {
                return NotFound();
            }

            return View(megami);
        }

        // GET: Megamis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Megamis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MegamiId,AerialMobility,ArmorDefense,Comment,CreatedAt,Description,Design,GroundMobility,LongRangeBattle,MediumRangeBattle,Name,OperationTime,OwnerId,SearchEnemy,Secret,ShortRangeBattle,Type,UpdatedAt,Weight")] Megami megami)
        {
            if (ModelState.IsValid)
            {
                _context.Add(megami);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(megami);
        }

        // GET: Megamis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var megami = await _context.Megami.SingleOrDefaultAsync(m => m.MegamiId == id);
            if (megami == null)
            {
                return NotFound();
            }
            return View(megami);
        }

        // POST: Megamis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MegamiId,AerialMobility,ArmorDefense,Comment,CreatedAt,Description,Design,GroundMobility,LongRangeBattle,MediumRangeBattle,Name,OperationTime,OwnerId,SearchEnemy,Secret,ShortRangeBattle,Type,UpdatedAt,Weight")] Megami megami)
        {
            if (id != megami.MegamiId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(megami);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MegamiExists(megami.MegamiId))
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
            return View(megami);
        }

        // GET: Megamis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var megami = await _context.Megami.SingleOrDefaultAsync(m => m.MegamiId == id);
            if (megami == null)
            {
                return NotFound();
            }

            return View(megami);
        }

        // POST: Megamis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var megami = await _context.Megami.SingleOrDefaultAsync(m => m.MegamiId == id);
            _context.Megami.Remove(megami);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MegamiExists(int id)
        {
            return _context.Megami.Any(e => e.MegamiId == id);
        }
    }
}
