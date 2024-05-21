using Microsoft.EntityFrameworkCore;

namespace ProfileTask.Models
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
        public DbSet<Employee> Employees { get; set; }   
       public DbSet<Contacts> contacts { get; set; }   
       public DbSet<Note> notes { get; set; }   
       public DbSet<OtherTable> otherTables { get; set; }   
       public DbSet<OtherEx> OtherExperiences { get; set; }   
       public DbSet<Licenses> licenses { get; set; }   
       public DbSet<Education> educations { get; set; }   
       public DbSet<Background> backgrounds { get; set; }   

    }

}
