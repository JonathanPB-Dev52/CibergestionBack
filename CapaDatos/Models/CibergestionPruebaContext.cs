using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace CapaDatos.Models
{
    public partial class CibergestionPruebaContext : DbContext
    {
        public readonly IConfiguration _configuration;
        public CibergestionPruebaContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public CibergestionPruebaContext(DbContextOptions<CibergestionPruebaContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public virtual DbSet<RolUsuario> RolUsuarios { get; set; } = null!;
        public virtual DbSet<TblCliente> TblClientes { get; set; } = null!;
        public virtual DbSet<TblRolAdmin> TblRolAdmins { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("CadenaSQL"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RolUsuario>(entity =>
            {
                entity.ToTable("RolUsuario");

                entity.Property(e => e.Rol)
                    .HasMaxLength(20)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TblCliente>(entity =>
            {
                entity.ToTable("Tbl_Clientes");

                entity.Property(e => e.Documento)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(d => d.IdRol);
            });

            modelBuilder.Entity<TblRolAdmin>(entity =>
            {
                entity.ToTable("Tbl_RolAdmin");

                entity.Property(e => e.Contra)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.NombreUsuario)
                    .HasMaxLength(40)
                    .IsFixedLength();

                entity.Property(d => d.IdRol);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
