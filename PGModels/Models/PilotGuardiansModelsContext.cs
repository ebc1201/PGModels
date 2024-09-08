using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PGModels.Models
{
    public partial class PilotGuardiansModelsContext : DbContext
    {
        public PilotGuardiansModelsContext()
        {
        }

        public PilotGuardiansModelsContext(DbContextOptions<PilotGuardiansModelsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AirCraft> AirCraft { get; set; } = null!;
        public virtual DbSet<Flight> Flights { get; set; } = null!;
        public virtual DbSet<Passenger> Passengers { get; set; } = null!;
        public virtual DbSet<Person> People { get; set; } = null!;
        public virtual DbSet<Pilot> Pilots { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=ERICA-BOOK3-PRO;Database=PilotGuardiansModels;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AirCraft>(entity =>
            {
                entity.Property(e => e.AirCraftId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("AirCraftID");

                entity.Property(e => e.EngineType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Flight>(entity =>
            {
                entity.ToTable("Flight");

                entity.Property(e => e.FlightId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("FlightID");

                entity.Property(e => e.AirCraftId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("AirCraftID");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.FuelUsed).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.AirCraft)
                    .WithMany(p => p.Flights)
                    .HasForeignKey(d => d.AirCraftId)
                    .HasConstraintName("FK_Flight_AirCraft");
            });

            modelBuilder.Entity<Passenger>(entity =>
            {
                entity.ToTable("Passenger");

                entity.Property(e => e.PassengerId)
                    .ValueGeneratedNever()
                    .HasColumnName("PassengerID");

                entity.Property(e => e.CompanionName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CompanionRelation)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Passengers)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_Passenger_Person");

                entity.HasMany(d => d.Flights)
                    .WithMany(p => p.Passengers)
                    .UsingEntity<Dictionary<string, object>>(
                        "PassengerFlight",
                        l => l.HasOne<Flight>().WithMany().HasForeignKey("FlightId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_PassengerFlight_Flight"),
                        r => r.HasOne<Passenger>().WithMany().HasForeignKey("PassengerId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_PassengerFlight_Passenger"),
                        j =>
                        {
                            j.HasKey("PassengerId", "FlightId").HasName("PK__Passenge__7038BED8FC39C80B");

                            j.ToTable("PassengerFlight");

                            j.IndexerProperty<int>("PassengerId").HasColumnName("PassengerID");

                            j.IndexerProperty<string>("FlightId").HasMaxLength(20).IsUnicode(false).HasColumnName("FlightID");
                        });
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FName");

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Lname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LName");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Street)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pilot>(entity =>
            {
                entity.HasKey(e => e.LicenseNumber)
                    .HasName("PK__Pilot__E8890167E8612D2E");

                entity.ToTable("Pilot");

                entity.Property(e => e.LicenseNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Certifications)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FlightId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("FlightID");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.Ratings)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Flight)
                    .WithMany(p => p.Pilots)
                    .HasForeignKey(d => d.FlightId)
                    .HasConstraintName("FK_Pilot_Flight");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Pilots)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pilot_Person");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
