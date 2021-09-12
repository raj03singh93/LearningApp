using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningApp.Facade.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = GetConfiguration(args);

            //Log.Logger = GetLoggerHardCoded();
            Log.Logger = GetLoggerFromConfiguration(config);
            try
            {
                Log.Information("Service Started");
                CreateHostBuilder(args).Build().Run();
                Log.Information("Service Stopped");
            }
            catch (Exception)
            {
                Log.Fatal("Application Crashed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();

        public static IConfiguration GetConfiguration(string[] args)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env ?? "Production"}.json", optional: true, reloadOnChange : true)
                .AddEnvironmentVariables()
                .Build();
        }
        public static Serilog.ILogger GetLoggerHardCoded()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Information() //set log level
                .WriteTo.Debug()
                .WriteTo.File(path: "D:/Logs/LearningApp-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
        public static Serilog.ILogger GetLoggerFromConfiguration(IConfiguration configuration)
        {
            return new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
    }
}
