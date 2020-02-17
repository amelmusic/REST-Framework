using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using X.Core.Interface;
using X.Core.RESTClient.Core;

namespace Identity.Services
{
    public class EmailRESTClient : RESTClient
    {
        public EmailRESTClient(IActionContext actionContext, IConfiguration configuration) 
            : base("Common", "Email", actionContext, configuration)
        {
        }
    }
}
