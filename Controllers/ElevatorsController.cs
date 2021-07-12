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
    public class ElevatorsController : ControllerBase
    {
        private readonly FactInterventionContext _context;

        public ElevatorsController(FactInterventionContext context)
        {
            _context = context;
        }

        // GET: api/Elevators
        [HttpGet("offline")]
        public async Task<ActionResult<IEnumerable<Elevator>>> GetElevators()
        {
           var offline = await _context.elevators.Where(e => e.Status.Equals("Intervention") || e.Status.Equals("Inactive")).ToListAsync();
            return offline;
        }

        // GET: api/Elevators/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Elevator>> GetElevator(long id)
        {
            var elevator = await _context.elevators.FindAsync(id);

            if (elevator == null)
            {
                return NotFound();
            }

            return Content("The status of this Elevator is currently:" + elevator.Status);
        }

        // PUT: api/Elevators/5/status
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}/inactive")]
        public async Task<ActionResult<Elevator>> PutElevatortoInactive([FromRoute] long id)
        {
            var elevator = await this._context.elevators.FindAsync(id);
            if (elevator == null)
            {
                return NotFound();
            }
            else
            {
                elevator.Status = "Inactive";
            }
            this._context.elevators.Update(elevator);
            await this._context.SaveChangesAsync();

            return Content("The status of the elevator ID: " + elevator.Id +
            " has been changed to: " + elevator.Status);
        }

        [HttpPut("{id}/active")]
        public async Task<ActionResult<Elevator>> PutElevatortoActive([FromRoute] long id)
        {
            var elevator = await this._context.elevators.FindAsync(id);
            if (elevator == null)
            {
                return NotFound();
            }
            else
            {
                elevator.Status = "Active";
            }
            this._context.elevators.Update(elevator);
            await this._context.SaveChangesAsync();

            return Content("The status of the elevator ID: " + elevator.Id +
            " has been changed to: " + elevator.Status);
        }

        [HttpPut("{id}/intervention")]
        public async Task<ActionResult<Elevator>> PutElevatortoIntervention([FromRoute] long id)
        {
            var elevator = await this._context.elevators.FindAsync(id);
            if (elevator == null)
            {
                return NotFound();
            }
            else
            {
                elevator.Status = "Intervention";
            }
            this._context.elevators.Update(elevator);
            await this._context.SaveChangesAsync();

            return Content("The status of the elevator ID: " + elevator.Id +
            " has been changed to: " + elevator.Status);
        }

        // POST: api/Elevators
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        // [HttpPost]
        // public async Task<ActionResult<Elevator>> PostElevator(Elevator elevator)
        // {
        //     _context.elevators.Add(elevator);
        //     await _context.SaveChangesAsync();

        //     return CreatedAtAction("GetElevator", new { id = elevator.Id }, elevator);
        // }

        // // DELETE: api/Elevators/5
        // [HttpDelete("{id}")]
        // public async Task<ActionResult<Elevator>> DeleteElevator(long id)
        // {
        //     var elevator = await _context.elevators.FindAsync(id);
        //     if (elevator == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.elevators.Remove(elevator);
        //     await _context.SaveChangesAsync();

        //     return elevator;
        // }

        private bool ElevatorExists(long id)
        {
            return _context.elevators.Any(e => e.Id == id);
        }
    }
}
