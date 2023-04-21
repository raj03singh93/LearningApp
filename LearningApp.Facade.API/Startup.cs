using LearningApp.Common.Model;
using LearningApp.Facade.API.Extension;
using LearningApp.Service.Extension;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LearningApp.Facade.API
{
    public class Startup
    {
        private IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            AppSetting appSetting = new AppSetting();
            Configuration.Bind(appSetting);

            services.AddAppService();
            services.AddSingleton(appSetting);
            services.AddCustomSwaggerDoc();
            services.AddJwtAuthentication(appSetting);
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
