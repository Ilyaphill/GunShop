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
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Получаем популярные товары
            var popularGuns = await _context.Guns.OrderByDescending(g => g.Price).Take(6).ToListAsync();
            return View(popularGuns);
        }
    }
}
