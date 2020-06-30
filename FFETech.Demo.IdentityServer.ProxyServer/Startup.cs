using System;
using FFETech.Demo.IdentityServer.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

using ProxyKit;

namespace FFETech.Demo.IdentityServer.ProxyServer
{
    public class Startup
    {
        #region Public Methods

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddProxy();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            //app.UseRouting();

            app.UseWebSockets();

            app.UseWebSocketProxy(context =>
            {
                string forward = "ws://localhost";

                if (context.Request.Path.StartsWithSegments($"/{GlobalConfig.IdentityServerId}"))
                    forward = $"ws://localhost:{GlobalConfig.IdentityServerPort}";

                if (context.Request.Path.StartsWithSegments($"/{GlobalConfig.RazorClientId}"))
                    forward = $"ws://localhost:{GlobalConfig.RazorPort}";

                if (context.Request.Path.StartsWithSegments($"/{GlobalConfig.BlazorClientId}"))
                    forward = $"ws://localhost:{GlobalConfig.BlazorPort}";

                return new Uri(forward);
            },
                options => options.AddXForwardedHeaders());

            app.RunProxy(context =>
            {
                string forward = "http://localhost";

                if (context.Request.Path.StartsWithSegments($"/{GlobalConfig.IdentityServerId}"))
                    forward = $"http://localhost:{GlobalConfig.IdentityServerPort}";

                if (context.Request.Path.StartsWithSegments($"/{GlobalConfig.RazorClientId}"))
                    forward = $"http://localhost:{GlobalConfig.RazorPort}";

                if (context.Request.Path.StartsWithSegments($"/{GlobalConfig.BlazorClientId}"))
                    forward = $"http://localhost:{GlobalConfig.BlazorPort}";

                return context.ForwardTo(forward).AddXForwardedHeaders().Send();
            });
        }

        #endregion
    }
}