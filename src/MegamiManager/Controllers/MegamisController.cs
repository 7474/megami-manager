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
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MegamiManager.Models;
using Microsoft.AspNetCore.Http;
using MegamiManager.Repositories;

namespace MegamiManager.Controllers
{
    public class MegamisController : AbstractController
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageRepository _imageRepository;

        public MegamisController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IImageRepository imageRepository,
            ILoggerFactory loggerFactory)
            : base(userManager, loggerFactory)
        {
            _context = context;
            _imageRepository = imageRepository;
        }

        // GET: Megamis
        public async Task<IActionResult> Index()
        {
            return View(await _context.Megami
                .Include(x => x.Owner)
                .ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> MyIndex()
        {
            var user = await GetCurrentUserAsync();
            return View(await _context.Megami
                .Include(x => x.Owner)
                .Where(x => x.OwnerId == user.Id)
                .ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> AddImage(int? id)
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddImage(int? id, Image image, IFormFile file)
        {
            var megami = await _context.Megami.SingleOrDefaultAsync(m => m.MegamiId == id);
            if (megami == null)
            {
                return NotFound();
            }
            megami.AssertOwn(await GetCurrentUserAsync());

            var imageInsert = await _imageRepository.Create(file);

            imageInsert.Name = string.IsNullOrEmpty(image.Name) ? imageInsert.Name : image.Name;
            imageInsert.Description = image.Description;
            imageInsert.Comment = image.Comment;
            _context.Add(imageInsert);
            await _context.SaveChangesAsync();

            var megamiImage = new MegamiImage()
            {
                MegamiId = megami.MegamiId,
                ImageId = imageInsert.ImageId,
                DisplayOrder = _context.MegamiImages.Where(x => x.MegamiId == megami.MegamiId).Max(x => x.DisplayOrder) + 1
            };
            _context.Add(megamiImage);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new
            {
                id = id
            });
        }


        // GET: Megamis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var megami = await _context.Megami
                .Include(x => x.Images).ThenInclude(x => x.Image)
                .SingleOrDefaultAsync(m => m.MegamiId == id);
            if (megami == null)
            {
                return NotFound();
            }

            return View(megami);
        }

        // GET: Megamis/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Megamis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("MegamiId,AerialMobility,ArmorDefense,Comment,CreatedAt,Description,Design,GroundMobility,LongRangeBattle,MediumRangeBattle,Name,OperationTime,OwnerId,SearchEnemy,Secret,ShortRangeBattle,Type,UpdatedAt,Weight")] Megami megami)
        public async Task<IActionResult> Create(Megami megami)
        {
            if (ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();
                megami.OwnerId = user.Id;
                _context.Add(megami);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(megami);
        }

        // GET: Megamis/Edit/5
        [Authorize]
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
            megami.AssertOwn(await GetCurrentUserAsync());
            return View(megami);
        }

        // POST: Megamis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Megami megami)
        {
            if (id != megami.MegamiId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var megamiExist = await _context.Megami.SingleOrDefaultAsync(m => m.MegamiId == id);
                megamiExist.AssertOwn(await GetCurrentUserAsync());
                try
                {
                    // XXX ãlÇﬂë÷Ç¶Ç‡ÇµÇ≠ÇÕÇ‡Ç¡Ç∆óvóÃÇÊÇ≠åüèÿ
                    // TryUpdateModel Ç∆ïπópÅH
                    //                 if (TryUpdateModel(studentToUpdate, "",
                    //new string[] { "LastName", "FirstMidName", "EnrollmentDate" }))
                    //                 {
                    _context.Update(megamiExist);
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
        [Authorize]
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
            megami.AssertOwn(await GetCurrentUserAsync());

            return View(megami);
        }

        // POST: Megamis/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var megami = await _context.Megami.SingleOrDefaultAsync(m => m.MegamiId == id);
            megami.AssertOwn(await GetCurrentUserAsync());
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
