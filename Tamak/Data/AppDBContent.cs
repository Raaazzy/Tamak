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
        public DbSet<Time> Times { get; set; }
        public DbSet<Assortiment> Assortements { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Order> Orders { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {

            modelBuilder.Entity<User>(builder =>
            {
                builder.ToTable("User").HasKey(x => x.Id);
                builder.Property(x => x.Id).ValueGeneratedOnAdd();
                builder.Property(x => x.Password).IsRequired();
                builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
                builder.Property(x => x.Email).HasMaxLength(100).IsRequired();

                builder.HasOne(x => x.Assortiment).WithOne(x => x.User).HasPrincipalKey<User>(x => x.Id).OnDelete(DeleteBehavior.Cascade);

                builder.HasOne(x => x.Basket)
                    .WithOne(x => x.User)
                    .HasPrincipalKey<User>(x => x.Id)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Assortiment>(builder =>
            {
                builder.ToTable("Assortiments").HasKey(x => x.Id);
            });

            modelBuilder.Entity<Product>(builder =>
            {
                builder.ToTable("Products").HasKey(x => x.Id);
                builder.Property(x => x.Id).ValueGeneratedOnAdd();
                builder.HasOne(r => r.Assortiment).WithMany(t => t.Products).HasForeignKey(x => x.AssortimentId);
            });

            modelBuilder.Entity<Basket>(builder =>
            {
                builder.ToTable("Baskets").HasKey(x => x.Id);
            });

            modelBuilder.Entity<Order>(builder =>
            {
                builder.ToTable("Orders").HasKey(x => x.Id);
                builder.HasOne(r => r.Basket).WithMany(t => t.Orders)
                    .HasForeignKey(r => r.BasketId);
            });

            modelBuilder.Entity<Time>(builder =>
            {
                builder.ToTable("Times").HasKey(x => x.Id);
                builder.Property(x => x.Id).ValueGeneratedOnAdd();
                builder.HasOne(r => r.Assortiment).WithMany(t => t.Times)
                    .HasForeignKey(r => r.AssortimentId);
            });
        }
    }
}
