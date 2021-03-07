using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DigitalGameStore.Models
{
    public partial class ris_dbContext : DbContext
    {
        public ris_dbContext()
        {
        }

        public ris_dbContext(DbContextOptions<ris_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<CreditCard> CreditCard { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductClass> ProductClass { get; set; }
        public virtual DbSet<ProductOrder> ProductOrder { get; set; }
        public virtual DbSet<Review> Review { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost,1433;Initial Catalog=ris_db;Persist Security Info=False;User ID=sa;Password=adminPassword123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.AddressId).ValueGeneratedNever();

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CreditCard>(entity =>
            {
                entity.Property(e => e.CreditCardId).ValueGeneratedNever();

                entity.Property(e => e.CreditCardCvv).HasColumnName("CreditCardCVV");

                entity.Property(e => e.CreditCardNumber)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CreditCard)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CreditCard_FK");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).ValueGeneratedNever();

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.Property(e => e.OrderDetails)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Order_FK");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).ValueGeneratedNever();

                entity.Property(e => e.ProductDescription)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ProductImgUrl)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductPrice).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.ProductClass)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ProductClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Product_FK");
            });

            modelBuilder.Entity<ProductClass>(entity =>
            {
                entity.Property(e => e.ProductClassId).ValueGeneratedNever();

                entity.Property(e => e.ProductClassName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProductOrder>(entity =>
            {
                entity.Property(e => e.ProductOrderId).ValueGeneratedNever();

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.ProductOrder)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProductOrder_FK");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductOrder)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProductOrder_FK_1");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.Property(e => e.ReviewId)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ReviewComment)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Review)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Review_FK");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Review)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("User_FK");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("User_FK_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
