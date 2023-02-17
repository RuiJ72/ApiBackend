using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.DataAccess
{
    public class UniversityDBContext : DbContext
    {
        public UniversityDBContext(DbContextOptions<UniversityDBContext> options) : base(options)
        {

        }

        // TODO: Add DbSets ( Tables from the Database)
        public DbSet<User>? Users { get; set; }
        public DbSet<Course>? Courses { get; set; }
        public DbSet<Chapter>? Chapters  { get; set; }
        public DbSet<Category>? Students { get; set; }
        public DbSet<Student>? Categories { get; set; }
        
    }
}
