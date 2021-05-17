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

		public virtual DbSet<Categories> Categories { get; set; }
		public virtual DbSet<Customers> Customers { get; set; }
		public virtual DbSet<OrderDetails> OrderDetails { get; set; }
		public virtual DbSet<Orders> Orders { get; set; }
		public virtual DbSet<Products> Products { get; set; }
		public virtual DbSet<Suppliers> Suppliers { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Categories>()
				.HasMany(e => e.Products)
				.WithRequired(e => e.Categories)
				.HasForeignKey(e => e.CategoryId);

			modelBuilder.Entity<Customers>()
				.Property(e => e.Username)
				.IsUnicode(false);

			modelBuilder.Entity<Customers>()
				.Property(e => e.UserPassword)
				.IsUnicode(false);

			modelBuilder.Entity<Orders>()
				.Property(e => e.CustomerId)
				.IsUnicode(false);

			modelBuilder.Entity<Orders>()
				.HasMany(e => e.OrderDetails)
				.WithRequired(e => e.Orders)
				.HasForeignKey(e => e.OrderId);

			modelBuilder.Entity<Products>()
				.HasMany(e => e.OrderDetails)
				.WithRequired(e => e.Products)
				.HasForeignKey(e => e.ProductId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Suppliers>()
				.Property(e => e.SupplyCode)
				.IsUnicode(false);

			modelBuilder.Entity<Suppliers>()
				.HasMany(e => e.Products)
				.WithRequired(e => e.Suppliers)
				.HasForeignKey(e => e.SupplierId);
		}
	}
}
