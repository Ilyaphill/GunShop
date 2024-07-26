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
    
    public class Guns1Controller : Controller
    {
        private readonly GunShopContext _context;

        public Guns1Controller(GunShopContext context)
        {
            _context = context;
        }

        // GET: Guns1
        public async Task<IActionResult> Index()
        {
            return View(await _context.Gun.ToListAsync());
        }

        // GET: Guns1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gun = await _context.Gun
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gun == null)
            {
                return NotFound();
            }

            return View(gun);
        }

        // GET: Guns1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Guns1/Create
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

        // GET: Guns1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gun = await _context.Gun.FindAsync(id);
            if (gun == null)
            {
                return NotFound();
            }
            return View(gun);
        }

        // POST: Guns1/Edit/5
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

        // GET: Guns1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gun = await _context.Gun
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gun == null)
            {
                return NotFound();
            }

            return View(gun);
        }

        // POST: Guns1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gun = await _context.Gun.FindAsync(id);
            if (gun != null)
            {
                _context.Gun.Remove(gun);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GunExists(int id)
        {
            return _context.Gun.Any(e => e.Id == id);
        }
    }
}