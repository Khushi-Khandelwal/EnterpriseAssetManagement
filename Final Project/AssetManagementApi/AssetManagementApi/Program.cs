using Microsoft.AspNetCore;
using NLog.Extensions.Logging;
namespace AssetManagementApi
{
    public class Program
    {
        static void Main()
        {
            CreateHostBuilder().Build().Run();
        }
        public static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder().ConfigureWebHostDefaults(webHost =>
            {
                webHost.UseStartup<Startup>();
            });
        }
    }
        /*  public static IHostBuilder CreateHostBuilder(string[] args) =>
         WebHost.CreateDefaultBuilder(args)
         .ConfigureLogging((hostingContext, logging) =>
         {
             logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
             logging.AddConsole();
             logging.AddDebug();
             logging.AddEventSourceLogger();
             // Enable NLog as one of the Logging Provider
             logging.AddNLog();
         })
         .UseStartup<Startup>();*//*
        public static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder().ConfigureWebHostDefaults((WebHost,logging) =>
            {
                logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                logging.AddConsole();
                logging.AddDebug();
                logging.AddEventSourceLogger();
                // Enable NLog as one of the Logging Provider
                logging.AddNLog();
                webHost.UseStartup<Startup>();
            });
        }
    }
    */
        /*  public class Program
          {
              public static void Main(string[] args)
              {
                  CreateWebHostBuilder(args).Build().Run();
              }

              public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
                  WebHost.CreateDefaultBuilder(args)
                  .ConfigureLogging((hostingContext, logging) =>
                  {
                      logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                      logging.AddConsole();
                      logging.AddDebug();
                      logging.AddEventSourceLogger();
                      // Enable NLog as one of the Logging Provider
                      logging.AddNLog();
                  })
                  .UseStartup<Startup>();
          }*/

    }