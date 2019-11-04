using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace msc
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var fileName = envName == "Production" ? "D:/Logs/msC/msc.prod.log"
                            : (envName == "Staging" ? "D:/Logs/msC/msc.staging.log"
                            : (envName == "Development" ? "D:/Logs/msC/msc.devl.log"
                            : "S:/Logs/msc/msc.local.log"));
            var loggerConfiguration = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name)
                .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
                .Enrich.WithEnvironmentUserName()
                .Enrich.WithMachineName()
                .WriteTo.File(fileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 3
                        , outputTemplate: "{Timestamp:o} [{Level:u3}] ({Environment}/{ApplicationName}/{MachineName}/{EnvironmentUserName}) ({SourceContext} {MemberName}) {Message:lj}{NewLine}{Exception}")
                ;
#if DEBUG
            loggerConfiguration.Enrich.WithProperty("DebuggerAttached", Debugger.IsAttached);
#endif

            // When using ".UseSerilog()" it will use "Log.Logger".
            Log.Logger = loggerConfiguration.CreateLogger();

            try
            {
                Log.Information("***********************************Starting up {fileName}", fileName);
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config
                    .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                    .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                    //.AddJsonFile($"log.{hostingContext.HostingEnvironment.EnvironmentName}.json")
                    .AddEnvironmentVariables();
                })
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
