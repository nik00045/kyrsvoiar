﻿using System;
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

        //api/mobile/GetAnchorsByiot/1
        [HttpGet("GetAnchorsByiot/{id}")]
        public async Task<ActionResult<IEnumerable<Anchor>>> GetAnchorsByiot(int id)
        {
            return await _context.Anchor.Where(iotid => iotid.Idiot == id).ToListAsync();
        }

        // GET: api/mobile/GetlastAnchors
        [HttpGet("GetlastAnchors")]
        public async Task<ActionResult<Anchor>> GetlastAnchors()
        {
            var anchor =  _context.Anchor.ToList().Last();

            return anchor;
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


        // GET: api/mobile/DeleteAnchor/5
        [HttpGet("DeleteAnchor/{id}")]
        public async Task<ActionResult<Anchor>> DeleteAnchorMobile(int id)
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
