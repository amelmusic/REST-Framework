using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public partial interface ISetupService
    {
        Task Run(object args = null);
    }
}
