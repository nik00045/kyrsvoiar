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
    public class IotsController : ControllerBase
    {
        private readonly kyrsarbdContext _context;

        public IotsController(kyrsarbdContext context)
        {
            _context = context;
        }

        // GET: api/Iots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Iot>>> GetIot()
        {
            return await _context.Iot.ToListAsync();
        }

        // GET: api/Iots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Iot>> GetIot(int id)
        {
            var iot = await _context.Iot.FindAsync(id);

            if (iot == null)
            {
                return NotFound();
            }

            return iot;
        }

        // GET: api/Iots/GetIotbybybuilding/1
        [HttpGet("GetIotbybybuilding/{id}")]
        public async Task<ActionResult<Iot>> GetIotbybybuilding(int id)
        {
            var iot =  _context.Iot
                .Where(iotb => iotb.Idbuilding == id)
                .FirstOrDefault();

            if (iot == null)
            {
                return NotFound();
            }

            return iot;
        }


        // POST: api/Iots
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Iot>> PostIot(Iot iot)
        {
            _context.Iot.Add(iot);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (IotExists(iot.Idiot))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetIot", new { id = iot.Idiot }, iot);
        }

        // DELETE: api/Iots/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Iot>> DeleteIot(int id)
        {
            var iot = await _context.Iot.FindAsync(id);
            if (iot == null)
            {
                return NotFound();
            }

            _context.Iot.Remove(iot);
            await _context.SaveChangesAsync();

            return iot;
        }

        private bool IotExists(int id)
        {
            return _context.Iot.Any(e => e.Idiot == id);
        }
    }
}
