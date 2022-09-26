using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using outlookCalendarApi.Application.Settings;
using outlookCalendarApi.Infrastructure.Clients;
using outlookCalendarApi.Infrastructure.Clients.Interfaces;
using System;
using System.Collections.Generic;

namespace outlookCalendarApi.Infrastructure.Boostrap
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var azureAD = new AzureAD();
            configuration.GetSection("AzureAd").Bind(azureAD);

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(x => x.FullName);
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "outlookCalendarApi v1.0", Version = "1.0" });
                //c.SwaggerDoc("v{v}", new OpenApiInfo { Title = "outlookCalendarApi v{v.V}", Version = "{v.V}" }); for new versions
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the oauth2 schema",
                    Name = "oauth2",
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(azureAD.Instance + azureAD.Endpoint_Authorize, UriKind.RelativeOrAbsolute),
                            Scopes = new Dictionary<string, string>
                            {
                                { $"api://{azureAD.ClientId}/{azureAD.Scope}", azureAD.Scope },
                            },
                        }
                    }

                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oauth2"
                            },
                        },
                        new string[]{ azureAD.Scope }
                    }
                });

            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(configuration.GetSection("AzureAd"));

            services.AddHttpClient();

            services.AddScoped<IGraphClient, GraphClient>();

            return services;
        }
    }
}
