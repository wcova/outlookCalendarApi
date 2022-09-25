using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using outlookCalendarApi.Domain.Dtos;
using System;
using System.Collections.Generic;

namespace Infrastructure.Boostrap
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var azureAD = new AzureADDto();
            configuration.GetSection("AzureAd").Bind(azureAD);

            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(x => x.FullName);
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "outlookCalendarApi", Version = "v1" });
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

            return services;
        }
    }
}
