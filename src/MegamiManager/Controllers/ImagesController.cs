using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MegamiManager.Data;
using MegamiManager.Models.MegamiModels;
using Microsoft.AspNetCore.Http;
using MegamiManager.Repositories;

namespace MegamiManager.Controllers
{
    public class ImagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ImagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        private static SakuraObjectStorageImageRepository GetRepository()
        {
            // XXX DI
            return new SakuraObjectStorageImageRepository(
                    "megami-device",
                    "megami-device",
                    "xxx"
                );
        }

        // GET: Images
        public async Task<IActionResult> Index()
        {
            return View(await _context.Images.ToListAsync());
        }

        // GET: Images/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _context.Images.SingleOrDefaultAsync(m => m.ImageId == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // GET: Images/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Images/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //        public async Task<IActionResult> Create([Bind("ImageId,CreatedAt,Name,OwnerId,PrivateThumbnailUri,PrivateUri,PublicThumbnailUri,PublicUri,Timestamp,UpdatedAt")] Image image)
        public async Task<IActionResult> Create(IFormFile file)
        {
            var imageRepository = GetRepository();
            var image = await imageRepository.Create(file);
            _context.Add(image);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = image.ImageId });
        }

        // GET: Images/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _context.Images.SingleOrDefaultAsync(m => m.ImageId == id);
            if (image == null)
            {
                return NotFound();
            }
            return View(image);
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ImageId,Name,Timestamp")] Image image, IFormFile file)
        {
            if (id != image.ImageId)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            {
                try
                {
                    var imageExist = await _context.Images.SingleOrDefaultAsync(x => x.ImageId == id);
                    imageExist.Name = image.Name;
                    var imageRepository = GetRepository();
                    await imageRepository.Update(imageExist, file);
                    _context.Update(imageExist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImageExists(image.ImageId))
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
            return View(image);
        }

        // GET: Images/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _context.Images.SingleOrDefaultAsync(m => m.ImageId == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var image = await _context.Images.SingleOrDefaultAsync(m => m.ImageId == id);
            var imageRepository = GetRepository();
            await imageRepository.Delete(image);
            _context.Images.Remove(image);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ImageExists(int id)
        {
            return _context.Images.Any(e => e.ImageId == id);
        }
    }
}
