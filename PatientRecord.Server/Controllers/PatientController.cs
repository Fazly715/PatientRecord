using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PatientRecord.Server.Data;
using PatientRecord.Server.Data.Entities;

namespace PatientRecord.Server.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PatientController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public PatientController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET api/patients
        [HttpGet]
        public async Task<IActionResult>GetPatients()
        {
            // Get Patients from database, return to client
            return Ok(await dbContext.Patients.ToListAsync());

        }

        // GET api/patients/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientById (int id)
        {
            // Get Patient by Id from database, return to client
            var patient = await dbContext.Patients.FirstOrDefaultAsync(x => x.Id == id);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }

        // GET api/patients/{email}
        [HttpGet("{email}")]
        public async Task<IActionResult> GetPatientByEmail (string email)
        {
            // Get Patient by Email from database, return to client
            var patient = await dbContext.Patients.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }

        // GET api/patients/{search}
        [HttpGet("{search}")]
        public async Task<IActionResult> SearchPatientByName (string search)
        {
            // Get Patient by Search from database, return to client
            var patients = await dbContext.Patients.Where(p => p.Name.ToLower().Contains(search.ToLower()))
                                  .ToListAsync();
            // p.Name = Carlos,search = car 
            if (!patients.Any())
            {
                return NotFound();
            }
            return Ok(patients);
        }

        // POST api/patient
        [HttpPost]
        public async Task<IActionResult> CreatePatient([FromBody] Patient patient)
        {
            // Post Patient from client, add to database
            if (patient == null)
            {
                return BadRequest();
            }
            await dbContext.Patients.AddAsync(patient);
            await dbContext.SaveChangesAsync();
            return Created();

        }

        // PUT api/patient/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] Patient patient)
        {
            // Put Patient from client, search Patient by Id from database, Update patient in database 
            if (id != patient.Id)
            {
                return NotFound();
            }

            try
            {
                dbContext.Patients.Update(patient);
                await dbContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/patient/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            // Delete Patient by Id from database 
            var pantient = await dbContext.Patients.FirstOrDefaultAsync(x => x.Id == id);
            if (pantient == null)
            {
                return NotFound();
            }
            dbContext.Patients.Remove(pantient);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

    }   
}
