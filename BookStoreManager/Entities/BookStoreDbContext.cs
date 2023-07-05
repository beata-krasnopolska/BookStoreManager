using Microsoft.EntityFrameworkCore;

namespace BookStoreManager.Entities
{
    public class BookStoreDbContext : DbContext
    {
        private string _connectionString = "Server=(Localdb)\\mssqllocaldb;Database=BookStoreDb;Trusted_Connection=True;";
        public DbSet<BookStore> BookStores { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(r => r.Email).IsRequired();
            modelBuilder.Entity<Role>().Property(r => r.Name).IsRequired();
            modelBuilder.Entity<BookStore>().Property(r => r.Name).IsRequired().HasMaxLength(25);
            modelBuilder.Entity<Address>().Property(r => r.City).IsRequired().HasMaxLength(15);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
