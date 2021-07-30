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
    public class InterventionController : ControllerBase
    {
        private readonly FactInterventionContext _context;

        public InterventionController(FactInterventionContext context)
        {
            _context = context;
        }

        // GET: api/Intervention
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Intervention>>> Getinterventions()
        {
            var offline = await _context.interventions.Where(i => i.status.Equals("Pending") || i.start_date.Equals(null)).ToListAsync();
            return offline;
        }
        
        // GET: api/Intervention/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Intervention>> GetIntervention(long id)
        {
            var intervention = await _context.interventions.FindAsync(id);

            if (intervention == null)
            {
                return NotFound();
            }

            return intervention;
        }

        // PUT: api/Intervention/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
         [HttpPut("{id}/InProgress")]
        public async Task<ActionResult<Intervention>> PutInterventionToInProgress([FromRoute] long id)
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

            return Content("The status of the intervention ID: " + intervention.id +
            " has been changed to: " + intervention.status + " and the start date as been updated to: " + intervention.start_date);
        }

        [HttpPut("{id}/Completed")]
        public async Task<ActionResult<Intervention>> PutInterventionToCompleted([FromRoute] long id)
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

            return Content("The status of the intervention ID: " + intervention.id +
            " has been changed to: " + intervention.status + " and the end date as been updated to: " + intervention.end_date);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutIntervention(long id, Intervention intervention)
        {
            if (id != intervention.id)
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

        // POST: api/Intervention
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Intervention>> PostIntervention(Intervention newIntervention)
        {
            newIntervention.start_date = null;
            newIntervention.end_date = null;
            newIntervention.status = "InProgress";
            newIntervention.result = "Incomplete";
            newIntervention.employee_id = null;

            _context.interventions.Add(newIntervention);
            await _context.SaveChangesAsync();

            return newIntervention;
        }
        

        // DELETE: api/Intervention/5
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
            return _context.interventions.Any(e => e.id == id);
        }
    }
}
