using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GunShop.Data;
using GunShop.Models;

namespace GunShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Guns1Controller : ControllerBase
    {
        private readonly GunShopContext _context;

        public Guns1Controller(GunShopContext context)
        {
            _context = context;
        }

        // GET: api/guns1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gun>>> GetGuns()
        {
            return await _context.Gun.ToListAsync();
        }

        // GET: api/guns1/5
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

        // POST: api/guns1
        [HttpPost]
        public async Task<ActionResult<Gun>> PostGun(Gun gun)
        {
            _context.Gun.Add(gun);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGun), new { id = gun.Id }, gun);
        }

        // PUT: api/guns1/5
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

        // DELETE: api/guns1/5
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
