using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace midterm.Model;

public partial class Se4458midtermContext : DbContext
{
    public Se4458midtermContext()
    {
    }

    public Se4458midtermContext(DbContextOptions<Se4458midtermContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<Passenger> Passengers { get; set; }

    public virtual DbSet<PassengerFlight> PassengerFlights { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=tcp:se4458mid.database.windows.net,1433;Initial Catalog=se4458midterm;Persist Security Info=False;User ID=19070006020@stu.yasar.edu.tr;Password=ebGueeFk;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Authentication=Active Directory Password;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.FlightId).HasName("PK__Flight__8A9E148E50E76A3C");

            entity.ToTable("Flight");

            entity.HasIndex(e => e.FlightNumber, "UQ__Flight__2EAE6F509867CA8B").IsUnique();

            entity.Property(e => e.FlightId).HasColumnName("FlightID");
            entity.Property(e => e.Departure).HasMaxLength(50);
            entity.Property(e => e.Destination).HasMaxLength(50);
            entity.Property(e => e.FlightDate).HasColumnType("date");
            entity.Property(e => e.FlightNumber).HasMaxLength(50);
        });

        modelBuilder.Entity<Passenger>(entity =>
        {
            entity.HasKey(e => e.PassengerId).HasName("PK__Passenge__88915F909256FB35");

            entity.ToTable("Passenger");

            entity.HasIndex(e => e.Username, "UQ__Passenge__536C85E48347ABDA").IsUnique();

            entity.Property(e => e.PassengerId).HasColumnName("PassengerID");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PassengerPassword).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<PassengerFlight>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Passenge__3214EC276A3C6024");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FlightId).HasColumnName("FlightID");
            entity.Property(e => e.PassengerId).HasColumnName("PassengerID");

            entity.HasOne(d => d.Flight).WithMany(p => p.PassengerFlights)
                .HasForeignKey(d => d.FlightId)
                .HasConstraintName("FK__Passenger__Fligh__76969D2E");

            entity.HasOne(d => d.Passenger).WithMany(p => p.PassengerFlights)
                .HasForeignKey(d => d.PassengerId)
                .HasConstraintName("FK__Passenger__Passe__75A278F5");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
