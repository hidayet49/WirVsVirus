using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WeVsVirus.DataAccess.DatabaseContext;
// using Microsoft.Extensions.Logging.ApplicationInsights;

namespace WeVsVirus.WebApp
{

    public static class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                /*.UseUrls("http://*:5000")*/
                .UseStartup<Startup>()
                // .ConfigureLogging((hostingContext, logging) =>
                // {
                //     var configSectionForApplicationInsights = hostingContext.Configuration.GetSection("Logging:ApplicationInsights");
                //     if (!string.IsNullOrWhiteSpace(configSectionForApplicationInsights["InstrumentationKey"]))
                //     {
                //         logging.AddApplicationInsights(configSectionForApplicationInsights["InstrumentationKey"]?.ToString() ?? "");
                //         logging.AddFilter<ApplicationInsightsLoggerProvider>("", Enum.Parse<LogLevel>(configSectionForApplicationInsights["LogLevel:Default"] ?? "Error"));
                //     }
                // })
            .ConfigureKestrel((context, options) =>
            {
                // Set properties and call methods on options
            });
    }
}
