using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Controllers.Account
{
    public class ForgotPasswordViewModel
    {
        public string Email { get; set; }
        public string SuccessMessage { get; set; }
    }
}
