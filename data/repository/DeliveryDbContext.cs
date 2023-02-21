
using hitsLab.data.entity;
using Microsoft.EntityFrameworkCore;

namespace hitsLab.data.repository
{
    public sealed class DeliveryDbContext: DbContext
    {
        public DbSet<DishEntity>? Dishes {get; set; }
        public DbSet<DishInOrderEntity>? DishesInOrder {get; set; }
        public DbSet<OrderEntity>? Orders {get; set; }
        public DbSet<UserEntity>? Users {get; set; }
        public DbSet<RatingEntity>? Ratings { get; set; }
        
        public DbSet<TokenEntity>? Tokens { get; set; }
        
        public DeliveryDbContext(DbContextOptions<DeliveryDbContext> options): base(options)
        {
            Database.EnsureCreated();
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DishInOrderEntity>()
                .HasIndex(u => new { u.UserId, u.OrderId, u.DishId })
                .IsUnique();
            builder.Entity<RatingEntity>()
                .HasIndex(u => new { u.UserId, u.DishId })
                .IsUnique();
            builder.Entity<UserEntity>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
