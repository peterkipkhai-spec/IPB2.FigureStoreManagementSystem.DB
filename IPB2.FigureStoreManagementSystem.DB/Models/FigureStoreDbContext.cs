using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IPB2.FigureStoreManagementSystem.DB.Models;

public partial class FigureStoreDbContext : DbContext
{
    public FigureStoreDbContext()
    {
    }

    public FigureStoreDbContext(DbContextOptions<FigureStoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FsCategory> FsCategories { get; set; }

    public virtual DbSet<FsCustomer> FsCustomers { get; set; }

    public virtual DbSet<FsFigure> FsFigures { get; set; }

    public virtual DbSet<FsOrder> FsOrders { get; set; }

    public virtual DbSet<FsOrderDetail> FsOrderDetails { get; set; }

    public virtual DbSet<FsPayment> FsPayments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
        {
            return;
        }

        var connectionString = Environment.GetEnvironmentVariable("FIGURE_STORE_CONNECTION_STRING")
            ?? "Server=150.95.90.244;Database=PeterInPersonBatch2;User Id=sa;Password=sasa@123;TrustServerCertificate=True;";

        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FsCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__fs_categ__D54EE9B4E535B6E2");

            entity.ToTable("fs_categories");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("category_name");
        });

        modelBuilder.Entity<FsCustomer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__fs_custo__CD65CB85220BD35F");

            entity.ToTable("fs_customers");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("customer_name");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<FsFigure>(entity =>
        {
            entity.HasKey(e => e.FigureId).HasName("PK__fs_figur__7431624D3E7E2125");

            entity.ToTable("fs_figures");

            entity.Property(e => e.FigureId).HasColumnName("figure_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.FigureName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("figure_name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Stock).HasColumnName("stock");

            entity.HasOne(d => d.Category).WithMany(p => p.FsFigures)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_fs_figures_categories");
        });

        modelBuilder.Entity<FsOrder>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__fs_order__46596229E6923F7A");

            entity.ToTable("fs_orders");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("order_date");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("Pending")
                .HasColumnName("status");
            entity.Property(e => e.TotalAmount)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_amount");

            entity.HasOne(d => d.Customer).WithMany(p => p.FsOrders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_fs_orders_customers");
        });

        modelBuilder.Entity<FsOrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__fs_order__3C5A408026D8EB44");

            entity.ToTable("fs_order_details");

            entity.Property(e => e.OrderDetailId).HasColumnName("order_detail_id");
            entity.Property(e => e.FigureId).HasColumnName("figure_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Subtotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("subtotal");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("unit_price");

            entity.HasOne(d => d.Figure).WithMany(p => p.FsOrderDetails)
                .HasForeignKey(d => d.FigureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_fs_orderdetails_figures");

            entity.HasOne(d => d.Order).WithMany(p => p.FsOrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_fs_orderdetails_orders");
        });

        modelBuilder.Entity<FsPayment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__fs_payme__ED1FC9EA825CF5D8");

            entity.ToTable("fs_payments");

            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("payment_date");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("payment_method");

            entity.HasOne(d => d.Order).WithMany(p => p.FsPayments)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_fs_payments_orders");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
