using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace DbModel.demoDb;

public partial class _demoContext : DbContext
{
    public _demoContext()
    {
    }

    public _demoContext(DbContextOptions<_demoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Persona> Persona { get; set; }

    public virtual DbSet<PersonaTipoDocumento> PersonaTipoDocumento { get; set; }

    public virtual DbSet<Casa> Casa { get; set; }

    public virtual DbSet<Vehiculo> Vehiculo { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("name=demoDb", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.45-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.DateCreated).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.DateUpdate).ValueGeneratedOnAddOrUpdate();

            entity.HasOne(d => d.IdTipoDocumentoNavigation).WithMany(p => p.Persona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_persona_tipo_documento");
        });

        
        modelBuilder.Entity<Casa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.DateCreated).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.DateUpdate).ValueGeneratedOnAddOrUpdate();

            entity.HasOne(d => d.Propietario)
                .WithMany()
                .HasForeignKey(d => d.IdPropietarioPersona)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_casa_propietario_persona");
        });
        modelBuilder.Entity<Vehiculo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.DateCreated).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.DateUpdate).ValueGeneratedOnAddOrUpdate();

            entity.HasOne(d => d.Propietario)
                .WithMany()
                .HasForeignKey(d => d.IdPropietarioPersona)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_vehiculo_propietario_persona");

            entity.HasOne(d => d.Casa)
                .WithMany()
                .HasForeignKey(d => d.IdCasa)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_vehiculo_casa");
        });

        modelBuilder.Entity<PersonaTipoDocumento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.DateCreated).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.DateUpdate).ValueGeneratedOnAddOrUpdate();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
