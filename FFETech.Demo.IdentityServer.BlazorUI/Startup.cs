using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using FFETech.Demo.IdentityServer.BlazorUI.Data;
using FFETech.Demo.IdentityServer.Config;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FFETech.Demo.IdentityServer.BlazorUI
{
    public class Startup
    {
        #region Constructors

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        #region Properties

        public IConfiguration Configuration { get; }

        #endregion

        #region Public Methods

        public void ConfigureServices(IServiceCollection services)
        {
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            services.Configure<ForwardedHeadersOptions>(options => options.ForwardedHeaders = ForwardedHeaders.All);

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
                .AddCookie("Cookies")
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = $"http://localhost:{GlobalConfig.IdentityServerPortForClient}/auth";
                    options.RequireHttpsMetadata = false;

                    options.ClientId = GlobalConfig.BlazorClientId;
                    options.ClientSecret = GlobalConfig.BlazorClientSecret;
                    options.ResponseType = "code";

                    options.SaveTokens = true;

                    options.Events.OnRedirectToIdentityProvider = async n =>
                    {
                        n.ProtocolMessage.RedirectUri = $"http://localhost:{GlobalConfig.ProxyPort}/{GlobalConfig.BlazorClientId}/signin-oidc";
                        await Task.FromResult(0);
                    };
                });

            services.AddMvcCore(options =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UsePathBase(new PathString($"/{GlobalConfig.BlazorClientId}"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        #endregion
    }
}