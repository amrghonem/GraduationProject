using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using GraduationProject.DataAccess;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GraduationProject.Data;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using GraduationProject.Services.Interfaces;
using GraduationProject.Services.Implementation;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net;
using GraduationProject.Infrastructure;

namespace GraduationProject.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);


            builder.AddEnvironmentVariables();
            Configuration = builder.Build();

        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            ///services.AddSignalR();

            services.AddMvc();

            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.Configure<IdentityOptions>(config => {
                config.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = ctx => {

                        if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == (int)HttpStatusCode.OK)
                        {
                            ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        }
                        else
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                        return Task.FromResult(0);
                    }
                };
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                //options.Cookies.ApplicationCookie.LoginPath = "/api/Login";
                //options.Cookies.ApplicationCookie.AccessDeniedPath = "/AccessDenied";
                //options.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents {
                //    OnRedirectToLogin = ctx => {
                       
                //    if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == (int)HttpStatusCode.OK)
                //        {
                //            ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                //        }
                //        else
                //        {
                //            ctx.Response.Redirect(ctx.RedirectUri);
                //        }
                //        return Task.FromResult(0);
                //    } 
                //};
                //options.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents
                //{
                //    OnRedirectToAccessDenied = ctx => {

                //        if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == (int)HttpStatusCode.OK)
                //        {
                //            ctx.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                //        }
                //        else
                //        {
                //            ctx.Response.Redirect(ctx.RedirectUri);
                //        }
                //        return Task.FromResult(0);
                //    }
                //};

            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options => {
                //Password Settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                //User Settings
                options.User.AllowedUserNameCharacters = "ض ذ ص ث ق ف غ ع ه خ ح ج د ش س ي ب ل ا ت ن م ك ط ئ ء ؤ رل لا ى ة و ز ظ  abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+. ";
                options.User.RequireUniqueEmail = true;
            });

            services.AddAuthorization(cfg =>
            {
                cfg.AddPolicy("Students", p => p.RequireClaim("Student", "True"));
                cfg.AddPolicy("Owners", p => p.RequireClaim("Owner", "True"));
                cfg.AddPolicy("Admins", p => p.RequireClaim("Admin", "True"));
            });



            //Dependancy Injection
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ISkillServices, SkillServices>();
            services.AddScoped<IMainCategoryService, MainCategoryService>();
            services.AddScoped<ISubCategoryService, SubCategoryService>();
            services.AddScoped<IStudentProfileService, StudentProfileService>();
            services.AddScoped<INewsFeedService, NewsFeedService>();
        }
        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });


            app.UseJwtBearerAuthentication(new JwtBearerOptions()
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidIssuer = "http://graduationProject.com",
                    ValidAudience = "http://graduationProject.com",
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("VERYLONGKEYVALUETHATISSECURITYGRADUATIONPROJECTDEMOFORSECURITY")),
                    ValidateLifetime = true
                }
            });


            app.UseMvc(routes =>
                routes.MapRoute(
                    name: "Default",
                    template: "{controller=Home}/{action=Index}/{Id?}"
                    ));

            app.UseIdentity();

            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            loggerFactory.AddConsole();

            //app.UseStatusCodePagesWithReExecute("/ApplicationHandler/Error/{0}");
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });

        }
    }
}
