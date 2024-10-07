using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using PRM392.OnlineStore.Domain.Entities.Base;
using PRM392_OnlineStore_Domain.Interfaces;

namespace PRM392.OnlineStore.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            var migrator = this.Database.GetService<IMigrator>();
            migrator.Migrate();
        }

        // Define DbSet properties for each entity/table
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<CartEntity> Carts { get; set; }
        public DbSet<CartItemEntity> CartItems { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<PaymentEntity> Payments { get; set; }
        public DbSet<NotificationEntity> Notifications { get; set; }
        public DbSet<ChatMessageEntity> ChatMessages { get; set; }
        public DbSet<StoreLocationEntity> StoreLocations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureModel(modelBuilder);
        }

        private void ConfigureModel(ModelBuilder modelBuilder)
        {
            // Define entity configurations for each relationship

            // UserEntity Configuration
            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.Property(e => e.ID).HasMaxLength(36);
                entity.Property(e => e.Name).HasMaxLength(255);
                entity.Property(e => e.Email).HasMaxLength(255);
                entity.Property(e => e.Phone).HasMaxLength(20);
                entity.Property(e => e.Address).HasMaxLength(1000);

                entity.HasMany(u => u.Carts)
                      .WithOne(c => c.User)
                      .HasForeignKey(c => c.UserID)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.Orders)
                      .WithOne(o => o.User)
                      .HasForeignKey(o => o.UserID)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.Notifications)
                      .WithOne(n => n.User)
                      .HasForeignKey(n => n.UserID)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.ChatMessages)
                      .WithOne(cm => cm.User)
                      .HasForeignKey(cm => cm.UserID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ProductEntity Configuration
            modelBuilder.Entity<ProductEntity>(entity =>
            {
                entity.Property(e => e.ID).HasMaxLength(36);
                entity.Property(e => e.Name).HasMaxLength(255);
                entity.Property(e => e.BriefDescription).HasMaxLength(1000);
                entity.Property(e => e.FullDescription).HasMaxLength(2000);
                entity.Property(e => e.Image).HasMaxLength(1000);
                entity.Property(e => e.TechnicalSpecifications).HasMaxLength(2000);

                entity.HasMany(p => p.CartItems)
                      .WithOne(ci => ci.Product)
                      .HasForeignKey(ci => ci.ProductID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // CategoryEntity Configuration
            modelBuilder.Entity<CategoryEntity>(entity =>
            {
                entity.Property(e => e.ID).HasMaxLength(36);
                entity.Property(e => e.CategoryName).HasMaxLength(255);

                entity.HasMany(c => c.Products)
                      .WithOne(p => p.Category)
                      .HasForeignKey(p => p.CategoryID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // CartEntity Configuration
            modelBuilder.Entity<CartEntity>(entity =>
            {
                entity.Property(e => e.ID).HasMaxLength(36);
                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18,2)");

                entity.HasMany(c => c.CartItems)
                      .WithOne(ci => ci.Cart)
                      .HasForeignKey(ci => ci.CartID)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(c => c.Order)
                      .WithOne(o => o.Cart)
                      .HasForeignKey<OrderEntity>(o => o.CartID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // CartItemEntity Configuration
            modelBuilder.Entity<CartItemEntity>(entity =>
            {
                entity.Property(e => e.ID).HasMaxLength(36);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");

                entity.HasOne(ci => ci.Cart)
                      .WithMany(c => c.CartItems)
                      .HasForeignKey(ci => ci.CartID)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ci => ci.Product)
                      .WithMany(p => p.CartItems)
                      .HasForeignKey(ci => ci.ProductID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // OrderEntity Configuration
            modelBuilder.Entity<OrderEntity>(entity =>
            {
                entity.Property(e => e.ID).HasMaxLength(36);
                entity.Property(e => e.BillingAddress).HasMaxLength(1000);
                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.HasOne(o => o.User)
                      .WithMany(u => u.Orders)
                      .HasForeignKey(o => o.UserID)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(o => o.Cart)
                      .WithOne(c => c.Order)
                      .HasForeignKey<OrderEntity>(o => o.CartID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // PaymentEntity Configuration
            modelBuilder.Entity<PaymentEntity>(entity =>
            {
                entity.Property(e => e.ID).HasMaxLength(36);
                entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");
                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.HasOne(p => p.Order)
                      .WithOne(o => o.Payment)
                      .HasForeignKey<PaymentEntity>(p => p.OrderID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // NotificationEntity Configuration
            modelBuilder.Entity<NotificationEntity>(entity =>
            {
                entity.Property(e => e.ID).HasMaxLength(36);
                entity.Property(e => e.Message).HasMaxLength(1000);
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.HasOne(n => n.User)
                      .WithMany(u => u.Notifications)
                      .HasForeignKey(n => n.UserID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ChatMessageEntity Configuration
            modelBuilder.Entity<ChatMessageEntity>(entity =>
            {
                entity.Property(e => e.ID).HasMaxLength(36);
                entity.Property(e => e.Message).HasMaxLength(1000);
                entity.Property(e => e.SentAt).HasColumnType("datetime");

                entity.HasOne(cm => cm.User)
                      .WithMany(u => u.ChatMessages)
                      .HasForeignKey(cm => cm.UserID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // StoreLocationEntity Configuration
            modelBuilder.Entity<StoreLocationEntity>(entity =>
            {
                entity.Property(e => e.ID).HasMaxLength(36);
                entity.Property(e => e.Address).HasMaxLength(1000);
            });
        }
    }
}
