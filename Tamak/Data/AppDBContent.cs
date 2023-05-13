using Microsoft.EntityFrameworkCore;
using Tamak.Data.Enum;
using Tamak.Data.Helpers;
using Tamak.Data.Models;

namespace Tamak.Data
{
    public class AppDBContent : DbContext
    {
        public AppDBContent(DbContextOptions<AppDBContent> options) : base(options) {
            Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Assortiment> Assortements { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {

            modelBuilder.Entity<User>(builder =>
            {
                builder.HasData(new User()
                {
                    Id = 1,
                    Name = "Test1",
                    Email = "Test@edu.hse.ru",
                    Password = HashPasswordHelper.HashPassowrd("Test1"),
                    Role = Role.Admin,
                    City = City.Moscow,
                    Campus = Campus.Pokrovka,
                });
                builder.ToTable("User").HasKey(x => x.Id);
                builder.Property(x => x.Id).ValueGeneratedOnAdd();
                builder.Property(x => x.Password).IsRequired();
                builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
                builder.Property(x => x.Email).HasMaxLength(100).IsRequired();

                builder.HasOne(x => x.Assortiment).WithOne(x => x.User).HasPrincipalKey<User>(x => x.Id).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Assortiment>(builder =>
            {
                builder.ToTable("Assortiments").HasKey(x => x.Id);

                builder.HasData(new Assortiment()
                {
                    Id = 1,
                    UserId = 1
                });
            });

            modelBuilder.Entity<Product>(builder =>
            {
                builder.HasData(new Product()
                {
                    Id = 1,
                    Name = "Латте",
                    Description = "Свежий кофе с молочным оттенком",
                    Price = 200,
                    Available = true,
                    Category = Category.Coffee,
                    AssortimentId = 1,
                });
                builder.ToTable("Products").HasKey(x => x.Id);
                builder.Property(x => x.Id).ValueGeneratedOnAdd();
                builder.HasOne(r => r.Assortiment).WithMany(t => t.Products).HasForeignKey(x => x.AssortimentId);
            });
        }
    }
}
