using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StreamNet.DomainEntities.Data;
using StreamNet.DomainEntities.Entities;
using StreamNet.ExtensionMethod;
using StreamNet.Server.Services;
using StreamNet.Options;
using System;
using System.Collections.Generic;
using StreamNet.ExtensionsMethods;
using Microsoft.Extensions.Hosting;

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
            var connectionString = OptionsFactory.GetConnectionString().DefaultConnection;

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    //Configuration.GetConnectionString("DefaultConnection")));
                    connectionString));

            services.AddIdentity<AppIdentityUser, IdentityRole<Guid>>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddSingleton<StreamCache>();
            services.AddTransient(typeof(FileStoreOptions), fso => new OptionsFactory().GetOptions<FileStoreOptions>());
            services.AddTransient(typeof(VideoRepositoryOptions), vro => new OptionsFactory().GetOptions<VideoRepositoryOptions>());

            services.AddMvc(o => o.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
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
                cfg.CreateMap<DomainEntities.Entities.VideoMetaData, Models.MediaReadViewModel>()
                    .ForMember(dest => dest.CoverArtBase64, opt => opt.MapFrom(src =>
                        string.Format("data:{0};base64,{1}", src.CoverArtContentType,
                            Convert.ToBase64String(src.CoverArt))));
                //to UserProfileViewModel
                cfg.CreateMap<DomainEntities.Entities.AppIdentityUser, Models.UserProfileViewModel>()
                    .ForMember(dest => dest.UserProfilePictureBase64, opt => opt.MapFrom(src =>
                        string.Format("data:{0};base64,{1}", src.UserProfilePictureFileType,
                            Convert.ToBase64String(src.UserProfilePicture))));
                //to EditVideoMetaDataViewModel
                cfg.CreateMap<DomainEntities.Entities.VideoMetaData, Models.EditVideoMetaDataViewModel>()
                    .ForMember(dest => dest.CoverArtBase64, opt => opt.MapFrom(src =>
                        string.Format("data:{0};base64,{1}", src.CoverArtContentType,
                            Convert.ToBase64String(src.CoverArt))));
                cfg.CreateMap<DomainEntities.Entities.VideoMetaData, Models.ChangeCoverArtModel>()
                    .ForMember(dest => dest.Image, opt => opt.MapFrom(src =>
                        string.Format("data:{0};base64,{1}", src.CoverArtContentType,
                            Convert.ToBase64String(src.CoverArt))));
                cfg.CreateMap<DomainEntities.Entities.AppIdentityUser, Models.ChangeCoverArtModel>()
                    .ForMember(dest => dest.Image, opt => opt.MapFrom(src =>
                        string.Format("data:{0};base64,{1}", src.UserProfilePictureFileType,
                            Convert.ToBase64String(src.UserProfilePicture))));

                /*********************************
                 *      ViewModel to Entity
                 */
                //to AppIdentityUser
                cfg.CreateMap<Models.CreateUserViewModel, DomainEntities.Entities.AppIdentityUser>();
                cfg.CreateMap<Models.UserProfileViewModel, DomainEntities.Entities.AppIdentityUser>();
                //To VideoMetaData
                cfg.CreateMap<Models.EditVideoMetaDataViewModel, DomainEntities.Entities.VideoMetaData>();
                //.ForMember(dest => dest.CoverArt, opt => opt.MapFrom(src => src.CoverArtFile.ToByteArray()))
                //.ForMember(dest => dest.CoverArtContentType, opt => opt.MapFrom(src => src.CoverArtFile.ContentType));
                cfg.CreateMap<Models.ChangeCoverArtModel, DomainEntities.Entities.VideoMetaData>()
                    .ForMember(dest => dest.CoverArt, opt => opt.MapFrom(src => src.NewImage.ToByteArray()))
                    .ForMember(dest => dest.CoverArtContentType, opt => opt.MapFrom(src => src.NewImage.ContentType));
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
                //routes.MapRoute(
                    //name: "apiroute",
                    //)
            });
        }
    }
}
