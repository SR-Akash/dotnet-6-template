using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MGM_Lite.Models;

namespace MGM_Lite.DBContext
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ChartOfAccCategory> ChartOfAccCategories { get; set; } = null!;
        public virtual DbSet<ChartofAcc> ChartofAccs { get; set; } = null!;
        public virtual DbSet<ChartofAccTemplate> ChartofAccTemplates { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<Partner> Partners { get; set; } = null!;
        public virtual DbSet<PartnerType> PartnerTypes { get; set; } = null!;
        public virtual DbSet<PurchaseReceiveHeader> PurchaseReceiveHeaders { get; set; } = null!;
        public virtual DbSet<PurchaseReceiveRow> PurchaseReceiveRows { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=10.209.99.144;Initial Catalog=MGMLite;User ID=smeapp;Password=sds#dt454sesa0wdnp@1vpo#98;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChartOfAccCategory>(entity =>
            {
                entity.ToTable("ChartOfAccCategory");

                entity.Property(e => e.ChartOfAccCategoryName).HasMaxLength(250);
            });

            modelBuilder.Entity<ChartofAcc>(entity =>
            {
                entity.ToTable("ChartofAcc");

                entity.Property(e => e.ChartOfAccCategoryName).HasMaxLength(200);

                entity.Property(e => e.ChartOfAccCode).HasMaxLength(50);

                entity.Property(e => e.ChartOfAccName).HasMaxLength(300);

                entity.Property(e => e.LastActionDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<ChartofAccTemplate>(entity =>
            {
                entity.HasKey(e => e.TemplateId);

                entity.ToTable("ChartofAccTemplate");

                entity.Property(e => e.ChartOfAccCategoryName).HasMaxLength(300);

                entity.Property(e => e.ChartOfAccCode).HasMaxLength(50);

                entity.Property(e => e.ChartOfAccName).HasMaxLength(300);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.BloodGroup).HasMaxLength(5);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Designation).HasMaxLength(100);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.EmployeeName).HasMaxLength(100);

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.JoinDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MobileNumber).HasMaxLength(15);

                entity.Property(e => e.PresentAddress).HasMaxLength(250);

                entity.Property(e => e.Religion).HasMaxLength(50);
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.ItemCode).HasMaxLength(50);

                entity.Property(e => e.ItemDescription).HasMaxLength(500);

                entity.Property(e => e.ItemName).HasMaxLength(250);

                entity.Property(e => e.UomName).HasMaxLength(50);
            });

            modelBuilder.Entity<Partner>(entity =>
            {
                entity.ToTable("Partner");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.Mobile).HasMaxLength(50);

                entity.Property(e => e.PartnerCode).HasMaxLength(50);

                entity.Property(e => e.PartnerName).HasMaxLength(500);

                entity.Property(e => e.PartnerTypeName).HasMaxLength(150);
            });

            modelBuilder.Entity<PartnerType>(entity =>
            {
                entity.ToTable("PartnerType");

                entity.Property(e => e.PartnerTypeName).HasMaxLength(250);
            });

            modelBuilder.Entity<PurchaseReceiveHeader>(entity =>
            {
                entity.HasKey(e => e.PurchaseReceiveId);

                entity.ToTable("PurchaseReceiveHeader");

                entity.Property(e => e.ReceiveCode).HasMaxLength(50);

                entity.Property(e => e.ReceivedDate).HasColumnType("datetime");

                entity.Property(e => e.Remarks).HasMaxLength(500);

                entity.Property(e => e.SupplierName).HasMaxLength(250);

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.TotalQty).HasColumnType("decimal(18, 6)");
            });

            modelBuilder.Entity<PurchaseReceiveRow>(entity =>
            {
                entity.HasKey(e => e.RowId);

                entity.ToTable("PurchaseReceiveRow");

                entity.Property(e => e.ItemName).HasMaxLength(250);

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.Rate).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 6)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.EmployeeName).HasMaxLength(250);

                entity.Property(e => e.Mobile).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(100);

                entity.Property(e => e.UserName).HasMaxLength(250);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
