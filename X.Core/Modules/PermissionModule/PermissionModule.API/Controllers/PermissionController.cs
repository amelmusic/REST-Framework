using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PermissionModule.Interfaces;
using PermissionModule.Model;
using PermissionModule.Model.Requests;

namespace PermissionModule.API
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class PermissionCheckerController
    {
        public PermissionCheckerController(IPermissionChecker checker)
        {
            PermissionChecker = checker;
        }

        public IPermissionChecker PermissionChecker { get; set; }

        [HttpGet("Check")]
        public async Task<PermissionCheckResult> Check([FromQuery]PermissionCheckRequest request)
        {
            //throw new Exception("AMEL");
            var result = await PermissionChecker.IsAllowed(request);

            return result;
        }
    }
}
