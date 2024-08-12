
using Microsoft.EntityFrameworkCore;

namespace Hrms.Models.DTO
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }

        public DbSet<ExperienceLevel> ExperienceLevels { get; set; }

        public DbSet<Emp> Employees{ get; set; }

        public DbSet<Timesheet> Timesheets { get; set; }
        public DbSet<UserLoginLogs> UserLoginLogs { get; set; }
        public DbSet<Leave> Leaves { get; set; }


    }

}
