using Microsoft.EntityFrameworkCore;
using ShoppingWebApi.DAL.Entities;


namespace ShoppingWebApi.DAL
{
    public class DataBaseContext : DbContext
    {
        
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) 
        {
                
        }
        // creaccion de la tabla countries
        
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique();
        }
    }
}
