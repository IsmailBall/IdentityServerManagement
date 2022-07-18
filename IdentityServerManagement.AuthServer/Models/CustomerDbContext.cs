using Microsoft.EntityFrameworkCore;

namespace IdentityServerManagement.AuthServer.Models
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> contextOptions) : base(contextOptions)
        {

        }

        public DbSet<CustomUser> CustomUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomUser>().HasData(
                new CustomUser
                {
                    Id = 1,
                    UserName = "sancar",
                    Email = "sncr.@hotmail.com",
                    Password = "psw",
                    City = "Ankara"
                },
                new CustomUser
                {
                    Id = 2,
                    UserName = "sml",
                    Email = "sml.@hotmail.com",
                    Password = "psd",
                    City = "Istanbul"
                },
                new CustomUser
                {
                    Id = 3,
                    UserName = "trsp",
                    Email = "trsp.@hotmail.com",
                    Password = "sdp",
                    City = "Bursa"
                }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
