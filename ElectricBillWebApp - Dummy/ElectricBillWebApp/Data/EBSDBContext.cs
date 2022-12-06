using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ElectricBillWebApp.Models.Entities;

namespace ElectricBillWebApp.Data
{
    public partial class EBSDBContext : DbContext
    {
        public EBSDBContext()
        {
        }

        public EBSDBContext(DbContextOptions<EBSDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CustomerMeter> CustomerMeters { get; set; } = null!;
        public virtual DbSet<TblBillHistory> TblBillHistories { get; set; } = null!;
        public virtual DbSet<TblCustomer> TblCustomers { get; set; } = null!;
        public virtual DbSet<TblMeterType> TblMeterTypes { get; set; } = null!;
        public virtual DbSet<TblPayHistory> TblPayHistories { get; set; } = null!;
        public virtual DbSet<TblTransactionHistory> TblTransactionHistories { get; set; } = null!;
        public virtual DbSet<TblUser> TblUsers { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=(LocalDB)\\localdb;Database=EBSDB;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerMeter>(entity =>
            {
                entity.ToTable("CustomerMeter");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerMeters)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerMeter_CustomerMeter");

                entity.HasOne(d => d.Meter)
                    .WithMany(p => p.CustomerMeters)
                    .HasForeignKey(d => d.MeterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerMeter_tblMeterType");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CustomerMeters)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerMeter_tblUser");
            });

            modelBuilder.Entity<TblBillHistory>(entity =>
            {
                entity.ToTable("tblBillHistory");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.BillPeriod)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Currency)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.FineAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PrepaidAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ReadDate).HasColumnType("datetime");

                entity.Property(e => e.RemainAmount).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TblBillHistories)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblBillHistory_tblCustomer");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblBillHistories)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblBillHistory_tblUser");
            });

            modelBuilder.Entity<TblCustomer>(entity =>
            {
                entity.ToTable("tblCustomer");

                entity.Property(e => e.Address).HasMaxLength(150);

                entity.Property(e => e.Barcode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.MeterNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Township).HasMaxLength(100);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TblCustomers)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_tblCustomer_tblUser");
            });

            modelBuilder.Entity<TblMeterType>(entity =>
            {
                entity.ToTable("tblMeterType");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TblMeterTypes)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_tblMeterType_tblUser");
            });

            modelBuilder.Entity<TblPayHistory>(entity =>
            {
                entity.ToTable("tblPayHistory");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PayAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PayDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Bill)
                    .WithMany(p => p.TblPayHistories)
                    .HasForeignKey(d => d.BillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblPayHistory_tblBillHistory");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TblPayHistories)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblPayHistory_tblCustomer");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblPayHistories)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblPayHistory_tblUser");
            });

            modelBuilder.Entity<TblTransactionHistory>(entity =>
            {
                entity.ToTable("tblTransactionHistory");

                entity.Property(e => e.BillPeriod)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FineAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.LateMonthCount).HasDefaultValueSql("((0))");

                entity.Property(e => e.MeterNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrepaidAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.RemainAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Status).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Bill)
                    .WithMany(p => p.TblTransactionHistories)
                    .HasForeignKey(d => d.BillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTransactionHistory_tblBillHistory");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TblTransactionHistories)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTransactionHistory_tblCustomer");

                entity.HasOne(d => d.Pay)
                    .WithMany(p => p.TblTransactionHistories)
                    .HasForeignKey(d => d.PayId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTransactionHistory_tblPayHistory");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.ToTable("tblUser");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).HasMaxLength(20);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
