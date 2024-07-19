using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GunShop.Data;
using GunShop.Models;

namespace GunShop.Controllers
{
    public class GunsController : Controller
    {
        private readonly AppDbContext _context;

        public GunsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Guns
        public async Task<IActionResult> Index()
        {
            return View(await _context.Guns.ToListAsync());
        }

        // GET: Guns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gun = await _context.Guns
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gun == null)
            {
                return NotFound();
            }

            return View(gun);
        }

        // GET: Guns/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Guns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Model,Manufacturer,Type,Caliber,Price,Weight,MagazineCapacity,Description")] Gun gun)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gun);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gun);
        }

        // GET: Guns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gun = await _context.Guns.FindAsync(id);
            if (gun == null)
            {
                return NotFound();
            }
            return View(gun);
        }

        // POST: Guns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Model,Manufacturer,Type,Caliber,Price,Weight,MagazineCapacity,Description")] Gun gun)
        {
            if (id != gun.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gun);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GunExists(gun.Id))
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
            return View(gun);
        }

        // GET: Guns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gun = await _context.Guns
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gun == null)
            {
                return NotFound();
            }

            return View(gun);
        }

        // POST: Guns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gun = await _context.Guns.FindAsync(id);
            if (gun != null)
            {
                _context.Guns.Remove(gun);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GunExists(int id)
        {
            return _context.Guns.Any(e => e.Id == id);
        }
    }
}
