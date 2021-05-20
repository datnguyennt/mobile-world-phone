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

		public virtual DbSet<About> About { get; set; }
		public virtual DbSet<Contact> Contact { get; set; }
		public virtual DbSet<Feedback> Feedback { get; set; }
		public virtual DbSet<Footer> Footer { get; set; }
		public virtual DbSet<Menu> Menu { get; set; }
		public virtual DbSet<MenuType> MenuType { get; set; }
		public virtual DbSet<New> New { get; set; }
		public virtual DbSet<NewCategory> NewCategory { get; set; }
		public virtual DbSet<ProductCategory> ProductCategory { get; set; }
		public virtual DbSet<Products> Products { get; set; }
		public virtual DbSet<Slider> Slider { get; set; }
		public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
		public virtual DbSet<SystemConfig> SystemConfig { get; set; }
		public virtual DbSet<Tag> Tag { get; set; }
		public virtual DbSet<User> User { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<About>()
				.Property(e => e.MetaTitle)
				.IsUnicode(false);

			modelBuilder.Entity<About>()
				.Property(e => e.CreatedBy)
				.IsUnicode(false);

			modelBuilder.Entity<About>()
				.Property(e => e.ModifiedBy)
				.IsUnicode(false);

			modelBuilder.Entity<Footer>()
				.Property(e => e.FooterID)
				.IsUnicode(false);

			modelBuilder.Entity<New>()
				.Property(e => e.MetaTitle)
				.IsUnicode(false);

			modelBuilder.Entity<New>()
				.Property(e => e.CreatedBy)
				.IsUnicode(false);

			modelBuilder.Entity<New>()
				.Property(e => e.ModifiedBy)
				.IsUnicode(false);

			modelBuilder.Entity<New>()
				.Property(e => e.TagID)
				.IsUnicode(false);

			modelBuilder.Entity<New>()
				.HasMany(e => e.Tag)
				.WithMany(e => e.New)
				.Map(m => m.ToTable("NewTag").MapLeftKey("NewID").MapRightKey("TagID"));

			modelBuilder.Entity<NewCategory>()
				.Property(e => e.MetaTitle)
				.IsUnicode(false);

			modelBuilder.Entity<NewCategory>()
				.Property(e => e.CreatedBy)
				.IsUnicode(false);

			modelBuilder.Entity<NewCategory>()
				.Property(e => e.ModifiedBy)
				.IsUnicode(false);

			modelBuilder.Entity<ProductCategory>()
				.Property(e => e.MetaTitle)
				.IsUnicode(false);

			modelBuilder.Entity<ProductCategory>()
				.Property(e => e.CreatedBy)
				.IsUnicode(false);

			modelBuilder.Entity<ProductCategory>()
				.Property(e => e.ModifiedBy)
				.IsUnicode(false);

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

			modelBuilder.Entity<Products>()
				.Property(e => e.CreatedBy)
				.IsUnicode(false);

			modelBuilder.Entity<Products>()
				.Property(e => e.ModifiedBy)
				.IsUnicode(false);

			modelBuilder.Entity<Slider>()
				.Property(e => e.CreatedBy)
				.IsUnicode(false);

			modelBuilder.Entity<Slider>()
				.Property(e => e.ModifiedBy)
				.IsUnicode(false);

			modelBuilder.Entity<SystemConfig>()
				.Property(e => e.ConfigID)
				.IsUnicode(false);

			modelBuilder.Entity<Tag>()
				.Property(e => e.TagID)
				.IsUnicode(false);

			modelBuilder.Entity<User>()
				.Property(e => e.Username)
				.IsFixedLength();

			modelBuilder.Entity<User>()
				.Property(e => e.Password)
				.IsUnicode(false);

			modelBuilder.Entity<User>()
				.Property(e => e.ModifiedBy)
				.IsUnicode(false);
		}
	}
}
