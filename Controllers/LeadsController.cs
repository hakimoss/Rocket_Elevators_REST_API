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
    public class LeadsController : ControllerBase
    {
        private readonly FactInterventionContext _context;

        public LeadsController(FactInterventionContext context)
        {
            _context = context;
        }

        // GET: api/Leads
        // [HttpGet("lastNoCheck")]
        // public IEnumerable<Lead> GetLeads()
        // {
        //     var Mydate = System.DateTime.Now.AddDays(-30);
        //     IQueryable<Lead> Leads = 
        //     from lead in _context.leads
        //     where lead.created_at  >= Mydate
        //     select lead;
        //     return Leads.ToList();
 
        // }
        // GET: api/Leads/5
        [HttpGet]
        public IEnumerable<Lead> GetLead(long id)
        {   
            var Mydate = System.DateTime.Now.AddDays(-30);
            IQueryable<Lead> Leads =
            from lead in _context.leads
            where lead.created_at  >= Mydate
            select lead;
            var customersList = _context.customers.ToList();
            var Results = Leads.ToList();

            foreach (var Slead in Leads) {
               var email = Slead.email;
                foreach (var customer in customersList) {
                    if (customer.email_of_the_company_contact == email || customer.technical_manager_email_for_service == email) {
                        Results.RemoveAll(r => r.email == customer.email_of_the_company_contact || r.email == customer.technical_manager_email_for_service);
                    }
                }
            }
            return Results; 
        }

        // PUT: api/Leads/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutLead(long id, Lead lead)
        // {
        //     if (id != lead.Id)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(lead).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!LeadExists(id))
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

        // POST: api/Leads
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        // [HttpPost]
        // public async Task<ActionResult<Lead>> PostLead(Lead lead)
        // {
        //     _context.leads.Add(lead);
        //     await _context.SaveChangesAsync();

        //     return CreatedAtAction("GetLead", new { id = lead.Id }, lead);
        // }

        // // DELETE: api/Leads/5
        // [HttpDelete("{id}")]
        // public async Task<ActionResult<Lead>> DeleteLead(long id)
        // {
        //     var lead = await _context.leads.FindAsync(id);
        //     if (lead == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.leads.Remove(lead);
        //     await _context.SaveChangesAsync();

        //     return lead;
        // }

        private bool LeadExists(long id)
        {
            return _context.leads.Any(e => e.Id == id);
        }
    }
}
