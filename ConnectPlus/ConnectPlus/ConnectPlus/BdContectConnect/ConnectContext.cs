using System;
using System.Collections.Generic;
using ConnectPlus.Models;
using Microsoft.EntityFrameworkCore;

namespace ConnectPlus.BdContectConnect;

public partial class ConnectContext : DbContext
{
    public ConnectContext()
    {
    }

    public ConnectContext(DbContextOptions<ConnectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Contato> Contatos { get; set; }

    public virtual DbSet<TipoContato> TipoContatos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ConnectPlus;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contato>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Contato__3214EC077204C20F");

            entity.HasOne(d => d.IdTipoContato).WithMany(p => p.Contatos).HasConstraintName("FK__Contato__TipoCon__66603565");
        });

        modelBuilder.Entity<TipoContato>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TipoCont__3214EC079A9AB2E1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
