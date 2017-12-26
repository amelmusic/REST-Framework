using A.Core.PermissionModule.Model;
using System.Data.Entity.ModelConfiguration;

namespace A.Core.PermissionModule.Services.Mapping
{
    public class RolePermissionMap : EntityTypeConfiguration<RolePermission>
    {
        public RolePermissionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("RolePermission", "permission");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RoleId).HasColumnName("RoleId");
            this.Property(t => t.PermissionId).HasColumnName("PermissionId");
            this.Property(t => t.IsAllowed).HasColumnName("IsAllowed");

            // Relationships
            this.HasRequired(t => t.Permission)
                .WithMany()
                .HasForeignKey(d => d.PermissionId);
            this.HasRequired(t => t.Role)
                .WithMany(t => t.RolePermissions)
                .HasForeignKey(d => d.RoleId);

        }
    }
}
