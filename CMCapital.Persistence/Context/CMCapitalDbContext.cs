using System;
using System.Collections.Generic;
using CMCapital.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CMCapital.Persistence.Context;

public partial class CMCapitalDbContext : DbContext
{
    public CMCapitalDbContext()
    {
    }

    public CMCapitalDbContext(DbContextOptions<CMCapitalDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblCliente> TblClientes { get; set; }

    public virtual DbSet<TblProduto> TblProdutos { get; set; }

    public virtual DbSet<TblUsuario> TblUsuarios { get; set; }

    public virtual DbSet<TblVendum> TblVenda { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblCliente>(entity =>
        {
            entity.HasKey(e => e.ClienteId)
                .HasName("TblCliente_pk")
                .IsClustered(false);

            entity.ToTable("TblCliente");

            entity.HasIndex(e => e.ClienteId, "TblCliente_ClienteId_index");

            entity.Property(e => e.DthDelete)
                .HasColumnType("datetime")
                .HasColumnName("DTH_Delete");
            entity.Property(e => e.DthInsert)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("DTH_Insert");
            entity.Property(e => e.DthUpdate)
                .HasColumnType("datetime")
                .HasColumnName("DTH_Update");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SaldoDisponivel).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<TblProduto>(entity =>
        {
            entity.HasKey(e => e.ProdutoId)
                .HasName("TblProduto_pk")
                .IsClustered(false);

            entity.ToTable("TblProduto");

            entity.HasIndex(e => e.ProdutoId, "TblProduto_ProdutoId_index");

            entity.Property(e => e.DthDelete)
                .HasColumnType("datetime")
                .HasColumnName("DTH_Delete");
            entity.Property(e => e.DthInsert)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("DTH_Insert");
            entity.Property(e => e.DthUpdate)
                .HasColumnType("datetime")
                .HasColumnName("DTH_Update");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Preco).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<TblUsuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId)
                .HasName("TblUsuario_pk")
                .IsClustered(false);

            entity.ToTable("TblUsuario");

            entity.HasIndex(e => e.UsuarioId, "TblUsuario_UsuarioId_index");

            entity.Property(e => e.Cpf)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("CPF");
            entity.Property(e => e.DthDelete)
                .HasColumnType("datetime")
                .HasColumnName("DTH_Delete");
            entity.Property(e => e.DthInsert)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("DTH_Insert");
            entity.Property(e => e.DthUpdate)
                .HasColumnType("datetime")
                .HasColumnName("DTH_Update");
            entity.Property(e => e.Senha)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblVendum>(entity =>
        {
            entity.HasKey(e => e.VendaId)
                .HasName("TblVenda_pk")
                .IsClustered(false);

            entity.HasIndex(e => e.ClienteId, "TblVenda_ClienteId_index");

            entity.HasIndex(e => e.ProdutoId, "TblVenda_ProdutoId_index");

            entity.HasIndex(e => e.VendaId, "TblVenda_VendaId_index");

            entity.Property(e => e.DthDelete)
                .HasColumnType("datetime")
                .HasColumnName("DTH_Delete");
            entity.Property(e => e.DthInsert)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("DTH_Insert");
            entity.Property(e => e.DthUpdate)
                .HasColumnType("datetime")
                .HasColumnName("DTH_Update");

            entity.HasOne(d => d.Cliente).WithMany(p => p.TblVenda)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TblVenda_TblCliente_ClienteId_fk");

            entity.HasOne(d => d.Produto).WithMany(p => p.TblVenda)
                .HasForeignKey(d => d.ProdutoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TblVenda_TblProduto_ProdutoId_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
