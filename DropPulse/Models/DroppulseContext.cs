using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DropPulse.Models;

public partial class DroppulseContext : DbContext
{
    public DroppulseContext()
    {
    }

    public DroppulseContext(DbContextOptions<DroppulseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<IrrigationEvent> IrrigationEvents { get; set; }

    public virtual DbSet<PlantProfile> PlantProfiles { get; set; }

    public virtual DbSet<SensorDatum> SensorData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
 //      => optionsBuilder.UseSqlServer("server=localhost\\SQLEXPRESS; database=Droppulse; Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IrrigationEvent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Irrigati__3214EC07B7681282");

            entity.Property(e => e.TriggerReason).HasMaxLength(50);
        });

        modelBuilder.Entity<PlantProfile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PlantPro__3214EC07A21550FF");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<SensorDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SensorDa__3214EC0706F6E145");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
