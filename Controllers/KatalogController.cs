using GunShop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GunShop.Controllers
{
    public class KatalogController : Controller
    {
        private readonly GunShopContext _context;

        public KatalogController(GunShopContext context)
        {
            _context = context;
        }

        // GET: Katalog
        public async Task<IActionResult> Index()
        {
            return View(await _context.Gun.ToListAsync());
        }
    }
}
