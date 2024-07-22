using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GunShop.Data;
using GunShop.Models;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly GunShopContext _context;

        public HomeController(GunShopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Получаем популярные товары
            var popularGuns = await _context.Gun
                .OrderByDescending(g => g.Price)
                .Take(6)
                .ToListAsync();

            return View(popularGuns);
        }
    }
}
