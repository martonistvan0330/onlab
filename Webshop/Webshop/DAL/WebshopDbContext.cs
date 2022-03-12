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
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductStock> ProductStock { get; set; }
        public virtual DbSet<Size> Size { get; set; }
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

                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.Country).HasMaxLength(50);
                entity.Property(e => e.Region).HasMaxLength(50);
                entity.Property(e => e.ZipCode).HasMaxLength(10);
                entity.Property(e => e.City).HasMaxLength(50);
                entity.Property(e => e.Street).HasMaxLength(50);
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

            modelBuilder.Entity<Size>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
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
