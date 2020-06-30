using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using FFETech.Demo.IdentityServer.Config;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FFETech.Demo.IdentityServer.RazorUI
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
                    options.Authority = $"http://localhost:{GlobalConfig.IdentityServerPortForClient}/{GlobalConfig.IdentityServerId}";
                    options.RequireHttpsMetadata = false;

                    options.ClientId = GlobalConfig.RazorClientId;
                    options.ClientSecret = GlobalConfig.RazorClientSecret;
                    options.ResponseType = "code";

                    options.UsePkce = true;
                    options.Scope.Add("profile");

                    options.SaveTokens = true;

                    options.Events.OnRedirectToIdentityProvider = async n =>
                    {
                        n.ProtocolMessage.RedirectUri = $"http://localhost:{GlobalConfig.ProxyPort}/{GlobalConfig.RazorClientId}/signin-oidc";
                        await Task.FromResult(0);
                    };
                });

            services.AddAuthorization();

            services.AddRazorPages().AddRazorPagesOptions(options =>
            {
                options.Conventions.AuthorizeFolder("/");
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UsePathBase(new PathString($"/{GlobalConfig.RazorClientId}"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }

        #endregion
    }
}