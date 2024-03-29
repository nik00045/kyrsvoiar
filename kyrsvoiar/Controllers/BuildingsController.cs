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
    public class BuildingsController : ControllerBase
    {
        private readonly kyrsarbdContext _context;

        public BuildingsController(kyrsarbdContext context)
        {
            _context = context;
        }

        // GET: api/Buildings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Building>>> GetBuilding()
        {
            return await _context.Building.ToListAsync();
        }

        // GET: api/Buildings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Building>> GetBuilding(int id)
        {
            var building = await _context.Building.FindAsync(id);

            if (building == null)
            {
                return NotFound();
            }

            return building;
        }

        // GET: api/Buildings/GetAdmcode/qweqwe
        [HttpGet("GetAdmcode/{Admincode}")]
        public async Task<ActionResult<Building>> GetAdmcode(string Admincode)
        {
            var building = _context.Building
                .Where(builb => builb.Admincode == Admincode)
                .FirstOrDefault();

            if (building == null)
            {
                return NotFound();
            }

            return building;
        }

        // GET: api/Buildings/Getbuild/1
        [HttpGet("Getbuild/{id}")]
        public async Task<ActionResult<Building>> Getbuildbyowner(int id)
        {
            var building = _context.Building
                .Where(builb => builb.Idowner == id)
                .FirstOrDefault();

            if (building == null)
            {
                return NotFound();
            }

            return building;
        }


        // POST: api/Buildings
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Building>> PostBuilding(Building building)
        {
            _context.Building.Add(building);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BuildingExists(building.Idbuilding))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBuilding", new { id = building.Idbuilding }, building);
        }

        // DELETE: api/Buildings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Building>> DeleteBuilding(int id)
        {
            var building = await _context.Building.FindAsync(id);
            if (building == null)
            {
                return NotFound();
            }

            _context.Building.Remove(building);
            await _context.SaveChangesAsync();

            return building;
        }

        private bool BuildingExists(int id)
        {
            return _context.Building.Any(e => e.Idbuilding == id);
        }
    }
}
