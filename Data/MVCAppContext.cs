using Microsoft.EntityFrameworkCore;
using MVCApp.Models;
namespace MVCApp.Data
{
    public class MVCAppContext : DbContext
    {
        public MVCAppContext (DbContextOptions<MVCAppContext> options) : base(options)
        {

        }
        public DbSet<Student> Student { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
    }
    
}