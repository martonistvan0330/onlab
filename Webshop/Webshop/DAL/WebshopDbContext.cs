using Microsoft.EntityFrameworkCore;

namespace Webshop.DAL
{
    public class WebshopDbContext : DbContext
    {
        public WebshopDbContext()
        {
        }

        public WebshopDbContext(DbContextOptions<WebshopDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<PaymentInfo> PaymentInfo { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethod { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductStock> ProductStock { get; set; }
        public virtual DbSet<ShippingInfo> ShippingInfo { get; set; }
        public virtual DbSet<ShippingMethod> ShippingMethod { get; set; }
        public virtual DbSet<Size> Size { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Vat> Vat { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Webshop;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FirstName).HasMaxLength(50);
                entity.Property(e => e.LastName).HasMaxLength(50);
                entity.Property(e => e.Country).HasMaxLength(50);
                entity.Property(e => e.Region).HasMaxLength(50);
                entity.Property(e => e.ZipCode).HasMaxLength(10);
                entity.Property(e => e.City).HasMaxLength(50);
                entity.Property(e => e.Street).HasMaxLength(50);
                entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.ParentCategoryId).HasColumnName("ParentCategoryID");

                entity.HasOne(d => d.ParentCategory)
                    .WithMany(p => p.InverseParentCategory)
                    .HasForeignKey(d => d.ParentCategoryId);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.MainCustomer).HasColumnType("bit");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.UserId);

                entity.HasOne(d => d.ShippingInfo)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.ShippingInfoId);

                entity.HasOne(d => d.PaymentInfo)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.PaymentInfoId);
            });

            modelBuilder.Entity<PaymentInfo>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.PaymentMethodId).HasColumnName("PaymentMethodID");
                entity.Property(e => e.BillingAddressId).HasColumnName("BillingAddressID");

                entity.HasOne(d => d.PaymentMethod)
                    .WithMany(p => p.PaymentInfos)
                    .HasForeignKey(d => d.PaymentMethodId);

                entity.HasOne(d => d.BillingAddress)
                    .WithMany(p => p.PaymentInfos)
                    .HasForeignKey(d => d.BillingAddressId);
            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Method).HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
                entity.Property(e => e.VatId).HasColumnName("VATID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId);

                entity.HasOne(d => d.Vat)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.VatId);
            });

            modelBuilder.Entity<ProductStock>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.ProductId).HasColumnName("ProductID");
                entity.Property(e => e.SizeId).HasColumnName("SizeID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductStocks)
                    .HasForeignKey(d => d.ProductId);

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.ProductStocks)
                    .HasForeignKey(d => d.SizeId);
            });

            modelBuilder.Entity<ShippingInfo>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.ShippingMethodId).HasColumnName("ShippingMethodID");
                entity.Property(e => e.ShippingAddressId).HasColumnName("ShippingAddressID");

                entity.HasOne(d => d.ShippingMethod)
                    .WithMany(p => p.ShippingInfos)
                    .HasForeignKey(d => d.ShippingMethodId);

                entity.HasOne(d => d.ShippingAddress)
                    .WithMany(p => p.ShippingInfos)
                    .HasForeignKey(d => d.ShippingAddressId);
            });

            modelBuilder.Entity<ShippingMethod>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Method).HasMaxLength(50);
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Email).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<Vat>(entity =>
            {
                entity.ToTable("VAT");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Percentage).IsRequired().HasMaxLength(50);
            });
        }
    }
}
