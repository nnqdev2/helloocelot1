using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace msone
{
    public class Program
    {
        public static int Main(string[] args)
        {

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddJsonFile("appsettings.local.json", optional: true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name)
                .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
                .Enrich.WithEnvironmentUserName()
                .CreateLogger();

//#if DEBUG
//            loggerConfiguration.Enrich.WithProperty("DebuggerAttached", Debugger.IsAttached);
//#endif

            // When using ".UseSerilog()" it will use "Log.Logger".
            // Log.Logger = loggerConfiguration.CreateLogger();

            //Log.Logger = new LoggerConfiguration()
            //    .Enrich.FromLogContext()
            //    .WriteTo.Console()
            //    //.WriteTo.Console(new RenderedCompactJsonFormatter())
            //    .WriteTo.File(new RenderedCompactJsonFormatter(), "s:/logs/msone/msone.local.log")
            //    .CreateLogger();

            try
            {
                Log.Information("***********************************Starting up");
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
                    .AddJsonFile("appsettings.local.json", true, true)
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



















    //public class Program
    //{
    //    public static void Main(string[] args)
    //    {
    //        Log.Logger = new LoggerConfiguration()
    //            .Enrich.FromLogContext()
    //            .WriteTo.Console()
    //            //.WriteTo.Console(new RenderedCompactJsonFormatter())
    //            .WriteTo.File(new RenderedCompactJsonFormatter(), "s:/logs/msone/msone.local.log")
    //            .CreateLogger();

    //        try
    //        {
    //            Log.Information("***********************************Starting up");
    //            CreateHostBuilder(args).Build().Run();
    //        }
    //        catch (Exception ex)
    //        {
    //            Log.Fatal(ex, "Application start-up failed");
    //        }
    //        finally
    //        {
    //            Log.CloseAndFlush();
    //        }
    //    }

    //    public static IHostBuilder CreateHostBuilder(string[] args) =>
    //        Host.CreateDefaultBuilder(args)
    //            .ConfigureAppConfiguration((hostingContext, config) =>
    //            {
    //                config
    //                .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
    //                .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
    //                .AddJsonFile($"log.{hostingContext.HostingEnvironment.EnvironmentName}.json")
    //                .AddEnvironmentVariables();
    //            })
    //            .UseSerilog()
    //            .ConfigureWebHostDefaults(webBuilder =>
    //            {
    //                webBuilder.UseStartup<Startup>();
    //            });
    //}
}





