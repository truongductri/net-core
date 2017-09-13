using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace BuildingManage
{
    public partial class ApartmentDBContext : DbContext
    {
        public ApartmentDBContext(DbContextOptions<ApartmentDBContext> options)
    : base(options)
      { }
        public virtual DbSet<Contract> Contract { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<TypeRoom> TypeRoom { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contract>(entity =>
            {
                entity.Property(e => e.BeginDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.MetaTitle).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Contract)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Contract_Customer");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Contract)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Contract_Room");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.BirthDay).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.MetaTitle).HasColumnType("nchar(10)");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Sex).HasColumnType("nchar(10)");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.Image).HasMaxLength(50);

                entity.Property(e => e.MetaTiTle).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.Room)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Room_TypeRoom");
            });

            modelBuilder.Entity<TypeRoom>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });
        }
    }
}