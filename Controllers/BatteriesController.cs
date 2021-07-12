using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FactInterventionApi.Models;

namespace FactIntervention.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatteriesController : ControllerBase
    {
        private readonly FactInterventionContext _context;

        public BatteriesController(FactInterventionContext context)
        {
            _context = context;
        }

        // // GET: api/Batteries/all
        // [HttpGet("all")]
        // public async Task<ActionResult<IEnumerable<Battery>>> GetBatteries()
        // {
        //     return await _context.batteries.ToListAsync();
        // }

        // GET: api/Batteries/(id)
        [HttpGet("{id}")]
        public async Task<ActionResult<Battery>> GetBattery(long id)
        {
            var battery = await _context.batteries.FindAsync(id);

            if (battery == null)
            {
                return NotFound();
            }

            return Content("The status of this Battery is currently:" + battery.Status);
        }

        // PUT: api/Batteries/5    //Put Request must be the full body, not just the update
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}/inactive")]
        public async Task<ActionResult<Battery>> PutBatterytoInactive([FromRoute] long id)
        {
            var battery = await this._context.batteries.FindAsync(id);
            if (battery == null)
            {
                return NotFound();
            }
            else
            {
                battery.Status = "Inactive";
            }
            this._context.batteries.Update(battery);
            await this._context.SaveChangesAsync();

            return Content("The status of the Battery ID: " + battery.Id +
            " has been changed to: " + battery.Status);
        }

        [HttpPut("{id}/active")]
        public async Task<ActionResult<Battery>> PutBatterytoActive([FromRoute] long id)
        {
            var battery = await this._context.batteries.FindAsync(id);
            if (battery == null)
            {
                return NotFound();
            }
            else
            {
                battery.Status = "Active";
            }
            this._context.batteries.Update(battery);
            await this._context.SaveChangesAsync();

            return Content("The status of the Battery ID: " + battery.Id +
            " has been changed to: " + battery.Status);
        }

        [HttpPut("{id}/intervention")]
        public async Task<ActionResult<Battery>> PutBatterytoIntervention([FromRoute] long id)
        {
            var battery = await this._context.batteries.FindAsync(id);
            if (battery == null)
            {
                return NotFound();
            }
            else
            {
                battery.Status = "Intervention";
            }
            this._context.batteries.Update(battery);
            await this._context.SaveChangesAsync();

            return Content("The status of the Battery ID: " + battery.Id +
            " has been changed to: " + battery.Status);
        }
        // POST: api/Batteries
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        // [HttpPost]
        // public async Task<ActionResult<Battery>> PostBattery(Battery battery)
        // {
        //     _context.batteries.Add(battery);
        //     await _context.SaveChangesAsync();

        //     //return CreatedAtAction("GetBattery", new { id = battery.Id }, battery);
        //     return CreatedAtAction(nameof(GetBatteries), new { id = battery.Id }, battery);
        // }

        // DELETE: api/Batteries/5
        // [HttpDelete("{id}")]
        // public async Task<ActionResult<Battery>> DeleteBattery(long id)
        // {
        //     var battery = await _context.Batteries.FindAsync(id);
        //     if (battery == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.Batteries.Remove(battery);
        //     await _context.SaveChangesAsync();

        //     return battery;
        // }

        private bool BatteryExists(long id)
        {
            return _context.batteries.Any(e => e.Id == id);
        }
    }
}
