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
        // [HttpGet("intervention")]
        // public IEnumerable<Building> GetBuildingsIntervention() 
        // {
        //     IQueryable<Building> Building = from build in _context.buildings
        //         join bat in _context.batteries on build.Id equals bat.building_id
        //         join col in _context.columns on bat.Id equals col.battery_id
        //         join ele in _context.elevators on col.Id equals ele.column_id
        //         where bat.status == "Intervention" || col.Status == "Intervention" || ele.Status == "Intervention"
        //         select build;
        //     var result = Building.DistinctBy(i => i.Id);
        //     return result;
        // }

        [HttpGet("{CustomerID}")]
        public async Task<ActionResult<IEnumerable<Building>>> Building(long customerID)
        {
            var customer = await _context.buildings.Where(b => b.customer_Id == customerID).ToListAsync(); 

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }
        [HttpGet("{email}/building")]
        public IEnumerable<Building> BuildingsCostumer([FromRoute] string email)
        {
            var customer_ = _context.customers.Where(c => c.email_of_the_company_contact.Equals(email));
            Customer customer = customer_.FirstOrDefault();      

            IEnumerable<Building> Buil =
              (from building in _context.buildings join customers in _context.customers on building.customer_Id equals customer.Id
               select building).Take(10);
            return Buil.Distinct().ToList();

        }
        //GET: api/Building/Intervention
        [HttpGet ("Intervention")]
        public ActionResult<IEnumerable<Building>> GetIntervention()
        {
            var AllBatteries = _context.batteries.ToList();
            var AllBuildings = _context.buildings.ToList();
            var AllColumns = _context.columns.ToList();
            var AllElevators = _context.elevators.ToList();
            List<Column> interventionColumn = new List<Column>();
            List<Elevator> interventionElevator = new List<Elevator>();
            List<Battery> interventionBattery = new List<Battery>();
            List<Building> buildingsIntervention = new List<Building>();

            
            foreach (Elevator elevator in AllElevators) // Check if any elevator status = intervention
            {
                if (elevator.Status == "Intervention")
                {
                    Int64 counter = 0;
                    foreach (Elevator E in interventionElevator)
                    {
                        if (E.Id == elevator.column_id)
                        {
                            counter++;
                        }
                    }
                    if (counter == 0)
                    {
                        interventionElevator.Add(elevator);
                        Console.WriteLine(interventionElevator);
                    }
                }
            }

            foreach (Elevator ele in interventionElevator) // Add the elevator's column in intervention column
            {
                foreach (Column col in AllColumns)
                {
                    if (col.Id == ele.column_id)
                    {
                        interventionColumn.Add(col);
                        Console.WriteLine(interventionColumn);
                    }
                }
            }

            foreach (Column column in AllColumns) // Check if any column status = intervention
            {
                if (column.Status == "Intervention")
                {
                    Int64 counter = 0;
                    foreach (Column C in interventionColumn)
                    {
                        if (C.Id == column.battery_id)
                        {
                            counter++;
                        }
                    }
                    if (counter == 0)
                    {
                        interventionColumn.Add(column);
                        Console.WriteLine(interventionColumn);
                    }
                }
            }

            foreach (Column columns in interventionColumn) // Add the column's battery in intervention battery
            {
                foreach (Battery bat in AllBatteries)
                {
                    if (bat.Id == columns.battery_id)
                    {
                        interventionBattery.Add(bat);
                        Console.WriteLine(interventionBattery);
                    }
                }
            }

            foreach (Battery battery in AllBatteries) // Check if any battery status = intervention
            {
                if (battery.status == "Intervention")
                {
                    Int64 counter = 0;
                    foreach (Battery BA in interventionBattery)
                    {
                        if (BA.Id == battery.building_id)
                        {
                            counter++;
                        }
                    }
                    if (counter == 0)
                    {
                        interventionBattery.Add(battery);
                        Console.WriteLine(interventionBattery);
                    }
                }
            }

            foreach (Battery batteries in interventionBattery) // Add the Battery's Building in intervention battery
            {
                foreach (Building building in AllBuildings)
                {
                    if (building.Id == batteries.building_id)
                    // Console.WriteLine(building.Id);
                    // Console.WriteLine(batteries.BuildingId);
                    {
                        buildingsIntervention.Add(building);
                        Console.WriteLine(buildingsIntervention);
                    }
                }
            }
            buildingsIntervention = buildingsIntervention.OrderBy(o=>o.Id).ToList();
            List<Building> buildingsInterventionNoDup = buildingsIntervention.Distinct().ToList();
            return buildingsInterventionNoDup;
        }
        // GET: api/Buildings/5
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

        // PUT: api/Buildings/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuilding(long id, Building building)
        {
            if (id != building.Id)
            {
                return BadRequest();
            }

            _context.Entry(building).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuildingExists(id))
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

        // POST: api/Buildings
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Building>> PostBuilding(Building building)
        {
            _context.buildings.Add(building);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBuilding", new { id = building.Id }, building);
        }

        // DELETE: api/Buildings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Building>> DeleteBuilding(long id)
        {
            var building = await _context.buildings.FindAsync(id);
            if (building == null)
            {
                return NotFound();
            }

            _context.buildings.Remove(building);
            await _context.SaveChangesAsync();

            return building;
        }

        private bool BuildingExists(long id)
        {
            return _context.buildings.Any(e => e.Id == id);
        }
    }
}
