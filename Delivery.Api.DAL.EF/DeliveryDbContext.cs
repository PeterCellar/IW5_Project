using Delivery.Api.DAL.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Api.DAL.EF
{
    public class DeliveryDbContext : DbContext
    {
        public DbSet<DishEntity> Dishes { get; set; } = null!;
        public DbSet<OrderEntity> Orders { get; set; } = null!;
        public DbSet<RestaurantEntity> Restaurants { get; set; } = null!;
        public DbSet<DishAmountEntity> DishAmounts { get; set; } = null!;
        public DbSet<DishAmountEntity> DishAllergens { get; set; } = null!;

        public DeliveryDbContext(DbContextOptions<DeliveryDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DishEntity>()
               .HasOne(dishEntity => dishEntity.Restaurant)
               .WithMany(restaurantEntity => restaurantEntity.Dishes);

            modelBuilder.Entity<OrderEntity>()
                .HasMany(orderEntity => orderEntity.DishAmounts)
                .WithOne(dishAmountEntity => dishAmountEntity.Order)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RestaurantEntity>()
                .HasMany(restaurantEntity => restaurantEntity.Orders)
                .WithOne(orderEntity => orderEntity.Restaurant)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DishAmountEntity>()
                .HasOne(dishAmountEntity => dishAmountEntity.Dish)
                .WithMany(dishEntity => dishEntity.DishAmounts);

            modelBuilder.Entity<DishAllergenEntity>()
                .HasOne(dishAllergenEntity => dishAllergenEntity.Dish)
                .WithMany(dishEntity => dishEntity.Allergens);
        }
    }
}
