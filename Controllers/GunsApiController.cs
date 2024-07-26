using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GunShop.Data;
using GunShop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GunShop.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class GunsApiController : ControllerBase
    {
        private readonly GunShopContext _context;

        public GunsApiController(GunShopContext context)
        {
            _context = context;
        }

        // GET: api/GunsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gun>>> GetGuns()
        {
            return await _context.Gun.ToListAsync();
        }

        // GET: api/GunsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Gun>> GetGun(int id)
        {
            var gun = await _context.Gun.FindAsync(id);

            if (gun == null)
            {
                return NotFound();
            }

            return gun;
        }

        // PUT: api/GunsApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGun(int id, Gun gun)
        {
            if (id != gun.Id)
            {
                return BadRequest();
            }

            _context.Entry(gun).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GunExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/GunsApi
        [HttpPost]
        public async Task<ActionResult<Gun>> PostGun(Gun gun)
        {
            _context.Gun.Add(gun);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGun", new { id = gun.Id }, gun);
        }

        // DELETE: api/GunsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGun(int id)
        {
            var gun = await _context.Gun.FindAsync(id);
            if (gun == null)
            {
                return NotFound();
            }

            _context.Gun.Remove(gun);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GunExists(int id)
        {
            return _context.Gun.Any(e => e.Id == id);
        }
    }
}
