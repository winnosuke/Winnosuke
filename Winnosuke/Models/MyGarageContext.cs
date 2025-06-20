using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Winnosuke.Models;

public partial class MyGarageContext : DbContext
{
    public MyGarageContext()
    {
    }

    public MyGarageContext(DbContextOptions<MyGarageContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdminActivity> AdminActivities { get; set; }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Garage> Garages { get; set; }

    public virtual DbSet<GarageSchedule> GarageSchedules { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<RepairStatus> RepairStatuses { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<TechnicalReport> TechnicalReports { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-K74LGB4H\\SQL2019;Initial Catalog=MyGarageFinal4;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminActivity>(entity =>
        {
            entity.HasKey(e => e.ActivityId).HasName("PK__AdminAct__45F4A7F19DEA1F75");

            entity.Property(e => e.ActivityId).HasColumnName("ActivityID");
            entity.Property(e => e.ActionType).HasMaxLength(100);
            entity.Property(e => e.AdminId).HasColumnName("AdminID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);

            entity.HasOne(d => d.Admin).WithMany(p => p.AdminActivities)
                .HasForeignKey(d => d.AdminId)
                .HasConstraintName("FK__AdminActi__Admin__4E88ABD4");
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__8ECDFCA21FF165BA");

            entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");
            entity.Property(e => e.AppointmentTime).HasColumnType("datetime");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.GarageId).HasColumnName("GarageID");
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TechnicianId).HasColumnName("TechnicianID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");

            entity.HasOne(d => d.Garage).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.GarageId)
                .HasConstraintName("FK__Appointme__Garag__35BCFE0A");

            entity.HasOne(d => d.Service).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__Appointme__Servi__36B12243");

            entity.HasOne(d => d.Technician).WithMany(p => p.AppointmentTechnicians)
                .HasForeignKey(d => d.TechnicianId)
                .HasConstraintName("FK__Appointme__Techn__37A5467C");

            entity.HasOne(d => d.User).WithMany(p => p.AppointmentUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Appointme__UserI__33D4B598");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK__Appointme__Vehic__34C8D9D1");
        });

        modelBuilder.Entity<Garage>(entity =>
        {
            entity.HasKey(e => e.GarageId).HasName("PK__Garages__5D8BEEB1DBBB9750");

            entity.Property(e => e.GarageId).HasColumnName("GarageID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.OperatingArea).HasMaxLength(100);

            entity.HasMany(d => d.Services).WithMany(p => p.Garages)
                .UsingEntity<Dictionary<string, object>>(
                    "GarageService",
                    r => r.HasOne<Service>().WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__GarageSer__Servi__30F848ED"),
                    l => l.HasOne<Garage>().WithMany()
                        .HasForeignKey("GarageId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__GarageSer__Garag__300424B4"),
                    j =>
                    {
                        j.HasKey("GarageId", "ServiceId").HasName("PK__GarageSe__E1DA55BF833CFCBC");
                        j.ToTable("GarageServices");
                        j.IndexerProperty<int>("GarageId").HasColumnName("GarageID");
                        j.IndexerProperty<int>("ServiceId").HasColumnName("ServiceID");
                    });
        });

        modelBuilder.Entity<GarageSchedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__GarageSc__9C8A5B69B0A31EF0");

            entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");
            entity.Property(e => e.DayOfWeek).HasMaxLength(20);
            entity.Property(e => e.GarageId).HasColumnName("GarageID");

            entity.HasOne(d => d.Garage).WithMany(p => p.GarageSchedules)
                .HasForeignKey(d => d.GarageId)
                .HasConstraintName("FK__GarageSch__Garag__52593CB8");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E32A2357290");

            entity.Property(e => e.NotificationId).HasColumnName("NotificationID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsRead).HasDefaultValue(false);
            entity.Property(e => e.Message).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Notificat__UserI__44FF419A");
        });

        modelBuilder.Entity<RepairStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__RepairSt__C8EE20436A9AAF8F");

            entity.ToTable("RepairStatus");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Notes).HasMaxLength(255);
            entity.Property(e => e.StatusStep).HasMaxLength(100);

            entity.HasOne(d => d.Appointment).WithMany(p => p.RepairStatuses)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK__RepairSta__Appoi__3B75D760");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79AED4602E8D");

            entity.Property(e => e.ReviewId).HasColumnName("ReviewID");
            entity.Property(e => e.Comment).HasMaxLength(500);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.GarageId).HasColumnName("GarageID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Garage).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.GarageId)
                .HasConstraintName("FK__Reviews__GarageI__403A8C7D");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Reviews__UserID__3F466844");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Services__C51BB0EA1BE5450E");

            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.image_url)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("image_url");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ServiceName).HasMaxLength(100);
        });

        modelBuilder.Entity<TechnicalReport>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__Technica__D5BD48E57BF104F2");

            entity.Property(e => e.ReportId).HasColumnName("ReportID");
            entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PerformedItems).HasMaxLength(500);
            entity.Property(e => e.Recommendations).HasMaxLength(500);
            entity.Property(e => e.TechnicianId).HasColumnName("TechnicianID");
            entity.Property(e => e.VehicleStatus).HasMaxLength(255);

            entity.HasOne(d => d.Appointment).WithMany(p => p.TechnicalReports)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK__Technical__Appoi__49C3F6B7");

            entity.HasOne(d => d.Technician).WithMany(p => p.TechnicalReports)
                .HasForeignKey(d => d.TechnicianId)
                .HasConstraintName("FK__Technical__Techn__4AB81AF0");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC49325489");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4215AEEB2").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Role).HasMaxLength(20);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.VehicleId).HasName("PK__Vehicles__476B54B235E89534");

            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");
            entity.Property(e => e.LicensePlate).HasMaxLength(20);
            entity.Property(e => e.Make).HasMaxLength(50);
            entity.Property(e => e.Model).HasMaxLength(50);
            entity.Property(e => e.Notes).HasMaxLength(255);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Vehicles__UserID__29572725");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
