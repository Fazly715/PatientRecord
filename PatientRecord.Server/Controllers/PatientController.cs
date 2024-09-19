using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PatientRecord.Server.Data;
using PatientRecord.Server.Data.Entities;

namespace PatientRecord.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public PatientController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET api/patients
        [HttpGet]
        public async IActionResult GetPatients ()
        {
            // Get Patients from database, return to client
            return Ok(await dbContext.Patients.ToListAsync());

        }

        // GET api/patients/{id}
        [HttpGet("{id}")]
        public async IActionResult GetPatientById (int id)
        {
            // Get Patient by Id from database, return to client
            var patient = await dbContext.Patients.FirstOrDefaultAsync (x => x.Id == id);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }

        // POST api/patient
        [HttpPost]
        public async IActionResult CreatePatient([FromBody] Patient patient)
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
        public async IActionResult UpdatePatient(int id, [FromBody] Patient patient)
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
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/patient/{id}
        [HttpDelete("{id}")]
        public async IActionResult DeletePatient(int id)
        {
            // Delete Patient by Id from database 
            var pantient = await dbContext.Patients.FirstOrDefaultAsync(id);
            if (pantient == null)
            {
                return NotFound();
            }
            dbContext.Patients.Remove(pantient);
            await dbContext.SaveChangesAsync();
            return Created();
        }

    }   
}
