using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FactInterventionApi.Models;

namespace FactIntervention.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingsController : ControllerBase
    {
        private readonly FactInterventionContext _context;

        public BuildingsController(FactInterventionContext context)
        {
            _context = context;
        }

        // GET: api/Buildings
        [HttpGet("intervention")]
        public IEnumerable<Building> GetBuildingsIntervention() 
        {
            IQueryable<Building> Building = from build in _context.buildings
                join bat in _context.batteries on build.Id equals bat.Building_Id
                join col in _context.columns on bat.Id equals col.battery_Id
                join ele in _context.elevators on col.Id equals ele.column_Id
                where bat.Status == "Intervention" || col.Status == "Intervention" || ele.Status == "Intervention"
                select build;
            var result = Building.DistinctBy(i => i.Id);
            return result;
        }

        // // GET: api/Buildings/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<Building>> GetBuilding(long id)
        // {
        //     var building = await _context.buildings.FindAsync(id);

        //     if (building == null)
        //     {
        //         return NotFound();
        //     }

        //     return building;
        // }

        // // PUT: api/Buildings/5
        // // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutBuilding(long id, Building building)
        // {
        //     if (id != building.Id)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(building).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!BuildingExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return NoContent();
        // }

        // // POST: api/Buildings
        // // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        // [HttpPost]
        // public async Task<ActionResult<Building>> PostBuilding(Building building)
        // {
        //     _context.buildings.Add(building);
        //     await _context.SaveChangesAsync();

        //     return CreatedAtAction("GetBuilding", new { id = building.Id }, building);
        // }

        // // DELETE: api/Buildings/5
        // [HttpDelete("{id}")]
        // public async Task<ActionResult<Building>> DeleteBuilding(long id)
        // {
        //     var building = await _context.buildings.FindAsync(id);
        //     if (building == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.buildings.Remove(building);
        //     await _context.SaveChangesAsync();

        //     return building;
        // }

        private bool BuildingExists(long id)
        {
            return _context.buildings.Any(e => e.Id == id);
        }
    }
}
