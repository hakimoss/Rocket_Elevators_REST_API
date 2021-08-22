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
    public class UsersController : ControllerBase
    {
        private readonly FactInterventionContext _context;

        public UsersController(FactInterventionContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _context.users.ToListAsync();
        }

        // [HttpGet("email/{email}")]
        // public async Task<ActionResult<string>> GetUserEmail(string email)
        // {
        //     IEnumerable<Users> usersAll = await _context.users.ToListAsync();

        //     foreach (Users user in usersAll)
        //     {
        //         if (user.email == email)
        //         {
        //             return user.email;
        //         }
        //     }
        //     return NotFound();
        // }

        [HttpGet("find/{email}")]
        public ActionResult<Users> GetEmployeeEmail(string email)
        {
            var decodedEmail = System.Web.HttpUtility.UrlDecode(email);
            Console.WriteLine(decodedEmail);
            var employeeEmail = _context.users
            .Where(u => u.email == decodedEmail);
            //.FirstOrDefaultAsync();
            if (employeeEmail == null)
            {
                return NotFound();
            }
            return Ok(employeeEmail);
        }

        // GET: api/Users/email
        // [HttpGet("email/{email}")]
        // public async Task<ActionResult<Users>> Getuser(string email)
        // {
        //     var user = await _context.users.FindAsync(email);

        //     if (user == null)
        //     {
        //         return NotFound();
        //     }

        //     return user;
        // }


        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        // [HttpPost]
        // public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        // {
        //     _context.employees.Add(employee);
        //     await _context.SaveChangesAsync();

        //     return CreatedAtAction("GetUsers", new { id = employee.Id }, employee);
        // }



        // private bool UsersExists(long id)
        // {
        //     return _context.users.Any(e => e.Id == id);
        // }
    }
}