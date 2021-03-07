﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace WebAssignment_2019_P03_Team06
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Make MVC service available to the app   
            services.AddMvc();
            // Add a default in-memory implementation of distributed cache
            services.AddDistributedMemoryCache();
            // Add the session service 
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

           

            // Enable access to static files (css, js, images) in wwwroot   
            app.UseStaticFiles();

            // Enable session state      
            // IMPORTANT: This session call MUST go before UseMvc()     
            app.UseSession(); 

            // Define the default route of MVC
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");


            });

            

            /*
            Inside the call to UseMvc, MapRoute is used to create a default route.The route template "{controller=Home}/{action=Index}/{id?}":  { controller = Home}
            1)defines Home as the default controller  { action = Index}
            2)defines Index as the default action  { id ?}
            3)defines id as optional
            */


        }
    }
}
