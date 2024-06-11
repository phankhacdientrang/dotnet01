using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace THX2.Entities;

public partial class NewthxContext : DbContext
{
    public NewthxContext()
    {
    }

    public NewthxContext(DbContextOptions<NewthxContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Huyen> Huyens { get; set; }

    public virtual DbSet<Tinh> Tinhs { get; set; }

    public virtual DbSet<Xa> Xas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-829QP1FR\\MAYAO;Database=NEWTHX;User Id=sa;Password=123;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Huyen>(entity =>
        {
            entity.HasKey(e => e.MaH).HasName("PK__Huyens__C7977BB03B1A21D3");

            entity.Property(e => e.MaH).ValueGeneratedNever();
            entity.Property(e => e.Cap).HasMaxLength(50);
            entity.Property(e => e.Ten).HasMaxLength(100);

            entity.HasOne(d => d.MaTNavigation).WithMany(p => p.Huyens)
                .HasForeignKey(d => d.MaT)
                .HasConstraintName("FK__Huyens__MaT__398D8EEE");
        });

        modelBuilder.Entity<Tinh>(entity =>
        {
            entity.HasKey(e => e.MaT).HasName("PK__Tinhs__C7977BA4A6F6C99D");

            entity.Property(e => e.MaT).ValueGeneratedNever();
            entity.Property(e => e.Cap).HasMaxLength(50);
            entity.Property(e => e.Ten).HasMaxLength(100);
        });

        modelBuilder.Entity<Xa>(entity =>
        {
            entity.HasKey(e => e.MaX).HasName("PK__Xas__C7977BA06C98FFAD");

            entity.Property(e => e.MaX).ValueGeneratedNever();
            entity.Property(e => e.Cap).HasMaxLength(50);
            entity.Property(e => e.Ten).HasMaxLength(100);

            entity.HasOne(d => d.MaHNavigation).WithMany(p => p.Xas)
                .HasForeignKey(d => d.MaH)
                .HasConstraintName("FK__Xas__MaH__3C69FB99");

            entity.HasOne(d => d.MaTNavigation).WithMany(p => p.Xas)
                .HasForeignKey(d => d.MaT)
                .HasConstraintName("FK__Xas__MaT__3D5E1FD2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
