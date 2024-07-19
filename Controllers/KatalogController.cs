using GunShop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GunShop.Controllers
{
    public class KatalogController : Controller
    {
        private readonly AppDbContext _context;

        public KatalogController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Katalog
        public async Task<IActionResult> Index()
        {
            return View(await _context.Guns.ToListAsync());
        }
    }
 
    }

