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
        [HttpGet("{BuildingID}")]
        public async Task<ActionResult<IEnumerable<Battery>>> Battery(long BuildingID)
        {
            var customer = await _context.batteries.Where(b => b.building_id == BuildingID).ToListAsync(); 

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }
        [HttpGet("{email}/battery")]
        public IEnumerable<Battery> BatteryCostumer([FromRoute] string email)
        {
            var customer_ = _context.customers.Where(c => c.email_of_the_company_contact.Equals(email));
            Customer customer = customer_.FirstOrDefault();          
            IEnumerable<Battery> Bat =
            (from battery in _context.batteries join building in _context.buildings on battery.building_id equals building.Id
             where building.customer_Id == customer.Id orderby building.created_at select battery).Take(5);
            return Bat.Distinct().ToList();
        }
        // [HttpGet("{id}")]
        // public async Task<ActionResult<Battery>> GetBattery(long id)
        // {
        //     var battery = await _context.batteries.FindAsync(id);

        //     if (battery == null)
        //     {
        //         return NotFound();
        //     }

        //     return Content("The status of this Battery is currently:" + battery.status);
        // }

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
                battery.status = "Inactive";
            }
            this._context.batteries.Update(battery);
            await this._context.SaveChangesAsync();

            return Content("The status of the Battery ID: " + battery.Id +
            " has been changed to: " + battery.status);
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
                battery.status = "Active";
            }
            this._context.batteries.Update(battery);
            await this._context.SaveChangesAsync();

            return Content("The status of the Battery ID: " + battery.Id +
            " has been changed to: " + battery.status);
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
                battery.status = "Intervention";
            }
            this._context.batteries.Update(battery);
            await this._context.SaveChangesAsync();

            return Content("The status of the Battery ID: " + battery.Id +
            " has been changed to: " + battery.status);
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
