using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Models.EF
{
	public partial class PhoneDbContext : DbContext
	{
		public PhoneDbContext()
			: base("name=PhoneDbContext")
		{
		}

		public virtual DbSet<DonHang> DonHang { get; set; }
		public virtual DbSet<KhachHang> KhachHang { get; set; }
		public virtual DbSet<NhanVien> NhanVien { get; set; }
		public virtual DbSet<ProductCategory> ProductCategory { get; set; }
		public virtual DbSet<Products> Products { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<KhachHang>()
				.Property(e => e.UserName)
				.IsUnicode(false);

			modelBuilder.Entity<KhachHang>()
				.Property(e => e.KHPassword)
				.IsUnicode(false);

			modelBuilder.Entity<KhachHang>()
				.Property(e => e.PhoneNum)
				.IsUnicode(false);

			modelBuilder.Entity<KhachHang>()
				.Property(e => e.Email)
				.IsUnicode(false);

			modelBuilder.Entity<KhachHang>()
				.HasMany(e => e.DonHang)
				.WithOptional(e => e.KhachHang)
				.HasForeignKey(e => e.MaKH);

			modelBuilder.Entity<NhanVien>()
				.Property(e => e.UserName)
				.IsUnicode(false);

			modelBuilder.Entity<NhanVien>()
				.Property(e => e.NVPassword)
				.IsUnicode(false);

			modelBuilder.Entity<NhanVien>()
				.Property(e => e.PhoneNum)
				.IsUnicode(false);

			modelBuilder.Entity<NhanVien>()
				.Property(e => e.Email)
				.IsUnicode(false);

			modelBuilder.Entity<ProductCategory>()
				.Property(e => e.MetaTitle)
				.IsUnicode(false);

			modelBuilder.Entity<ProductCategory>()
				.Property(e => e.Image)
				.IsUnicode(false);

			modelBuilder.Entity<ProductCategory>()
				.HasMany(e => e.Products)
				.WithOptional(e => e.ProductCategory)
				.WillCascadeOnDelete();

			modelBuilder.Entity<Products>()
				.Property(e => e.ProductCode)
				.IsUnicode(false);

			modelBuilder.Entity<Products>()
				.Property(e => e.MetaTitle)
				.IsUnicode(false);

			modelBuilder.Entity<Products>()
				.Property(e => e.Price)
				.HasPrecision(18, 0);

			modelBuilder.Entity<Products>()
				.Property(e => e.PromotionPrice)
				.HasPrecision(18, 0);
		}
	}
}
