using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Model.Requests
{
    partial class AspNetUsersInsertRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string GivenName { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string FamilyName { get; set; }
        public List<string> Roles { get; set; }
    }
}
