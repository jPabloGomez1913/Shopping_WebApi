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
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<State> States { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
            //Indice compuesto
            //La ciudad no se repiote para el mismo estado
            modelBuilder.Entity<State>().HasIndex("Name","CountryId").IsUnique();
            modelBuilder.Entity<City>().HasIndex("Name","StateId").IsUnique();

        }
    }
}
