using EmployeeMgmt.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMgmt.Infrastructure
{
    public class EmployeeDbContext: IdentityDbContext<IdentityUser>
    {

        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {
        }


        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
