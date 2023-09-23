using Microsoft.EntityFrameworkCore;
using VShop.DiscountApi.Models;

namespace VShop.DiscountApi.Context;

// dotnet tool install --global dotnet-ef
// dotnet tool update --global dotnet-ef
// dotnet build 

// dotnet ef 
// dotnet ef migrations add InicialTables
// dotnet ef database update
// dotnet ef migrations remove

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Coupon> Coupons { get; set; }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        base.OnModelCreating(mb);

        mb.Entity<Coupon>().HasKey(c => c.CouponId);
        //mb.Entity<Coupon>().Property(c => c.CouponId).ValueGeneratedNever();
        mb.Entity<Coupon>().Property(c => c.CouponCode).HasMaxLength(100).IsRequired();
        mb.Entity<Coupon>().Property(c => c.Discount).HasPrecision(10, 2);

        mb.Entity<Coupon>().HasData(new Coupon { CouponId = 1, CouponCode = "VSHOP_PROMO_10", Discount = 10 });
        mb.Entity<Coupon>().HasData(new Coupon { CouponId = 2, CouponCode = "VSHOP_PROMO_20", Discount = 20 });
        mb.Entity<Coupon>().HasData(new Coupon { CouponId = 3, CouponCode = "VSHOP_PROMO_30", Discount = 30 });
        mb.Entity<Coupon>().HasData(new Coupon { CouponId = 4, CouponCode = "VSHOP_PROMO_40", Discount = 40 });
        mb.Entity<Coupon>().HasData(new Coupon { CouponId = 5, CouponCode = "VSHOP_PROMO_50", Discount = 50 });
        mb.Entity<Coupon>().HasData(new Coupon { CouponId = 6, CouponCode = "VSHOP_PROMO_60", Discount = 60 });
        mb.Entity<Coupon>().HasData(new Coupon { CouponId = 7, CouponCode = "VSHOP_PROMO_70", Discount = 70 });
    }
}
