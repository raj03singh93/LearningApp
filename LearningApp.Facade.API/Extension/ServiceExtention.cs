using LearningApp.Common.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace LearningApp.Facade.API.Extension
{
    public static class ServiceExtention
    {
        public static IServiceCollection AddCustomSwaggerDoc(this IServiceCollection services)
        {
            string bearer = "Bearer";
            // This code tell to generate swagger doc
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("Learning",  // {SwaggerDocName}
                    new OpenApiInfo()
                    {
                        Title = "Learning App",
                        Description = "This API helps to create learning data for core and related techs",
                        Version = "v1",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                        {
                            Email = "mail2awesomeDev@gmail.com",
                            Name = "Raj"
                        },
                        TermsOfService = new Uri("http://www.example.com"),
                        License = new Microsoft.OpenApi.Models.OpenApiLicense()
                        {
                            Name = "Free licencing",
                            Url = new Uri("https://www.example.com")
                        }
                    });
                swagger.AddSecurityDefinition(bearer, new OpenApiSecurityScheme()
                {
                    Name = "Jwt Authentication",
                    Description = "API Jwt auth",
                    Scheme = bearer,
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = bearer
                                }
                            },
                            new string[] {}
                    }
                });


                // Set the comments path for the Swagger JSON and UI.    
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                swagger.IncludeXmlComments(xmlPath);
            });
            return services;
        }
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, AppSetting appSetting)
        {

            if (string.IsNullOrWhiteSpace(appSetting.jwtSetting.Key)) 
                throw new ArgumentNullException(nameof(appSetting.jwtSetting.Key));

            if (string.IsNullOrWhiteSpace(appSetting.jwtSetting.Issuer)) 
                throw new ArgumentNullException(nameof(appSetting.jwtSetting.Issuer));

            services.AddAuthentication(option =>
                {
                    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(option => 
                {
                    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    { 
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = appSetting.jwtSetting.Issuer,
                        ValidAudience = appSetting.jwtSetting.Audiance ?? appSetting.jwtSetting.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSetting.jwtSetting.Key))
                    };
                });
            return services;
        }
    }
}
