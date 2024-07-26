using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GunShop.Data;
using GunShop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiBasketController : ControllerBase
    {
        private readonly GunShopContext _context;

        public ApiBasketController(GunShopContext context)
        {
            _context = context;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToBasket([FromBody] int gunId)
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

        [HttpPost("remove")]
        public async Task<IActionResult> RemoveFromBasket([FromBody] int gunId)
        {
            var basketItem = await _context.BasketItems.FirstOrDefaultAsync(b => b.GunId == gunId);
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

        [HttpPost("clear")]
        public async Task<IActionResult> ClearBasket()
        {
            var basketItems = await _context.BasketItems.ToListAsync();
            _context.BasketItems.RemoveRange(basketItems);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("items")]
        public async Task<IActionResult> GetBasketItems()
        {
            var basketItems = await _context.BasketItems.Include(b => b.Gun).ToListAsync();
            return Ok(basketItems);
        }
    }
}
