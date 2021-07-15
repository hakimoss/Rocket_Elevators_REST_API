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
    public class InterventionsController : ControllerBase
    {
        private readonly FactInterventionContext _context;

        public InterventionsController(FactInterventionContext context)
        {
            _context = context;
        }

        // GET: api/Interventions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Intervention>>> Getinterventions()
        {
            
            var pending = await _context.interventions.Where(i => i.status.Equals("Pending") || i.start_date.Equals(null)).ToListAsync();
            return pending;
        }

        // GET: api/Interventions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Intervention>> GetInterventions(long id)
        {
            var intervention = await _context.interventions.FindAsync(id);

            if (intervention == null)
            {
                return NotFound();
            }

            return intervention;
        }

        [HttpPut("{id}/inprogress")]
        public async Task<ActionResult<Intervention>> PutInterventiontoInProgress([FromRoute] long id)
        {
            
            var intervention = await this._context.interventions.FindAsync(id);


            if (intervention == null)
            {
                return NotFound();
            }
            else
            {
                intervention.status = "InProgress";
                intervention.start_date = DateTime.Now;
            }
            this._context.interventions.Update(intervention);
            await this._context.SaveChangesAsync();

            return Content("The status of the intervention ID: " + intervention.Id +
            " has been changed to: " + intervention.status + " and the date of Intervention as update to: " + intervention.start_date);
        }

        [HttpPut("{id}/completed")]
        public async Task<ActionResult<Intervention>> PutInterventiontoCompleted([FromRoute] long id)
        {
            
            var intervention = await this._context.interventions.FindAsync(id);


            if (intervention == null)
            {
                return NotFound();
            }
            else
            {
                intervention.status = "Completed";
                intervention.end_date = DateTime.Now;
            }
            this._context.interventions.Update(intervention);
            await this._context.SaveChangesAsync();

            return Content("The status of the intervention ID: " + intervention.Id +
            " has been changed to: " + intervention.status + " and the date of Intervention as update to: " + intervention.start_date);
        }

    
        // PUT: api/Interventions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIntervention(long id, Intervention intervention)
        {
            if (id != intervention.Id)
            {
                return BadRequest();
            }

            _context.Entry(intervention).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterventionExists(id))
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

        // POST: api/Interventions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Intervention>> PostIntervention(Intervention intervention)
        {
            _context.interventions.Add(intervention);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIntervention", new { id = intervention.Id }, intervention);
        }

        // DELETE: api/Interventions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Intervention>> DeleteIntervention(long id)
        {
            var intervention = await _context.interventions.FindAsync(id);
            if (intervention == null)
            {
                return NotFound();
            }

            _context.interventions.Remove(intervention);
            await _context.SaveChangesAsync();

            return intervention;
        }

        private bool InterventionExists(long id)
        {
            return _context.interventions.Any(e => e.Id == id);
        }
    }
}
