using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LMS.Models
{
    public partial class LMSContext : DbContext
    {
        public LMSContext()
        {
        }

        public LMSContext(DbContextOptions<LMSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Module> Modules { get; set; } = null!;
        public virtual DbSet<ModuleType> ModuleTypes { get; set; } = null!;
        public virtual DbSet<Test> Tests { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserType> UserTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=sql.bsite.net\\MSSQL2016;Database=acorderoumg_;User ID=acorderoumg_;Password=Acces1234;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Module>(entity =>
            {
                entity.HasKey(e => e.Uuid)
                    .HasName("PK__Module__65A475E75A7A08B2");

                entity.ToTable("Module");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("UUID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TipoModulo)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("Tipo_Modulo");

                entity.HasOne(d => d.TipoModuloNavigation)
                    .WithMany(p => p.Modules)
                    .HasForeignKey(d => d.TipoModulo)
                    .HasConstraintName("FK_Module.Tipo_Modulo");
            });

            modelBuilder.Entity<ModuleType>(entity =>
            {
                entity.HasKey(e => e.Uuid)
                    .HasName("PK__ModuleTy__65A475E7D6468FC4");

                entity.ToTable("ModuleType");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("UUID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.HasKey(e => e.Uuid)
                    .HasName("PK__Test__65A475E7A9CE1B32");

                entity.ToTable("Test");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("UUID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Modulo)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.Usuario)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.HasOne(d => d.ModuloNavigation)
                    .WithMany(p => p.Tests)
                    .HasForeignKey(d => d.Modulo)
                    .HasConstraintName("FK_Test.Modulo");

                entity.HasOne(d => d.UsuarioNavigation)
                    .WithMany(p => p.Tests)
                    .HasForeignKey(d => d.Usuario)
                    .HasConstraintName("FK_Test.Usuario");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Uuid)
                    .HasName("PK__User__65A475E74E0298BA");

                entity.ToTable("User");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("UUID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Contraseña)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TipoUsuario)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("Tipo_Usuario");

                entity.Property(e => e.Usuario)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.TipoUsuarioNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.TipoUsuario)
                    .HasConstraintName("FK_User.Tipo_Usuario");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.HasKey(e => e.Uuid)
                    .HasName("PK__UserType__65A475E7A620A090");

                entity.ToTable("UserType");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("UUID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
