using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StreamNet.Server.DomainEntities.Data;
using StreamNet.Server.DomainEntities.Entities;
using StreamNet.Server.ExtensionMethod;
using StreamNet.Server.Services;
using StreamNet.Server.Options;
using System;
using System.Collections.Generic;

namespace StreamNet.Server
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

            services.AddIdentity<AppIdentityUser, IdentityRole<Guid>>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddTransient<MediaStreamFactory>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            AutoMapper.Mapper.Initialize(cfg =>
            {
                /*********************************
                 *      Entity to ViewModel
                 */               
                //to MediaReadViewModel 
                cfg.CreateMap<DomainEntities.Entities.VideoMetaData, Models.MediaReadViewModel>();
                cfg.CreateMap<DomainEntities.Entities.AudioMetaData, Models.MediaReadViewModel>();
                //to UserProfileViewModel
                cfg.CreateMap<DomainEntities.Entities.AppIdentityUser, Models.UserProfileViewModel>();
                cfg.CreateMap<DomainEntities.Entities.AppIdentityUser, Models.UserProfileViewModel>();
                //to EditVideoMetaDataViewModel
                cfg.CreateMap<DomainEntities.Entities.VideoMetaData, Models.EditVideoMetaDataViewModel>();

                /*********************************
                 *      ViewModel to Entity
                 */
                //to AppIdentityUser
                cfg.CreateMap<Models.CreateUserViewModel, DomainEntities.Entities.AppIdentityUser>();
                cfg.CreateMap<Models.UserProfileViewModel, DomainEntities.Entities.AppIdentityUser>();
                //To VideoMetaData
                cfg.CreateMap<Models.EditVideoMetaDataViewModel, DomainEntities.Entities.VideoMetaData>();
                /*********************************
                 *      Data to ViewModel
                 */
                //to UserProfileViewModel
                cfg.CreateMap<List<string>, Models.UserProfileViewModel>()
                    .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.ToArray().ConcatArray()));
                //to SetPermissionsViewModel
                cfg.CreateMap<List<string>, Models.SetPermissionsViewModel>()
                    .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.ToArray().ConcatArray()));
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
