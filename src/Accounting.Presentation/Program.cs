using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using Autofac.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureAppConfiguration((builderContext, config) =>
                    {
                        config.AddJsonFile("autofac.json");
                        config.AddEnvironmentVariables();
                    })
                    .UseSerilog((hostingContext, config) =>
                    {
                        config.MinimumLevel.Debug()
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                        .Enrich.FromLogContext()
                        .WriteTo.RollingFile("logs/log={Date}.log");
                    }).ConfigureServices(services => services.AddAutofac());
                });
    }
}
