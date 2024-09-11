using Microsoft.EntityFrameworkCore;
using PatientRecord.Server.Data.Entities;
using System.Reflection;

namespace PatientRecord.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        //public DbSet<TodoList> TodoLists => Set<TodoList>();
        public DbSet<Patient>   Patients => Set<Patient>();


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
