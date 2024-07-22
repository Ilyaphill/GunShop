using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GunShop.Data;
using GunShop.Models;
using System.Threading.Tasks;

namespace GunShop.Controllers
{
    public class BasketController : Controller
    {
        private readonly GunShopContext _context;

        public BasketController(GunShopContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddToBasket(int gunId)
        {
            var basketItem = await _context.BasketItems.FirstOrDefaultAsync(b => b.GunId == gunId);
            if (basketItem != null)
            {
                basketItem.Quantity++;
            }
            else
            {
                basketItem = new BasketItem
                {
                    GunId = gunId,
                    Quantity = 1
                };
                _context.BasketItems.Add(basketItem);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromBasket(int id)
        {
            var basketItem = await _context.BasketItems.FindAsync(id);
            if (basketItem != null)
            {
                if (basketItem.Quantity > 1)
                {
                    basketItem.Quantity--;
                }
                else
                {
                    _context.BasketItems.Remove(basketItem);
                }
                await _context.SaveChangesAsync();
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> ClearBasket()
        {
            var basketItems = await _context.BasketItems.ToListAsync();
            _context.BasketItems.RemoveRange(basketItems);
            await _context.SaveChangesAsync();
            return Ok();
        }

        public async Task<IActionResult> Index()
        {
            var basketItems = await _context.BasketItems.Include(b => b.Gun).ToListAsync();
            return View(basketItems);
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(string name, string address, string cardNumber)
        {
            // Создание нового заказа
            var order = new Order
            {
                Name = name,
                Address = address,
                CardNumber = cardNumber,
                OrderDate = DateTime.Now,
                OrderItems = new List<OrderItem>()
            };

            var basketItems = await _context.BasketItems.Include(b => b.Gun).ToListAsync();

            foreach (var item in basketItems)
            {
                order.OrderItems.Add(new OrderItem
                {
                    GunId = item.GunId,
                    Quantity = item.Quantity
                });
            }

            _context.Orders.Add(order);

            // Очистка корзины после оплаты
            _context.BasketItems.RemoveRange(basketItems);
            await _context.SaveChangesAsync();

            // Перенаправление на страницу подтверждения
            return RedirectToAction("OrderConfirmation");
        }

        public IActionResult OrderConfirmation()
        {
            return View();
        }

        public async Task<IActionResult> OrderHistory()
        {
            var orders = await _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Gun).ToListAsync();
            return View(orders);
        }
        public async Task<IActionResult> History()
        {
            var orders = await _context.Orders.Include(o => o.OrderItems)
                                              .ThenInclude(oi => oi.Gun)
                                              .ToListAsync();
            return View(orders);
        }

        // POST: Basket/ClearHistory
        [HttpPost]
        public async Task<IActionResult> ClearHistory()
        {
            var orders = await _context.Orders.Include(o => o.OrderItems).ToListAsync();

            _context.OrderItems.RemoveRange(orders.SelectMany(o => o.OrderItems));
            _context.Orders.RemoveRange(orders);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(History));
        }
    }
}
