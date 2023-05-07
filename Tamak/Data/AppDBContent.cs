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

        public DbSet<Profile> Profiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<User>(builder =>
            {
                builder.HasData(new User
                {
                    Id = 1,
                    Name = "Test1",
                    Email = "Test@edu.hse.ru",
                    Password = HashPasswordHelper.HashPassowrd("Test1"),
                    Role = Role.Admin,
                    City = City.Moscow,
                    Campus = Campus.Pokrovka

                });
                builder.ToTable("User").HasKey(x => x.Id);
                builder.Property(x => x.Id).ValueGeneratedOnAdd();
                builder.Property(x => x.Password).IsRequired();
                builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
                builder.Property(x => x.Email).HasMaxLength(100).IsRequired();

                builder.HasOne(x => x.Profile).WithOne(x => x.User).HasPrincipalKey<User>(x => x.Id).OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<Product>(builder =>
            {
                builder.HasData(new Product
                {
                    Id = 1,
                    Name = "Латте",
                    Description = "Свежий кофе с молочным оттенком",
                    //Img = "https://dodopizza-a.akamaihd.net/static/Img/Products/d45790074f574ccfa9c75884dfe55f09_584x584.webp",
                    Price = 200,
                    Available = true,
                    Category = Category.Coffee
                });
                builder.ToTable("Products").HasKey(x => x.Id);
                builder.Property(x => x.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Profile>(builder =>
            {
                builder.ToTable("Profiles").HasKey(x => x.Id);
                builder.Property(x => x.Id).ValueGeneratedOnAdd();
                builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
                builder.Property(x => x.City).IsRequired();
                builder.Property(x => x.Campus).IsRequired();

            });
        }
    }
}
