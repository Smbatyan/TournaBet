using System.Reflection;
using Host.ModuleIntegration;
using Host.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Module = Host.ModuleIntegration.Module;

namespace Host;

public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Your API Name",
                    Version = "v1",
                    Description = "Description of your API"
                });

                // Define the XML comments file path (optional)
                string xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                c.IncludeXmlComments(xmlCommentsPath);
            });

            services.AddControllers().ConfigureApplicationPartManager(manager =>
            {
                // Clear all auto detected controllers.
                manager.ApplicationParts.Clear();

                // Add feature provider to allow "internal" controller
                manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
            });

            // Register a convention allowing to us to prefix routes to modules.
            services.AddTransient<IPostConfigureOptions<MvcOptions>, ModuleRoutingMvcOptionsPostConfigure>();

            services.AddModule<TournaBet.Auth.Startup>("auth/api");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                // c.RoutePrefix = string.Empty; // Set the Swagger UI at the root URL
            });
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            // Adds endpoints defined in modules
            var modules = app.ApplicationServices.GetRequiredService<IEnumerable<Module>>();
            foreach (var module in modules)
            {
                app.Map($"/{module.RoutePrefix}", builder =>
                {
                    builder.UseRouting();
                    module.Startup.Configure(builder, env);
                });
            }
        }
    }