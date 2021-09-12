using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LearningApp.Facade.API
{
    public class Startup
    {
        private const string bearer = "Bearer";

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            // This code tell to generate swagger doc
            services.AddSwaggerGen(g =>
            {
                g.SwaggerDoc("Learning",  // {SwaggerDocName}
                    new Microsoft.OpenApi.Models.OpenApiInfo() 
                    { 
                        Title = "Learning App",
                        Description = "This API helps to create learning data for core and related techs",
                        Version = "v1",
                        Contact =  new Microsoft.OpenApi.Models.OpenApiContact()
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

                ////Security definition
                //g.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                //{
                //    Description = "JWT Authorization header {token}",
                //    Name = "Authorization",
                //    In = ParameterLocation.Header,
                //    Type = SecuritySchemeType.ApiKey,
                //    Scheme = "Bearer"
                //});

                ////Security binding - requirement
                //g.AddSecurityRequirement(new OpenApiSecurityRequirement()
                //{
                //    {
                //        new OpenApiSecurityScheme()
                //        {
                //            Reference = new OpenApiReference()
                //            {
                //                Type = ReferenceType.SecurityScheme,
                //                Id = "Bearer"
                //            },
                //            Scheme = "oauth2",
                //            Name = "Bearer",
                //            In =  ParameterLocation.Header
                //        },
                //        new string[] { bearer }
                //    }
                //});

                // Set the comments path for the Swagger JSON and UI.    
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                g.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            // tells to use the generated swagger doc
            app.UseSwagger();
            //Tells to use swagger UI
            app.UseSwaggerUI(a => {
                a.SwaggerEndpoint("/swagger/Learning/swagger.json", "Learning app"); // "/swagger/{SwaggerDocName}/swagger.json" 
                a.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
