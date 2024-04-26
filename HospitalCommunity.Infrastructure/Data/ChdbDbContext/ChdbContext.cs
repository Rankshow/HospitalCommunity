using System;
using System.Collections.Generic;
using HospitalCommunity.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalCommunity.Infrastructure.Data.ChdbDbContext;

public partial class ChdbContext : DbContext
{
    public ChdbContext()
    {
    }

    public ChdbContext(DbContextOptions<ChdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admission> Admissions { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Encounter> Encounters { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Medication> Medications { get; set; }

    public virtual DbSet<NursingUnit> NursingUnits { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Physician> Physicians { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }

    public virtual DbSet<PurchaseOrderLine> PurchaseOrderLines { get; set; }

    public virtual DbSet<UnitDoseOrder> UnitDoseOrders { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=chdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admission>(entity =>
        {
            entity.HasKey(e => e.AdmissionId).HasName("PK__admissio__3D9F8C72F45B87B6");

            entity.HasOne(d => d.AttendingPhysician).WithMany(p => p.Admissions).HasConstraintName("FK__admission__atten__38996AB5");

            entity.HasOne(d => d.NursingUnit).WithMany(p => p.Admissions).HasConstraintName("FK__admission__nursi__398D8EEE");

            entity.HasOne(d => d.Patient).WithMany(p => p.Admissions).HasConstraintName("FK__admission__patie__37A5467C");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__departme__C2232422272ADDDC");
        });

        modelBuilder.Entity<Encounter>(entity =>
        {
            entity.HasKey(e => e.EncounterId).HasName("PK__encounte__CDF1340FB15DE6B4");

            entity.HasOne(d => d.Patient).WithMany(p => p.Encounters).HasConstraintName("FK__encounter__patie__3C69FB99");

            entity.HasOne(d => d.Physician).WithMany(p => p.Encounters).HasConstraintName("FK__encounter__physi__3D5E1FD2");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__items__52020FDD385653EA");

            entity.HasOne(d => d.PrimaryVendor).WithMany(p => p.Items).HasConstraintName("FK__items__primary_v__403A8C7D");
        });

        modelBuilder.Entity<Medication>(entity =>
        {
            entity.HasKey(e => e.MedicationId).HasName("PK__medicati__DD94789BCB9A83EA");
        });

        modelBuilder.Entity<NursingUnit>(entity =>
        {
            entity.HasKey(e => e.NursingUnitId).HasName("PK__nursing___0E85E2CCB32BE0BD");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PK__patients__4D5CE4766ECCC589");

            entity.Property(e => e.Gender).IsFixedLength();
            entity.Property(e => e.ProvinceId).IsFixedLength();

            entity.HasOne(d => d.Province).WithMany(p => p.Patients).HasConstraintName("FK__patients__provin__300424B4");
        });

        modelBuilder.Entity<Physician>(entity =>
        {
            entity.HasKey(e => e.PhysicianId).HasName("PK__physicia__8C035A3C5B18CA37");
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.HasKey(e => e.ProvinceId).HasName("PK__province__08DCB60F5B8CED75");

            entity.Property(e => e.ProvinceId).IsFixedLength();
        });

        modelBuilder.Entity<PurchaseOrder>(entity =>
        {
            entity.HasKey(e => e.PurchaseOrderId).HasName("PK__purchase__AFCA88E6A74459B1");

            entity.HasOne(d => d.Department).WithMany(p => p.PurchaseOrders).HasConstraintName("FK__purchase___depar__4316F928");

            entity.HasOne(d => d.Vendor).WithMany(p => p.PurchaseOrders).HasConstraintName("FK__purchase___vendo__440B1D61");
        });

        modelBuilder.Entity<PurchaseOrderLine>(entity =>
        {
            entity.HasKey(e => e.PurchaseOrderLineId).HasName("PK__purchase__AA56350E341B7207");

            entity.HasOne(d => d.Item).WithMany(p => p.PurchaseOrderLines).HasConstraintName("FK__purchase___item___4BAC3F29");

            entity.HasOne(d => d.PurchaseOrder).WithMany(p => p.PurchaseOrderLines).HasConstraintName("FK__purchase___purch__4AB81AF0");
        });

        modelBuilder.Entity<UnitDoseOrder>(entity =>
        {
            entity.HasKey(e => e.UnitDoseOrderId).HasName("PK__unit_dos__BB64A31AA997B529");

            entity.Property(e => e.PharmacistInitials).IsFixedLength();

            entity.HasOne(d => d.Medication).WithMany(p => p.UnitDoseOrders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__unit_dose__medic__47DBAE45");

            entity.HasOne(d => d.Patient).WithMany(p => p.UnitDoseOrders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__unit_dose__patie__46E78A0C");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.VendorId).HasName("PK__vendors__0F7D2B789509406D");

            entity.Property(e => e.ProvinceId).IsFixedLength();

            entity.HasOne(d => d.Province).WithMany(p => p.Vendors).HasConstraintName("FK__vendors__provinc__34C8D9D1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
