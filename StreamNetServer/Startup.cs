using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StreamNetServer.Data;
using StreamNetServer.Entities;
using StreamNetServer.ExtensionMethod;
using StreamNetServer.Services;
using StreamNetServer.Services.Options;

namespace StreamNetServer
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.Configure<FileStoreOptions>(Configuration.GetSection("FileStoreOptions"));

            services.AddIdentity<AppIdentityUser, IdentityRole<Guid>>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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

            AutoMapper.Mapper.Initialize(cfg =>
            {
                //Entity to ViewModel
                cfg.CreateMap<Entities.VideoMetaData, Models.MediaReadViewModel>();
                cfg.CreateMap<Entities.AudioMetaData, Models.MediaReadViewModel>();
                cfg.CreateMap<Entities.AppIdentityUser, Models.UserProfileViewModel>();
                cfg.CreateMap<Entities.AppIdentityUser, Models.UserProfileViewModel>();

                //ViewModel to Entity
                cfg.CreateMap<Models.CreateUserViewModel, Entities.AppIdentityUser>();

                //General Data to models
                cfg.CreateMap<List<string>, Models.UserProfileViewModel>()
                    .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.ToArray().ConcatArray()));
                cfg.CreateMap<List<string>, Models.SetPermissionsViewModel>()
                    .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.ToArray().ConcatArray()));
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
