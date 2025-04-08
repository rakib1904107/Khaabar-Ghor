using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Khaabar_Ghor.Models.Domain;


namespace Khaabar_Ghor.Data
{
    public class KhaabarGhorDbContext : IdentityDbContext<User>
    {
        public KhaabarGhorDbContext(DbContextOptions<KhaabarGhorDbContext> options)
       : base(options)
        {
        }


        public DbSet<Menu> Menus { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Cart> Carts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Customize Identity table names (optional)
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

            // Configure Cart relationship
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Menu)
                .WithMany()
                .HasForeignKey(c => c.MenuId);

            // Configure OrderItem relationships
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Menu)
                .WithMany()
                .HasForeignKey(oi => oi.MenuId);

            modelBuilder.Entity<OrderItem>()
                .HasOne<Order>()
                .WithMany()
                .HasForeignKey(oi => oi.OrderId);
        }
    }
}
