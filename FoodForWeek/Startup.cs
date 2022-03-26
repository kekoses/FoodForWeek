using FoodForWeek.Library.Services.Implementations;
using FoodForWeek.Library.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using FoodForWeek.Library.Models.Mapper;
using FoodForWeek.DAL.AppData.Repositories.Interfaces;
using FoodForWeek.DAL.AppData.Repositories.Implementations;
using FoodForWeek.DAL.Identity;
using FoodForWeek.DAL.Identity.Models;
using DAL=FoodForWeek.DAL;
using FoodForWeek.Filters;
using System.Net;
using System;

namespace FoodForWeek
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; init; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(options=>
            {
                options.Filters.Add<HeaderRenderFilter>();
            });
            services.AddDbContext<AppIdentityContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Identity"));
            });
            services.AddDbContext<DAL.AppContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
            });
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/ ";
                options.User.RequireUniqueEmail = true;
                options.Lockout.AllowedForNewUsers = false;
            }).AddEntityFrameworkStores<AppIdentityContext>()
              .AddDefaultTokenProviders();
            
            services.AddHsts(options=>
            {
                options.Preload=false;
                options.IncludeSubDomains=true;
                options.MaxAge=TimeSpan.FromDays(2);
            });
            services.AddHttpsRedirection(options=>
            {
                options.HttpsPort=5001;
                options.RedirectStatusCode=(int)HttpStatusCode.PermanentRedirect;
            });
            services.AddAutoMapper(config =>
            {
                config.AddProfile<ApplicationMapper>();
            });
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IIngredientRepository, IngredientRepository>();
            services.AddScoped<IDishRepository, DishRepository>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IUserService, UserService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
