using FFETech.Demo.IdentityServer.Config;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace FFETech.Demo.IdentityServer.ProxyServer
{
    public class Program
    {
        #region Public Static Methods

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls($"http://localhost:{GlobalConfig.ProxyPort}");
                    webBuilder.UseStartup<Startup>();
                });
        }

        #endregion
    }
}