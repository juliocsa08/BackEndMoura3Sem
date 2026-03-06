using System;
using System.Collections.Generic;
using EventPlus.WebAPIA.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPIA.BdContectEvent;

public partial class EventContext : DbContext
{
    public EventContext()
    {
    }

    public EventContext(DbContextOptions<EventContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ComentarioEvento> ComentarioEventos { get; set; }

    public virtual DbSet<Evento> Eventos { get; set; }

    public virtual DbSet<Instituição> Instituiçãos { get; set; }

    public virtual DbSet<Presenca> Presencas { get; set; }

    public virtual DbSet<TipoEvento> TipoEventos { get; set; }

    public virtual DbSet<TipoUsuario> TipoUsuarios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=EventPlus_Moura;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ComentarioEvento>(entity =>
        {
            entity.HasKey(e => e.IdComentarioEvento).HasName("PK__Comentar__4305A1F1BAB94D9D");

            entity.Property(e => e.IdComentarioEvento).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.IdEventoNavigation).WithMany(p => p.ComentarioEventos).HasConstraintName("FK__Comentari__IdEve__75A278F5");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.ComentarioEventos).HasConstraintName("FK__Comentari__IdUsu__76969D2E");
        });

        modelBuilder.Entity<Evento>(entity =>
        {
            entity.HasKey(e => e.IdEvento).HasName("PK__Evento__034EFC04B38E55EB");

            entity.Property(e => e.IdEvento).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.IdInstituicaoNavigation).WithMany(p => p.Eventos).HasConstraintName("FK__Evento__IdInstit__6D0D32F4");

            entity.HasOne(d => d.IdTipoEventoNavigation).WithMany(p => p.Eventos).HasConstraintName("FK__Evento__IdTipoEv__6C190EBB");
        });

        modelBuilder.Entity<Instituição>(entity =>
        {
            entity.HasKey(e => e.IdInstituicao).HasName("PK__Institui__B771C0D8E80833CF");

            entity.Property(e => e.IdInstituicao).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Presenca>(entity =>
        {
            entity.HasKey(e => e.IdPresenca).HasName("PK__Presenca__50FB6F5D46423728");

            entity.Property(e => e.IdPresenca).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.IdEventoNavigation).WithMany(p => p.Presencas).HasConstraintName("FK__Presenca__IdEven__70DDC3D8");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Presencas).HasConstraintName("FK__Presenca__IdUsua__71D1E811");
        });

        modelBuilder.Entity<TipoEvento>(entity =>
        {
            entity.HasKey(e => e.IdTipoEvento).HasName("PK__TipoEven__CDB3A3BE89D57E20");

            entity.Property(e => e.IdTipoEvento).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<TipoUsuario>(entity =>
        {
            entity.HasKey(e => e.IdTipoUsuario).HasName("PK__TipoUsua__CA04062BC2D3CA7B");

            entity.Property(e => e.IdTipoUsuario).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF9747BD2AB6");

            entity.Property(e => e.IdUsuario).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.IdTipoUsuarioNavigation).WithMany(p => p.Usuarios).HasConstraintName("FK__Usuario__IdTipoU__68487DD7");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
