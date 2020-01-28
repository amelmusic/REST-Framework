using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApiTemplate.Interfaces;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ApiTemplate.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var seed = args.Contains("/seed");
            if (seed)
            {
                args = args.Except(new[] { "/seed" }).ToArray();
            }

            var runWithSeed = args.Contains("/runwithseed");
            if (runWithSeed)
            {
                args = args.Except(new[] { "/runwithseed" }).ToArray();
            }

            var host = CreateWebHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var autoUpdate = scope.ServiceProvider.GetRequiredService<IConfiguration>()["Application:AutoSetupOnStartup"];
                if (seed || (autoUpdate != null && autoUpdate.Equals("true", StringComparison.OrdinalIgnoreCase)))
                {
                    var service = scope.ServiceProvider.GetRequiredService<ISetupService>();
                    var result = service.Run();
                    result.Wait();
                    //NOTE: You call sequentially one by one combined module here.
                }
            }
            if (!seed || runWithSeed)
            {
                host.Run();
            }

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging((hostingContext, logging) =>
                {
                    if (hostingContext.HostingEnvironment.EnvironmentName == "Development")
                    {
                        logging.AddDebug();
                        logging.AddConsole();
                    }
                    //hostingContext.Configuration
                    // The ILoggingBuilder minimum level determines the
                    // the lowest possible level for logging. The log4net
                    // level then sets the level that we actually log at.
                    logging.AddLog4Net();
                    logging.SetMinimumLevel(LogLevel.Debug);
                });
    }
}
