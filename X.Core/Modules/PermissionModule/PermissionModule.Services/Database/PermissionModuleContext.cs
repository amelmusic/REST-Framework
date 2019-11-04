using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PermissionModule.Services.Database
{
    public partial class PermissionModuleContext : DbContext
    {
        public PermissionModuleContext()
        {
        }

        public PermissionModuleContext(DbContextOptions<PermissionModuleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<PermissionGroup> PermissionGroup { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RolePermission> RolePermission { get; set; }
        public virtual DbSet<RoleRelations> RoleRelations { get; set; }
        public virtual DbSet<RoleType> RoleType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=Core360_IdentityManager; Integrated Security = true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasIndex(e => e.PermissionGroupId)
                    .HasName("IX_PermissionGroupId");

                entity.Property(e => e.CreatedById).IsUnicode(false);

                entity.Property(e => e.ModifiedById).IsUnicode(false);

                entity.HasOne(d => d.PermissionGroup)
                    .WithMany(p => p.Permission)
                    .HasForeignKey(d => d.PermissionGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_permission.Permission_permission.PermissionGroup_PermissionGroupId");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.RoleTypeId)
                    .HasName("IX_RoleTypeId");

                entity.Property(e => e.CreatedById).IsUnicode(false);

                entity.Property(e => e.ModifiedById).IsUnicode(false);

                entity.HasOne(d => d.RoleType)
                    .WithMany(p => p.Role)
                    .HasForeignKey(d => d.RoleTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_permission.Role_permission.RoleType_RoleTypeId");
            });

            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.HasIndex(e => e.PermissionId)
                    .HasName("IX_PermissionId");

                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_RoleId");

                entity.Property(e => e.CreatedById).IsUnicode(false);

                entity.Property(e => e.ModifiedById).IsUnicode(false);

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.RolePermission)
                    .HasForeignKey(d => d.PermissionId)
                    .HasConstraintName("FK_permission.RolePermission_permission.Permission_PermissionId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RolePermission)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_permission.RolePermission_permission.Role_RoleId");
            });

            modelBuilder.Entity<RoleRelations>(entity =>
            {
                entity.HasIndex(e => e.ParentRoleId)
                    .HasName("IX_ParentRoleId");

                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_RoleId");

                entity.Property(e => e.CreatedById).IsUnicode(false);

                entity.Property(e => e.ModifiedById).IsUnicode(false);

                entity.HasOne(d => d.ParentRole)
                    .WithMany(p => p.RoleRelationsParentRole)
                    .HasForeignKey(d => d.ParentRoleId)
                    .HasConstraintName("FK_permission.RoleRelations_permission.Role_ParentRoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleRelationsRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_permission.RoleRelations_permission.Role_RoleId");
            });

            modelBuilder.Entity<RoleType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedById).IsUnicode(false);

                entity.Property(e => e.IsAllowedAssigningPermissionToRole).HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedById).IsUnicode(false);
            });
        }
    }
}
