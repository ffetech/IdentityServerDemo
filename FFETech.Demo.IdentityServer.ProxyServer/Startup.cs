using AspNetCore.Proxy;

using FFETech.Demo.IdentityServer.Config;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FFETech.Demo.IdentityServer.ProxyServer
{
    public class Startup
    {
        #region Public Methods

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddProxies();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseWebSockets();
            app.UseProxies(proxies =>
            {
                proxies.Map($"auth/{"{*endpoint}"}", proxy => proxy.UseHttp(
                        (context, args) =>
                        {
                            var queryString = context.Request.QueryString;
                            return $"http://localhost:{GlobalConfig.IdentityServerPort}/{GlobalConfig.IdentityServerId}/{args["endpoint"]}{queryString}";
                        }, builder => builder.WithHttpClientName("proxyClient"))
                        .UseWs(
                            (context, args) =>
                            {
                                var queryString = context.Request.QueryString;
                                return $"ws://localhost:{GlobalConfig.IdentityServerPort}/{GlobalConfig.IdentityServerId}/{args["endpoint"]}{queryString}";
                            })
                        );

                proxies.Map($"blazor/{"{*endpoint}"}", proxy => proxy.UseHttp(
                        (context, args) =>
                        {
                            var queryString = context.Request.QueryString;
                            return $"http://localhost:{GlobalConfig.BlazorPort}/{GlobalConfig.BlazorClientId}/{args["endpoint"]}{queryString}";
                        }, builder => builder.WithHttpClientName("proxyClient"))
                        .UseWs(
                            (context, args) =>
                            {
                                var queryString = context.Request.QueryString;
                                return $"ws://localhost:{GlobalConfig.BlazorPort}/{GlobalConfig.BlazorClientId}/{args["endpoint"]}{queryString}";
                            })
                        );

                proxies.Map($"razor/{"{*endpoint}"}", proxy => proxy.UseHttp(
                        (context, args) =>
                        {
                            var queryString = context.Request.QueryString;
                            return $"http://localhost:{GlobalConfig.RazorPort}/{GlobalConfig.RazorClientId}/{args["endpoint"]}{queryString}";
                        }, builder => builder.WithHttpClientName("proxyClient"))
                        .UseWs(
                            (context, args) =>
                            {
                                var queryString = context.Request.QueryString;
                                return $"ws://localhost:{GlobalConfig.RazorPort}/{GlobalConfig.RazorClientId}/{args["endpoint"]}{queryString}";
                            })
                        );
            });
        }

        #endregion
    }
}