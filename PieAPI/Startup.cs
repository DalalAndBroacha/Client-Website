using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PieAPI.Repository;
using PieAPI.Repository.Interface;
using System;
using System.IO;

namespace PieAPI
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
            services.AddControllers();
            services.AddDistributedMemoryCache();
            services.AddScoped<IExports, ExportsRepository>();
            services.AddScoped<ExportsRepository>();
            services.AddScoped<IServices, ServicesRepository>();
            services.AddScoped<ServicesRepository>();
            services.AddScoped<IDashboard, DashboardsRepository>();
            services.AddScoped<DashboardsRepository>();
            services.AddScoped<IDapperRepository, DapperRepository>();            
            services.AddScoped<DapperRepository>();
            services.AddScoped<IReports, ReportsRepository>();
            services.AddScoped<ReportsRepository>();
            services.AddScoped<IIPO, IPORepository>();
            services.AddScoped<IPORepository>();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });
            
            services.AddCors(option => option.AddPolicy("APIPolicy", builder => {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //app.UseCors();
            var path = Directory.GetCurrentDirectory();
            loggerFactory.AddFile($"{path}\\Logs\\Log.txt");
            app.UseSession();
            app.UseCors("CorsPolicy");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
