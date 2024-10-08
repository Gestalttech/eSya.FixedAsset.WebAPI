﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace eSya.FixedAsset.DL.Entities
{
    public partial class eSyaEnterprise : DbContext
    {
        public static string _connString = "";
        public eSyaEnterprise()
        {
        }

        public eSyaEnterprise(DbContextOptions<eSyaEnterprise> options)
            : base(options)
        {
        }

        public virtual DbSet<GtEcapcd> GtEcapcds { get; set; } = null!;
        public virtual DbSet<GtEcbsln> GtEcbslns { get; set; } = null!;
        public virtual DbSet<GtEccncd> GtEccncds { get; set; } = null!;
        public virtual DbSet<GtEcfxag> GtEcfxags { get; set; } = null!;
        public virtual DbSet<GtEcfxam> GtEcfxams { get; set; } = null!;
        public virtual DbSet<GtEcfxdm> GtEcfxdms { get; set; } = null!;
        public virtual DbSet<GtEfxaal> GtEfxaals { get; set; } = null!;
        public virtual DbSet<GtEfxacd> GtEfxacds { get; set; } = null!;
        public virtual DbSet<GtEfxach> GtEfxaches { get; set; } = null!;
        public virtual DbSet<GtEfxapa> GtEfxapas { get; set; } = null!;
        public virtual DbSet<GtEfxapd> GtEfxapds { get; set; } = null!;
        public virtual DbSet<GtEiitct> GtEiitcts { get; set; } = null!;
        public virtual DbSet<GtEiitgc> GtEiitgcs { get; set; } = null!;
        public virtual DbSet<GtEiitgr> GtEiitgrs { get; set; } = null!;
        public virtual DbSet<GtEiitsc> GtEiitscs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(_connString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GtEcapcd>(entity =>
            {
                entity.HasKey(e => e.ApplicationCode)
                    .HasName("PK_GT_ECAPCD_1");

                entity.ToTable("GT_ECAPCD");

                entity.Property(e => e.ApplicationCode).ValueGeneratedNever();

                entity.Property(e => e.CodeDesc).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.ShortCode).HasMaxLength(15);
            });

            modelBuilder.Entity<GtEcbsln>(entity =>
            {
                entity.HasKey(e => new { e.BusinessId, e.LocationId });

                entity.ToTable("GT_ECBSLN");

                entity.HasIndex(e => e.BusinessKey, "IX_GT_ECBSLN")
                    .IsUnique();

                entity.Property(e => e.BusinessId).HasColumnName("BusinessID");

                entity.Property(e => e.BusinessName).HasMaxLength(100);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.CurrencyCode).HasMaxLength(4);

                entity.Property(e => e.DateFormat).HasMaxLength(25);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.Isdcode).HasColumnName("ISDCode");

                entity.Property(e => e.LocationDescription).HasMaxLength(150);

                entity.Property(e => e.Lstatus).HasColumnName("LStatus");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.ShortDateFormat).HasMaxLength(15);

                entity.Property(e => e.ShortDesc).HasMaxLength(15);
            });

            modelBuilder.Entity<GtEccncd>(entity =>
            {
                entity.HasKey(e => e.Isdcode);

                entity.ToTable("GT_ECCNCD");

                entity.Property(e => e.Isdcode)
                    .ValueGeneratedNever()
                    .HasColumnName("ISDCode");

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.CountryFlag).HasMaxLength(150);

                entity.Property(e => e.CountryName).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.CurrencyCode).HasMaxLength(4);

                entity.Property(e => e.DateFormat).HasMaxLength(25);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.IsPinapplicable).HasColumnName("IsPINApplicable");

                entity.Property(e => e.IsPoboxApplicable).HasColumnName("IsPOBoxApplicable");

                entity.Property(e => e.MobileNumberPattern)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.PincodePattern)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("PINcodePattern");

                entity.Property(e => e.PoboxPattern)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("POBoxPattern");

                entity.Property(e => e.ShortDateFormat).HasMaxLength(15);
            });

            modelBuilder.Entity<GtEcfxag>(entity =>
            {
                entity.HasKey(e => new { e.AssetGroup, e.AssetSubGroup });

                entity.ToTable("GT_ECFXAG");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtEcfxam>(entity =>
            {
                entity.HasKey(e => new { e.AssetGroup, e.AssetSubGroup });

                entity.ToTable("GT_ECFXAM");

                entity.Property(e => e.AccDepreciationAccount).HasMaxLength(15);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.DepreciationAccount).HasMaxLength(15);

                entity.Property(e => e.FixedAssetAccount).HasMaxLength(15);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtEcfxdm>(entity =>
            {
                entity.HasKey(e => new { e.Isdcode, e.AssetGroup, e.AssetSubGroup, e.EffectiveFrom, e.DepreciationMethod });

                entity.ToTable("GT_ECFXDM");

                entity.Property(e => e.Isdcode).HasColumnName("ISDCode");

                entity.Property(e => e.EffectiveFrom).HasColumnType("datetime");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.DepreciationPercentage).HasColumnType("numeric(5, 3)");

                entity.Property(e => e.EffectiveTill).HasColumnType("datetime");

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtEfxaal>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.AssetTag });

                entity.ToTable("GT_EFXAAL");

                entity.Property(e => e.AssetTag).HasMaxLength(50);

                entity.Property(e => e.AssetStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.CustodianType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DateAllocated).HasColumnType("datetime");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.DeptLocnId).HasColumnName("DeptLocnID");

                entity.Property(e => e.EmployeeName).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.IaserialNumber).HasColumnName("IASerialNumber");

                entity.Property(e => e.InstallationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.OtherDetails).HasMaxLength(50);

                entity.Property(e => e.TempDepartmentId).HasColumnName("TempDepartmentID");

                entity.Property(e => e.TransferDate).HasColumnType("datetime");

                entity.Property(e => e.TransferValue).HasColumnType("numeric(18, 6)");
            });

            modelBuilder.Entity<GtEfxacd>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.InternalAssetNo, e.IaserialNo });

                entity.ToTable("GT_EFXACD");

                entity.Property(e => e.IaserialNo).HasColumnName("IASerialNo");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.DepreciationValue).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.EquipmentSerialNo).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.LastProvDeprMonthYr)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.LastTransferDate).HasColumnType("datetime");

                entity.Property(e => e.LastTransferValue).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.ProvDepreciationValue).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.UnitAcquisitionValue).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.UnitAssetCost).HasColumnType("numeric(18, 6)");
            });

            modelBuilder.Entity<GtEfxach>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.InternalAssetNo, e.AssetGroup, e.AssetSubGroup });

                entity.ToTable("GT_EFXACH");

                entity.Property(e => e.AcquisitionDate).HasColumnType("datetime");

                entity.Property(e => e.AcquisitionValue).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.AssetCost).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.AssetDescription).HasMaxLength(100);

                entity.Property(e => e.AssetLifeInYears).HasColumnType("numeric(5, 3)");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.SourcedFrom).HasMaxLength(75);

                entity.Property(e => e.UnderWarrantyFrom).HasColumnType("datetime");

                entity.Property(e => e.UnderWarrantyTill).HasColumnType("datetime");
            });

            modelBuilder.Entity<GtEfxapa>(entity =>
            {
                entity.HasKey(e => new { e.InternalAssetNo, e.ParameterId });

                entity.ToTable("GT_EFXAPA");

                entity.Property(e => e.ParameterId).HasColumnName("ParameterID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.ParmDesc)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ParmPerc).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.ParmValue).HasColumnType("numeric(18, 6)");
            });

            modelBuilder.Entity<GtEfxapd>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.InternalAssetNo });

                entity.ToTable("GT_EFXAPD");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.Grndate)
                    .HasColumnType("datetime")
                    .HasColumnName("GRNDate");

                entity.Property(e => e.Grnnumber).HasColumnName("GRNNumber");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.Podate)
                    .HasColumnType("datetime")
                    .HasColumnName("PODate");

                entity.Property(e => e.Ponumber).HasColumnName("PONumber");

                entity.Property(e => e.VendorId).HasColumnName("VendorID");
            });

            modelBuilder.Entity<GtEiitct>(entity =>
            {
                entity.HasKey(e => e.ItemCategory);

                entity.ToTable("GT_EIITCT");

                entity.Property(e => e.ItemCategory).ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ItemCategoryDesc).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtEiitgc>(entity =>
            {
                entity.HasKey(e => new { e.ItemGroup, e.ItemCategory, e.ItemSubCategory });

                entity.ToTable("GT_EIITGC");

                entity.Property(e => e.ComittmentAmount).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.Fastatus).HasColumnName("FAStatus");

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.OriginalBudgetAmount).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.RevisedBudgetAmount).HasColumnType("numeric(18, 6)");

                entity.HasOne(d => d.ItemCategoryNavigation)
                    .WithMany(p => p.GtEiitgcs)
                    .HasForeignKey(d => d.ItemCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GT_EIITGC_GT_EIITCT");

                entity.HasOne(d => d.ItemGroupNavigation)
                    .WithMany(p => p.GtEiitgcs)
                    .HasForeignKey(d => d.ItemGroup)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GT_EIITGC_GT_EIITGR");
            });

            modelBuilder.Entity<GtEiitgr>(entity =>
            {
                entity.HasKey(e => e.ItemGroup);

                entity.ToTable("GT_EIITGR");

                entity.Property(e => e.ItemGroup).ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ItemGroupDesc).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtEiitsc>(entity =>
            {
                entity.HasKey(e => new { e.ItemCategory, e.ItemSubCategory });

                entity.ToTable("GT_EIITSC");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ItemSubCategoryDesc).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.HasOne(d => d.ItemCategoryNavigation)
                    .WithMany(p => p.GtEiitscs)
                    .HasForeignKey(d => d.ItemCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GT_EIITSC_GT_EIITCT");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
