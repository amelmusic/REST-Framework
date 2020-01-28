using ApiTemplate.Interfaces;
using ApiTemplate.Services.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiTemplate.Services
{
    public partial class SetupService : ISetupService
    {
        public virtual ApiTemplateContext Context { get; set; }
        public virtual async Task Run(object args = null)
        {
            await Context.Database.MigrateAsync();

            OnRunPartial();
        }

        /// <summary>
        /// Populate this method for additional seed, eg. call remote services etc
        /// </summary>
        partial void OnRunPartial();
    }
}
