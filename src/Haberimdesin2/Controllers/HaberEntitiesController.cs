using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Haberimdesin2.Data;
using Haberimdesin2.Models;

namespace Haberimdesin2.Controllers
{
    public class HaberEntitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HaberEntitiesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: HaberEntities
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Haber.Include(h => h.category).Include(h => h.user);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: HaberEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var haberEntity = await _context.Haber.SingleOrDefaultAsync(m => m.HaberID == id);
            if (haberEntity == null)
            {
                return NotFound();
            }

            return View(haberEntity);
        }

        // GET: HaberEntities/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Category, "CategoryID", "CategoryID");
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: HaberEntities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HaberID,CategoryID,Detail,HeadLine,Id,Latitude,Longitude,PrimaryImgURL,TimeStamp,Title")] HaberEntity haberEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(haberEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "HaberEntities");
            }
            ViewData["CategoryID"] = new SelectList(_context.Category, "CategoryID", "CategoryID", haberEntity.CategoryID);
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Id", haberEntity.Id);
            return View(haberEntity);
        }

        // GET: HaberEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var haberEntity = await _context.Haber.SingleOrDefaultAsync(m => m.HaberID == id);
            if (haberEntity == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Category, "CategoryID", "CategoryID", haberEntity.CategoryID);
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Id", haberEntity.Id);
            return View(haberEntity);
        }

        // POST: HaberEntities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HaberID,CategoryID,Detail,HeadLine,Id,Latitude,Longitude,PrimaryImgURL,TimeStamp,Title")] HaberEntity haberEntity)
        {
            if (id != haberEntity.HaberID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(haberEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HaberEntityExists(haberEntity.HaberID))
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
            ViewData["CategoryID"] = new SelectList(_context.Category, "CategoryID", "CategoryID", haberEntity.CategoryID);
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Id", haberEntity.Id);
            return View(haberEntity);
        }

        // GET: HaberEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var haberEntity = await _context.Haber.SingleOrDefaultAsync(m => m.HaberID == id);
            if (haberEntity == null)
            {
                return NotFound();
            }

            return View(haberEntity);
        }

        // POST: HaberEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var haberEntity = await _context.Haber.SingleOrDefaultAsync(m => m.HaberID == id);
            _context.Haber.Remove(haberEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool HaberEntityExists(int id)
        {
            return _context.Haber.Any(e => e.HaberID == id);
        }
    }
}
