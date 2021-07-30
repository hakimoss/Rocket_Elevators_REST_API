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
    public class CustomersController : ControllerBase
    {
        private readonly FactInterventionContext _context;

        public CustomersController(FactInterventionContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.customers.ToListAsync();
        }

        // GET: api/Customers/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<Customer>> GetCustomer(long id)
        // {
        //     var customer = await _context.customers.FindAsync(id);

        //     if (customer == null)
        //     {
        //         return NotFound();
        //     }

        //     return customer;
        // // }
        [HttpGet("{email}")]
        public async Task<ActionResult<IEnumerable<Customer>>> EmailCustomer(string email)
        {
            var customer = await _context.customers.Where(c => c.email_of_the_company_contact == email).ToListAsync(); 

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }
        [HttpGet("Email/{email}")]
        public async Task<ActionResult<Customer>> GetCustomerEmail(string email)
        {

            IEnumerable<Customer> customersAll = await _context.customers.ToListAsync();

            foreach (Customer customer in customersAll)
            {
                if (customer.email_of_the_company_contact == email)
                {
                    return customer;
                }
            }
            return NotFound();
        }

        // [HttpGet("{email}")]
        // public async Task<ActionResult<Customer>> GetCustomer(string email)
        // {
        //     var customer = await _context.customers.Include("Buildings.Batteries.Columns.Elevators")
        //                                         .Where(c => c.email_of_the_company_contact == email)
        //                                         .FirstOrDefaultAsync();  

        //     // customer = await _context.customers.Include("Buildings.Addresses")
        //     //                                     .Where(c => c.cpy_contact_email == email)
        //     //                                     .FirstOrDefaultAsync();          

        //     if (customer == null)
        //     {
        //         return NotFound();
        //     }

        //     return customer;
        // }
        // Get email for customer 

        [HttpGet("verify/{email}")]
        public async Task<ActionResult> VerifyEmail(string email)
        {
            var customer = await _context.customers.Include("Buildings.Batteries.Columns.Elevators")
                                                .Where(e => e.email_of_the_company_contact == email)
                                                .FirstOrDefaultAsync();            

            if (customer == null)
            {
                return NotFound();
            }

            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult<Customer>> PutCustomer(Customer customer)
        {
            var customerToUpdate = await _context.customers
                                                .Where(e => e.email_of_the_company_contact == customer.email_of_the_company_contact)
                                                .FirstOrDefaultAsync(); 

            if (customerToUpdate == null)
            {
                return NotFound();
            }

            customerToUpdate.compagny_name = customer.compagny_name;
            customerToUpdate.full_name_of_the_company_contact = customer.full_name_of_the_company_contact;
            customerToUpdate.company_contact_phone = customer.company_contact_phone;
            customerToUpdate.full_name_of_service_technical_authority = customer.full_name_of_service_technical_authority;
            customerToUpdate.technical_authority_phone_for_service = customer.technical_authority_phone_for_service;
            customerToUpdate.technical_manager_email_for_service = customer.technical_manager_email_for_service;
            customerToUpdate.company_description = customer.company_description;

            await _context.SaveChangesAsync();

            return customerToUpdate;
        }
    

        // PUT: api/Customers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(long id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Customers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            _context.customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(long id)
        {
            var customer = await _context.customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.customers.Remove(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        private bool CustomerExists(long id)
        {
            return _context.customers.Any(e => e.Id == id);
        }
    }
}
