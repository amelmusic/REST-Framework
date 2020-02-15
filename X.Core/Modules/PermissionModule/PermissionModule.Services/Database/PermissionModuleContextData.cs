using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace PermissionModule.Services.Database
{
    partial class PermissionModuleContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleType>()
               .HasData(new RoleType()
               {
                   Id = 1,
                   Name = "User type",
                   Code = "USER_TYPE",
                   IsMultipleAllowed = false,
                   PermissionHierarchyLevel = 1,
                   IsAllowedAssigningToUsers = true,
                   CreatedById = "1",
                   ModifiedById = "1",
                   CreatedOn = DateTime.Now,
                   ModifiedOn = DateTime.Now,
                   IsAllowedAssigningPermissionToRole = true,
                   IsAllowedMatchingOnSameLevel = true
               });

            modelBuilder.Entity<Role>()
               .HasData(new Role()
               {
                   Id = 1,
                   Name = "Super Admin",
                   OwnerPermission = "*",
                   Description = "The one who manages the system",
                   RoleTypeId = 1,
                   CreatedById = "1",
                   ModifiedById = "1",
                   CreatedOn = DateTime.Now,
                   ModifiedOn = DateTime.Now,
               });

            modelBuilder.Entity<Role>()
               .HasData(new Role()
               {
                   Id = 2,
                   Name = "User",
                   OwnerPermission = "*",
                   Description = "User with limited permissions",
                   RoleTypeId = 1,
                   CreatedById = "1",
                   ModifiedById = "1",
                   CreatedOn = DateTime.Now,
                   ModifiedOn = DateTime.Now,
               });

            modelBuilder.Entity<Role>()
               .HasData(new Role()
               {
                   Id = 3,
                   Name = "Admin",
                   OwnerPermission = "*",
                   Description = "Admin with somewhat limited functionalities",
                   RoleTypeId = 1,
                   CreatedById = "1",
                   ModifiedById = "1",
                   CreatedOn = DateTime.Now,
                   ModifiedOn = DateTime.Now,
               });

            modelBuilder.Entity<PermissionGroup>()
               .HasData(new PermissionGroup()
               {
                   Id = 1,
                   Name = "System",
                   CreatedById = "1",
                   ModifiedById = "1",
                   CreatedOn = DateTime.Now,
                   ModifiedOn = DateTime.Now,
               });

            modelBuilder.Entity<Permission>()
               .HasData(new Permission()
               {
                   Id = 1,
                   Name = "*",
                   Description = "Root permission",
                   IsAllowed = false, //everything is denied by default
                   OperationType = null,
                   OwnerPermission = "PermissionModule.Permission.Insert",
                   PermissionGroupId = 1,
                   CreatedById = "1",
                   ModifiedById = "1",
                   CreatedOn = DateTime.Now,
                   ModifiedOn = DateTime.Now,
               });

            modelBuilder.Entity<Permission>()
               .HasData(new Permission()
               {
                   Id = 2,
                   Name = "PermissionModule.Permission.*",
                   Description = "Permission - view data",
                   IsAllowed = false, //everything is denied by default
                   OperationType = "View",
                   OwnerPermission = "PermissionModule.Permission.Insert",
                   PermissionGroupId = 1,
                   CreatedById = "1",
                   ModifiedById = "1",
                   CreatedOn = DateTime.Now,
                   ModifiedOn = DateTime.Now,
               });

            modelBuilder.Entity<Permission>()
               .HasData(new Permission()
               {
                   Id = 3,
                   Name = "PermissionModule.Permission.*",
                   Description = "Permission - edit data",
                   IsAllowed = false, //everything is denied by default
                   OperationType = "Edit",
                   OwnerPermission = "PermissionModule.Permission.Insert",
                   PermissionGroupId = 1,
                   CreatedById = "1",
                   ModifiedById = "1",
                   CreatedOn = DateTime.Now,
                   ModifiedOn = DateTime.Now,
               });
            
            modelBuilder.Entity<Permission>()
               .HasData(new Permission()
               {
                   Id = 4,
                   Name = "PermissionModule.Permission.*",
                   Description = "Permission - delete data",
                   IsAllowed = false, //everything is denied by default
                   OperationType = "Delete",
                   OwnerPermission = "PermissionModule.Permission.Insert",
                   PermissionGroupId = 1,
                   CreatedById = "1",
                   ModifiedById = "1",
                   CreatedOn = DateTime.Now,
                   ModifiedOn = DateTime.Now,
               });

            //By default, for admin we allow everything
            modelBuilder.Entity<RolePermission>()
               .HasData(new RolePermission()
               {
                   Id = 1,
                   IsAllowed = true,
                   PermissionId = 1,
                   RoleId = 1,
                   CreatedById = "1",
                   ModifiedById = "1",
                   CreatedOn = DateTime.Now,
                   ModifiedOn = DateTime.Now,
               });

        }
    }
}
