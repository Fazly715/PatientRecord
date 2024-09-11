using Microsoft.AspNetCore.Mvc;
using PatientRecord.Server.Data;

namespace PatientRecord.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicalHistoryController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public MedicalHistoryController(ApplicationDbContext dbContext)
        {
              this.dbContext = dbContext;
        }
    }
}
