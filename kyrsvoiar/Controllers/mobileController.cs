using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using kyrsvoiar.Models;

namespace kyrsvoiar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class mobileController : ControllerBase
    {
        private readonly kyrsarbdContext _context;

        public mobileController(kyrsarbdContext context)
        {
          //  var contex = new mobilearContext();
            _context = context;
        }

        // GET: api/mobile
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Anchor>>> GetAnchors()
        {
            return await _context.Anchor.ToListAsync();
        }

        // GET: api/mobile/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Anchor>> GetAnchor(int id)
        {
            var anchor = await _context.Anchor.FindAsync(id);

            if (anchor == null)
            {
                return NotFound();
            }

            return anchor;
        }


        // PUT: api/mobile/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnchor(int id, Anchor anchor)
        {
            if (id != anchor.Idanchor)
            {
                return BadRequest();
            }

            _context.Entry(anchor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnchorExists(id))
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

        // POST: api/mobile
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Anchor>> PostAnchor(Anchor anchor)
        {
            _context.Anchor.Add(anchor);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AnchorExists(anchor.Idanchor))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAnchor", new { id = anchor.Idanchor }, anchor);
        }

        // DELETE: api/mobile/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Anchor>> DeleteAnchor(int id)
        {
            var anchor = await _context.Anchor.FindAsync(id);
            if (anchor == null)
            {
                return NotFound();
            }

            _context.Anchor.Remove(anchor);
            await _context.SaveChangesAsync();

            return anchor;
        }

        private bool AnchorExists(int id)
        {
            return _context.Anchor.Any(e => e.Idanchor == id);
        }
    }
}
