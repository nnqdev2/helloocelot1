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

namespace msa
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var fileName = envName == "Production" ? "D:/Logs/msA/msa.prod.log" 
                            : (envName == "Staging" ? "D:/Logs/msA/msa.staging.log" 
                            : (envName == "Development" ? "D:/Logs/msA/msa.devl.log" 
                            : "S:/Logs/msa/msa.local.log"));
            var loggerConfiguration = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name)
                .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
                .Enrich.WithEnvironmentUserName()
                .Enrich.WithMachineName()
                .WriteTo.File(fileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 3
                        , outputTemplate: "{Timestamp:o} [{Level:u3}] ({Environment}/{ApplicationName}/{MachineName}/{EnvironmentUserName}) ({SourceContext} {MemberName}) {Message:lj}{NewLine}{Exception}")
                ;
            //.CreateLogger();

            //Log.Logger = new LoggerConfiguration()
            //    .Enrich.FromLogContext()
            //    .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name)
            //    .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
            //    .Enrich.WithEnvironmentUserName()
            //    .Enrich.WithMachineName()
            //    //.WriteTo.File("s:/logs/msa/msa.inline.log", rollingInterval: RollingInterval.Day, retainedFileCountLimit:3)
            //    .WriteTo.File(fileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 3
            //            , outputTemplate: "{Timestamp:o} [{Level:u3}] ({Environment}/{ApplicationName}/{MachineName}/{EnvironmentUserName}) ({SourceContext} {MemberName}) {Message:lj}{NewLine}{Exception}")
            //    .CreateLogger();
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



//namespace msa
//{
//    public class Program
//    {
//        public static int Main(string[] args)
//        {
//            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
//            var fileName = envName == "Production" ? "D:/Logs/msA/msa.prod.log"
//                            : (envName == "Staging" ? "D:/Logs/msA/msa.staging.log"
//                            : (envName == "Development" ? "D:/Logs/msA/msa.devl.log"
//                            : "S:/Logs/msa/msa.local.log"));
//            Log.Logger = new LoggerConfiguration()
//                .Enrich.FromLogContext()
//                .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name)
//                .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
//                .Enrich.WithEnvironmentUserName()
//                .Enrich.WithMachineName()
//                //.WriteTo.File("s:/logs/msa/msa.inline.log", rollingInterval: RollingInterval.Day, retainedFileCountLimit:3)
//                .WriteTo.File(fileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 3
//                        , outputTemplate: "{Timestamp:o} [{Level:u3}] ({Environment}/{ApplicationName}/{MachineName}/{EnvironmentUserName}) ({SourceContext} {MemberName}) {Message:lj}{NewLine}{Exception}")
//                .CreateLogger();

//            //#if DEBUG
//            //            loggerConfiguration.Enrich.WithProperty("DebuggerAttached", Debugger.IsAttached);
//            //#endif

//            // When using ".UseSerilog()" it will use "Log.Logger".
//            // Log.Logger = loggerConfiguration.CreateLogger();

//            try
//            {
//                Log.Information("***********************************Starting up {fileName}", fileName);
//                CreateHostBuilder(args).Build().Run();
//                return 0;
//            }
//            catch (Exception ex)
//            {
//                Log.Fatal(ex, "Application start-up failed");
//                return 1;
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }

//        public static IHostBuilder CreateHostBuilder(string[] args) =>
//            Host.CreateDefaultBuilder(args)
//                .ConfigureAppConfiguration((hostingContext, config) =>
//                {
//                    config
//                    .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
//                    .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
//                    //.AddJsonFile($"log.{hostingContext.HostingEnvironment.EnvironmentName}.json")
//                    .AddEnvironmentVariables();
//                })
//                .UseSerilog()
//                .ConfigureWebHostDefaults(webBuilder =>
//                {
//                    webBuilder.UseStartup<Startup>();
//                });
//    }
//}


//namespace msa
//{
//    public class Program
//    {
//        public static int Main(string[] args)
//        {

//            var configuration = new ConfigurationBuilder()
//                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
//                .Build();

//            Log.Logger = new LoggerConfiguration()
//                .ReadFrom.Configuration(configuration)
//                .Enrich.FromLogContext()
//                .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name)
//                .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
//                .Enrich.WithEnvironmentUserName()
//                .CreateLogger();

//            //#if DEBUG
//            //            loggerConfiguration.Enrich.WithProperty("DebuggerAttached", Debugger.IsAttached);
//            //#endif

//            // When using ".UseSerilog()" it will use "Log.Logger".
//            // Log.Logger = loggerConfiguration.CreateLogger();

//            //Log.Logger = new LoggerConfiguration()
//            //    .Enrich.FromLogContext()
//            //    .WriteTo.Console()
//            //    //.WriteTo.Console(new RenderedCompactJsonFormatter())
//            //    .WriteTo.File(new RenderedCompactJsonFormatter(), "s:/logs/msone/msone.local.log")
//            //    .CreateLogger();

//            try
//            {
//                Log.Information("***********************************Starting up");
//                CreateHostBuilder(args).Build().Run();
//                return 0;
//            }
//            catch (Exception ex)
//            {
//                Log.Fatal(ex, "Application start-up failed");
//                return 1;
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }

//        public static IHostBuilder CreateHostBuilder(string[] args) =>
//            Host.CreateDefaultBuilder(args)
//                .ConfigureAppConfiguration((hostingContext, config) =>
//                {
//                    config
//                    .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
//                    .AddJsonFile("appsettings.local.json", true, true)
//                    .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
//                    //.AddJsonFile($"log.{hostingContext.HostingEnvironment.EnvironmentName}.json")
//                    .AddEnvironmentVariables();
//                })
//                .UseSerilog()
//                .ConfigureWebHostDefaults(webBuilder =>
//                {
//                    webBuilder.UseStartup<Startup>();
//                });
//    }
//}


