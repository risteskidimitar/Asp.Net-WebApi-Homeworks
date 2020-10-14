using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ToDo.App.Services;

namespace ToDo.App
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
            // get connection string
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            // register data access and services dependencies
            DiModule.RegisterModule(services, connectionString);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //Swagger

            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "V1";
                    document.Info.Title = "SEDC TODO app";
                    document.Info.Description = "This is our first ToDo api app";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Dimitar Risteski",
                        Email = "risteski.dimitar@gmail.com"
                    };
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseMvc();
        }
    }
}
