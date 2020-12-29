using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EJCRUD4
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc(router =>
            {
                router.MapRoute(
                name: "default",
                template: "accounts/{controller=Account}/{action=Index}/{id?}");
            });
            app.UseMvc(router =>
            {
                router.MapRoute(
                name: "default2",
                template: "cuentas/{controller=Cuentas}/{action=Index}/{id?}");
            });
            app.UseMvc(router =>
            {
                router.MapRoute(
                name: "default3",
                template: "{controller=Main}/{action=Index}/{id?}");
            });
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Carlos Mencía Serna estubo aquí");
            });
        }
    }
}
