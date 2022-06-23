using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using kyrsvoiar.Models;
using Microsoft.AspNetCore.Authorization;

namespace kyrsvoiar.Controllers
{

    [Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly kyrsarbdContext _context;

        public OwnersController(kyrsarbdContext context)
        {
            _context = context;
        }

        // GET: api/Owners
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Owner>>> GetOwner()
        {
            return await _context.Owner.ToListAsync();
        }

        // GET: api/Owners/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Owner>> GetOwner(int id)
        {
            var owner = await _context.Owner.FindAsync(id);

            if (owner == null)
            {
                return NotFound();
            }

            return owner;
        }

        // GET: api/Owners/Getuser
        [HttpGet("Getuser")]
        public async Task<ActionResult<Owner>> GetUser()
        {
            string email = HttpContext.User.Identity.Name;

            var owner = await _context.Owner.Where(owner => owner.Mail == email).FirstOrDefaultAsync();

            if (owner == null)
            {
                return NotFound();
            }

            return owner;
        }


        // POST: api/Owners
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Owner>> PostOwner(Owner owner)
        {
            _context.Owner.Add(owner);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OwnerExists(owner.Idowner))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOwner", new { id = owner.Idowner }, owner);
        }

        // DELETE: api/Owners/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Owner>> DeleteOwner(int id)
        {
            var owner = await _context.Owner.FindAsync(id);
            if (owner == null)
            {
                return NotFound();
            }

            _context.Owner.Remove(owner);
            await _context.SaveChangesAsync();

            return owner;
        }

        private bool OwnerExists(int id)
        {
            return _context.Owner.Any(e => e.Idowner == id);
        }
    }
}
