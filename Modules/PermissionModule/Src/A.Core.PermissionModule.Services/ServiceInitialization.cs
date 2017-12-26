using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A.Core.Interface;
using A.Core.PermissionModule.Services.Migrations;
using Microsoft.Practices.Unity;

namespace A.Core.PermissionModule.Services
{
    public class ServiceInitialization : IServicesRegistration
    {
        public ServiceInitialization()
        {
            Priority = 0;
        }

        public void Register(ref UnityContainer container)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Context, A.Core.PermissionModule.Services.Migrations.Configuration>());
        }

        public int Priority { get; set; }
    }
   
}
