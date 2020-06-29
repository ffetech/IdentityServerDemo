using FFETech.Demo.IdentityServer.Config;
using FFETech.Demo.IdentityServer.Identity;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FFETech.Demo.IdentityServer
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
            services.Configure<ForwardedHeadersOptions>(options => options.ForwardedHeaders = ForwardedHeaders.All);

            services.AddTransient<IUserStore<DemoUser>, DemoUserStore>();
            services.AddTransient<DemoUserStore>();
            services.AddTransient<IRoleStore<DemoRole>, DemoRoleStore>();
            services.AddTransient<DemoRoleStore>();

            services.AddIdentity<DemoUser, DemoRole>().AddDefaultTokenProviders();

            var identityServer = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            });

            identityServer.AddInMemoryIdentityResources(ServerConfig.Ids);
            identityServer.AddInMemoryApiResources(ServerConfig.Apis);
            identityServer.AddInMemoryClients(ServerConfig.Clients);
            identityServer.AddAspNetIdentity<DemoUser>();
            identityServer.AddDeveloperSigningCredential();

            services.AddRazorPages();
            services.AddMvc();
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UsePathBase(new PathString($"/{GlobalConfig.IdentityServerId}"));

            app.UseForwardedHeaders();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/home/error");

            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
            });
        }

        #endregion
    }
}