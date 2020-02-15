using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    partial interface IAspNetUsersService
    {
        Task<string> ConfirmEmail(string id, string code);
    }
}
