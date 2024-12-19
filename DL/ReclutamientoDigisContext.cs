using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class ReclutamientoDigisContext : DbContext
{
    public ReclutamientoDigisContext()
    {
    }

    public ReclutamientoDigisContext(DbContextOptions<ReclutamientoDigisContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Candidato> Candidatos { get; set; }

    public virtual DbSet<CandidatoVacante> CandidatoVacantes { get; set; }

    public virtual DbSet<Citum> Cita { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<EstatusCitum> EstatusCita { get; set; }

    public virtual DbSet<EstatusVacante> EstatusVacantes { get; set; }

    public virtual DbSet<Piso> Pisos { get; set; }

    public virtual DbSet<Vacante> Vacantes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database= ReclutamientoDigis;TrustServerCertificate=True;User ID=sa;Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candidato>(entity =>
        {
            entity.HasKey(e => e.IdCandidato).HasName("PK__Candidat__D5598905E8AFBD69");

            entity.ToTable("Candidato");

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Edad)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdVacanteNavigation).WithMany(p => p.Candidatos)
                .HasForeignKey(d => d.IdVacante)
                .HasConstraintName("FK__Candidato__IdVac__1CF15040");
        });

        modelBuilder.Entity<CandidatoVacante>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("CandidatoVacante");

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Edad)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.FechaHora).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreEstatusCita)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombrePiso).HasMaxLength(50);
            entity.Property(e => e.NombreVacante)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Url).HasMaxLength(50);
        });

        modelBuilder.Entity<Citum>(entity =>
        {
            entity.HasKey(e => e.IdCita).HasName("PK__Cita__394B02021C53D3A6");

            entity.Property(e => e.FechaHora).HasColumnType("datetime");
            entity.Property(e => e.Url).HasMaxLength(50);

            entity.HasOne(d => d.IdCandidatoNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdCandidato)
                .HasConstraintName("FK__Cita__IdCandidat__2A4B4B5E");

            entity.HasOne(d => d.IdEstatusCitaNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdEstatusCita)
                .HasConstraintName("FK__Cita__IdEstatusC__2B3F6F97");

            entity.HasOne(d => d.IdPisoNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdPiso)
                .HasConstraintName("FK__Cita__IdPiso__29572725");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.IdEmpresa).HasName("PK__Empresa__5EF4033E9187A053");

            entity.ToTable("Empresa");

            entity.Property(e => e.Latitud)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Longitud)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EstatusCitum>(entity =>
        {
            entity.HasKey(e => e.IdEstatusCita).HasName("PK__EstatusC__D2463B08EF6C6B9F");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EstatusVacante>(entity =>
        {
            entity.HasKey(e => e.IdEstatusVacante).HasName("PK__EstatusV__977022FA2167D882");

            entity.ToTable("EstatusVacante");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Piso>(entity =>
        {
            entity.HasKey(e => e.IdPiso).HasName("PK__Piso__F2823D8AB45DAA62");

            entity.ToTable("Piso");

            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Vacante>(entity =>
        {
            entity.HasKey(e => e.IdVacante).HasName("PK__Vacante__A58A8FA85A6F7DBD");

            entity.ToTable("Vacante");

            entity.Property(e => e.FechaLimite).HasColumnType("datetime");
            entity.Property(e => e.FechaPublicacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.UrlVacante)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEstatusVacanteNavigation).WithMany(p => p.Vacantes)
                .HasForeignKey(d => d.IdEstatusVacante)
                .HasConstraintName("FK__Vacante__IdEstat__1A14E395");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
