using Entites;
using Microsoft.EntityFrameworkCore;

namespace DataBase
{
    public class Course : DbContext
    {
        public Course(DbContextOptions<Course> Options) : base(Options)
        {
        }
        
            
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Category> Categories { get; set; }
    }
}
