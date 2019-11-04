using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using X.Core.Interface;
using X.Core.Model;

namespace X.Core.WebAPI.Core
{
    public class PermissionCheckController : BaseController
    {
        public PermissionCheckController(IPermissionChecker checker)
        {
            PermissionChecker = checker;
        }

        public IPermissionChecker PermissionChecker { get; set; }

        [HttpGet("Check")]
        public async Task<bool> Check([FromQuery]PermissionCheckRequest request)
        {
            //throw new Exception("AMEL");
            var result = await PermissionChecker.IsAllowed(request);

            return result;
        }
    }
}
