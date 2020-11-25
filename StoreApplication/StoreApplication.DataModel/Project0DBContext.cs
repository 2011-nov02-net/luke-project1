using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace StoreApplication.DataModel
{
    public partial class Project0DBContext : DbContext
    {
        public Project0DBContext()
        {
        }

        public Project0DBContext(DbContextOptions<Project0DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderSale> OrderSales { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<StoreInventory> StoreInventories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer", "StoreApp");

                entity.HasIndex(e => e.Email, "UQ__Customer__A9D1053461B38AC1")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location", "StoreApp");

                entity.HasIndex(e => e.Name, "UQ__Location__737584F6D233EE55")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Orders", "StoreApp");

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.Property(e => e.Total).HasColumnType("money");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Orders__Customer__6D9742D9");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK__Orders__Location__6CA31EA0");
            });

            modelBuilder.Entity<OrderSale>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId })
                    .HasName("PK_SaleId");

                entity.ToTable("OrderSale", "StoreApp");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.SalePrice).HasColumnType("money");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderSales)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__OrderSale__Order__762C88DA");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderSales)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__OrderSale__Produ__7720AD13");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product", "StoreApp");

                entity.HasIndex(e => e.Name, "UQ__Product__737584F621731BB2")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Price).HasColumnType("money");
            });

            modelBuilder.Entity<StoreInventory>(entity =>
            {
                entity.HasKey(e => new { e.LocationId, e.ProductId });

                entity.ToTable("StoreInventory", "StoreApp");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.StoreInventories)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK__StoreInve__Locat__7167D3BD");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.StoreInventories)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__StoreInve__Produ__725BF7F6");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
