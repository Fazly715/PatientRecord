using Microsoft.AspNetCore.Mvc;
using PatientRecord.Server.Data;

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
    }
}
