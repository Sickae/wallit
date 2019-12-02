﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace WallIT.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((ctx, cfg) =>
                {
                    cfg.SetBasePath(Directory.GetCurrentDirectory());
                    cfg.AddJsonFile("appsettings.json", false, true);
                    cfg.AddEnvironmentVariables();
                }).UseStartup<Startup>();
    }
}
