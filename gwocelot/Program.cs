using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Ocelot.Middleware;
using Ocelot.DependencyInjection;

namespace gwocelot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)

                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config
                    .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                    //.AddJsonFile("appsettings.json", true, true)
                    .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                    //.AddJsonFile("ocelot.json")
                    .AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json")
                    .AddEnvironmentVariables();

                })
                .UseStartup<Startup>()
                .ConfigureServices(s =>
                {
                    s.AddOcelot();
                })
                .Configure(app =>
                {
                    app.UseOcelot().Wait();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    //add your logging
                })
                .UseIISIntegration()
                .Build();
        }



        //public static IWebHost BuildWebHost(string[] args)
        //{
        //    return WebHost.CreateDefaultBuilder(args)
        //                  .ConfigureAppConfiguration((hostingContext, config) =>
        //                  {
        //                      config.AddJsonFile("ocelot.json")
        //                      .AddEnvironmentVariables();
        //                  })
        //                  .UseStartup<Startup>()
        //                  .ConfigureServices(s =>
        //                  {
        //                      s.AddOcelot();
        //                  })
        //                  .Configure(app =>
        //                  {
        //                      app.UseOcelot().Wait();
        //                  })
        //                  .ConfigureLogging((hostingContext, logging) =>
        //                  {
        //                      //add your logging
        //                  })
        //                  .UseIISIntegration()
        //                  .Build();
        //}

    }
}
