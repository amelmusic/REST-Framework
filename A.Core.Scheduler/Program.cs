using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/************************************************************************
 *
 *CHANGE quartz_jobs.xml properties -> Copy to output directory: CopyAlways
 * CHANGE language in SchedulerConfig.cs file. CURRENTLY IT's SET TO 'BS'
 *
 *
 * IF YOU NEED OAUTH2 Authentication,
 *      UNCOMMENT CODE IN AuthenticatorService and download IdentityModel nuget package
 ************************************************************************/
namespace A.Core.Scheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            Scheduler.Run();
        }
    }
}
