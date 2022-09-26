using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using outlookCalendarApi.Application.Boostrap;
using outlookCalendarApi.Infrastructure.Boostrap;
using System.IO;

namespace outlookCalendarApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var configurationBuilder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .AddEnvironmentVariables();

            Configuration = configurationBuilder.Build();

            services.AddApplication();
            services.AddInfrastructure(Configuration);

            services.AddCors(policy =>
            {
                policy.AddDefaultPolicy(options => options.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();
            app.UseRouting();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.OAuthClientId("07d060af-d6f7-407e-9999-e88a0960fb66");
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "outlookCalendarApi v1.0");
                //c.SwaggerEndpoint("/swagger/v{v}/swagger.json", "outlookCalendarApi v{v.V}"); for new verions
            });

            app.UseEndpoints(endpoints =>
            {
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            });
        }
    }
}
