using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Model.Requests
{
    partial class AspNetUsersUpdateRequest
    {
        public string Password { get; set; }
        public List<AspNetRolesUpsertRequest> Roles { get; set; } = new List<AspNetRolesUpsertRequest>();
    }
}
