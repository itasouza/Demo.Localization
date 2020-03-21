using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Globalization;

namespace Demo.Localization
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //lista de CultureInfo, que são as mesmas culturas que você gerou no arquivo de resource
            services.AddLocalization(options => options.ResourcesPath = "Resources").AddMvc().AddDataAnnotationsLocalization();



            //----------------------------------------------------------------

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //--------------------------------------------------------------------------
            //RequestLocationOptions, especificando as culturas suportadas 
            IList<CultureInfo> supportedCultures = new List<CultureInfo>
            {
               new CultureInfo("pt-BR"),
               new CultureInfo("en-US")
            };

            var opcoesDeCulturas = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            var requestProvider = new RouteDataRequestCultureProvider();
            opcoesDeCulturas.RequestCultureProviders.Insert(0, requestProvider);

            //--------------------------------------------------------------------------

            app.UseRequestLocalization(opcoesDeCulturas);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouter(routes =>
            {
                routes.MapMiddlewareRoute("{culture=pt-BR}/{*mvcRoute}", subApp =>
                {
                    subApp.UseRequestLocalization(opcoesDeCulturas);

                    subApp.UseMvc(mvcRoutes =>
                    {
                        mvcRoutes.MapRoute(
                            name: "default",
                            template: "{culture=pt-BR}/{controller=Home}/{action=Index}/{id?}");
                    });
                });
            });




        }
    }
}
