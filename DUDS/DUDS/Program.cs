using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace DUDS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureKestrel((context, options) =>
                    {
                        // Handle requests up to 400 MB
                        options.Limits.MaxRequestBodySize = 4194304000;
                        //3000000000
                        options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(20);
                    })
                    .UseStartup<Startup>();
                });
    }
}
