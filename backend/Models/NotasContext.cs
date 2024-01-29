using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NotasEnsolvers.Models
{
    public partial class NotasContext : DbContext
    {
        public NotasContext()
        {
        }

        public NotasContext(DbContextOptions<NotasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categorium> Categoria { get; set; } = null!;
        public virtual DbSet<Nota> Notas { get; set; } = null!;
        public virtual DbSet<NotasCategoria> NotasCategorias { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
             
             

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categorium>(entity =>
            {
                entity.HasKey(e => e.IdCategoria)
                    .HasName("PK__Categori__A3C02A10EB3E901F");

                entity.Property(e => e.Color).HasMaxLength(50);
            });

            modelBuilder.Entity<Nota>(entity =>
            {
                entity.HasKey(e => e.IdNota)
                    .HasName("PK__Notas__4B2ACFF2DABD840F");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Nota)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK__Notas__IdCategor__38996AB5");
            });

            modelBuilder.Entity<NotasCategoria>(entity =>
            {
                entity.HasKey(e => e.IdNotaCategoria)
                    .HasName("PK__NotasCat__00287517F06A812F");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.NotasCategoria)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK__NotasCate__IdCat__4AB81AF0");

                entity.HasOne(d => d.IdNotaNavigation)
                    .WithMany(p => p.NotasCategoria)
                    .HasForeignKey(d => d.IdNota)
                    .HasConstraintName("FK__NotasCate__IdNot__49C3F6B7");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
