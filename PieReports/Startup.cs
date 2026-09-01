using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PieReports.Encryption_Decryption;
using PieReports.Filters;
using Recaptcha.Web.Configuration;
using System;
using System.IO;
using System.Net.Http;
using ViewModel;

namespace PieReports
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
            services.AddHttpClient();


            /* ----To register Filter Globally-----
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<SessionTimeoutFilter>();
                // Or, if your filter has constructor dependencies resolved via DI:
                // options.Filters.Add(typeof(YourCustomFilter)); 
            }).AddRazorRuntimeCompilation();
            */

            services.AddControllersWithViews().AddRazorRuntimeCompilation();


            services.AddDetection();
			RecaptchaConfigurationManager.SetConfiguration(Configuration);
            services.AddDetectionCore().AddBrowser();
            services.AddRazorPages();
            services.AddSingleton<ITagHelperInitializer<ScriptTagHelper>, AppendVersionTagHelperInitializer>();
            services.AddSingleton<ITagHelperInitializer<LinkTagHelper>, AppendVersionTagHelperInitializer>();
            services.AddScoped<SessionTimeoutFilter>();
            services.AddMvc().AddSessionStateTempDataProvider();
            //    (options =>
            //{
            //    options.RespectBrowserAcceptHeader = true; // false by default
            //     .AddSessionStateTempDataProvider();
            //});
            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });

            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //.AddCookie(options =>
            //{
            //    options.LoginPath = "/Login/Index"; // Path to redirect to when not authenticated
            //    options.LogoutPath = "/Login/Logout"; // Path for logout
            //    //options.AccessDeniedPath = "/Account/AccessDenied"; // Path for access denied
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Set the session timeout
            //    options.SlidingExpiration = true; // Extend the cookie expiration with activity
            //});

            services.AddAntiforgery(options => {
                options.SuppressXFrameOptionsHeader = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            services.Configure<MintSettings>(Configuration.GetSection("MintSettings"));
			services.AddOptions();

		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            var path = Directory.GetCurrentDirectory();
            loggerFactory.AddFile($"{path}\\Logs\\Web.txt");
            app.UseSession();
            app.UseDetection();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.Use(async (context, next) => { context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN"); await next(); });
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthentication();
            //app.UseAuthorization();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
