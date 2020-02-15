using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.API.Controllers
{
    public partial class AspNetUsersController
    {
        [HttpGet]
        [Route("{id}/ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromRoute]string id, [FromQuery]string Token)
        {
            var result = await _mainService.ConfirmEmail(id, Token);
            return Redirect(result);
        }
    }
}
