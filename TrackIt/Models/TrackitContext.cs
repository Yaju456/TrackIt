using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TrackIt.Models;

public partial class TrackitContext : DbContext
{
    public TrackitContext()
    {
    }

    public TrackitContext(DbContextOptions<TrackitContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<CustomerTable> CustomerTables { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<LocalBody> LocalBodies { get; set; }

    public virtual DbSet<OrderTable> OrderTables { get; set; }

    public virtual DbSet<ProductTable> ProductTables { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<SalesTable> SalesTables { get; set; }

    public virtual DbSet<StockTable> StockTables { get; set; }

    public virtual DbSet<VendorTable> VendorTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=localhost; database=trackit; trusted_connection=true; trustservercertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Discriminator).HasDefaultValueSql("(N'')");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<CustomerTable>(entity =>
        {
            entity.ToTable("CustomerTable");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.ToTable("District");

            entity.Property(e => e.Imucode).HasColumnName("IMUCode");
            entity.Property(e => e.Name)
                .HasMaxLength(400)
                .IsUnicode(false);
            entity.Property(e => e.NameNp).HasMaxLength(400);
        });

        modelBuilder.Entity<LocalBody>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Vdc");

            entity.ToTable("LocalBody");

            entity.Property(e => e.Imucode).HasColumnName("IMUCode");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NameNp).HasMaxLength(400);

            entity.HasOne(d => d.District).WithMany(p => p.LocalBodies)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LocalBody_District");
        });

        modelBuilder.Entity<OrderTable>(entity =>
        {
            entity.ToTable("OrderTable");

            entity.HasIndex(e => e.ProductId, "IX_OrderTable_Product_id");

            entity.HasIndex(e => e.VendorId, "IX_OrderTable_vendor_id");

            entity.Property(e => e.ProductId).HasColumnName("Product_id");
            entity.Property(e => e.VendorId).HasColumnName("vendor_id");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderTables).HasForeignKey(d => d.ProductId);

            entity.HasOne(d => d.Vendor).WithMany(p => p.OrderTables).HasForeignKey(d => d.VendorId);
        });

        modelBuilder.Entity<ProductTable>(entity =>
        {
            entity.ToTable("ProductTable");

            entity.Property(e => e.Company).HasColumnName("company");
            entity.Property(e => e.InStock).HasColumnName("In_stock");
            entity.Property(e => e.Modal).HasDefaultValueSql("(N'')");
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.ToTable("Province");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Imucode).HasColumnName("IMUCode");
            entity.Property(e => e.Name).HasMaxLength(400);
            entity.Property(e => e.NameNp).HasMaxLength(400);
        });

        modelBuilder.Entity<SalesTable>(entity =>
        {
            entity.ToTable("SalesTable");

            entity.HasIndex(e => e.ClinentId, "IX_SalesTable_Clinent_id");

            entity.HasIndex(e => e.ProductId, "IX_SalesTable_Product_id");

            entity.Property(e => e.ClientId).HasColumnName("Client_id");
            entity.Property(e => e.ClinentId).HasColumnName("Clinent_id");
            entity.Property(e => e.ProductId).HasColumnName("Product_id");
            entity.Property(e => e.SalesDate).HasColumnName("Sales_Date");

            entity.HasOne(d => d.Clinent).WithMany(p => p.SalesTables).HasForeignKey(d => d.ClinentId);

            entity.HasOne(d => d.Product).WithMany(p => p.SalesTables).HasForeignKey(d => d.ProductId);
        });

        modelBuilder.Entity<StockTable>(entity =>
        {
            entity.ToTable("StockTable");

            entity.HasIndex(e => e.CustomerId, "IX_StockTable_Customer_id");

            entity.HasIndex(e => e.OrderId, "IX_StockTable_Order_id");

            entity.HasIndex(e => e.ProductId, "IX_StockTable_Product_id");

            entity.HasIndex(e => e.SalesId, "IX_StockTable_Sales_id");

            entity.HasIndex(e => e.SerialNumber, "IX_StockTable_serial_number")
                .IsUnique()
                .HasFilter("([serial_number] IS NOT NULL)");

            entity.Property(e => e.CustomerId).HasColumnName("Customer_id");
            entity.Property(e => e.InStock).HasMaxLength(1);
            entity.Property(e => e.OrderId).HasColumnName("Order_id");
            entity.Property(e => e.ProductId).HasColumnName("Product_id");
            entity.Property(e => e.SalesId).HasColumnName("Sales_id");
            entity.Property(e => e.SerialNumber).HasColumnName("serial_number");

            entity.HasOne(d => d.Customer).WithMany(p => p.StockTables).HasForeignKey(d => d.CustomerId);

            entity.HasOne(d => d.Order).WithMany(p => p.StockTables).HasForeignKey(d => d.OrderId);

            entity.HasOne(d => d.Product).WithMany(p => p.StockTables)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Sales).WithMany(p => p.StockTables).HasForeignKey(d => d.SalesId);
        });

        modelBuilder.Entity<VendorTable>(entity =>
        {
            entity.ToTable("VendorTable");

            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
