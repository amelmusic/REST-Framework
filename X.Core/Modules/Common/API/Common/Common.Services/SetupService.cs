using Common.Interfaces;
using Common.Services.Database;
using Microsoft.EntityFrameworkCore;
using PermissionModule.Services.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services
{
    public partial class SetupService : ISetupService
    {
        public virtual CommonContext Context { get; set; }
        public virtual PermissionModuleContext PermissionModuleContext { get; set; }
        public virtual async Task Run(object args = null)
        {
            await Context.Database.MigrateAsync();

            //additional data for permission module
            var permissionContent = "Common.StaticData.Content";
            if (!PermissionModuleContext.Permission.Where(x => x.Name == permissionContent).Any())
            {
                PermissionModuleContext.Permission.Add(new Permission()
                {
                    Name = permissionContent,
                    IsAllowed = true,
                    OperationType = "View",
                    OwnerPermission = "PermissionModule.Permission.Insert",
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now
                });
            }
            PermissionModuleContext.SaveChanges();
            OnRunPartial();
        }

        /// <summary>
        /// Populate this method for additional seed, eg. call remote services etc
        /// </summary>
        partial void OnRunPartial();
    }
}
