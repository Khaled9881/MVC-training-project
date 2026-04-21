using Microsoft.EntityFrameworkCore;

namespace MVC_Day2.Models
{
    public class SchoolContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Trainee> Trainees { get; set; }

        public DbSet<CrsResuly> CrsResults { get; set; }

        public SchoolContext() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=School;Trusted_Connection=True;TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder);
        }


    }
}
