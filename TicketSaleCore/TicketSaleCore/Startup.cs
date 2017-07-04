﻿using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TicketSaleCore.AuthorizationPolit.Password;
using TicketSaleCore.Models.BLL.Interfaces;
using TicketSaleCore.Models.BLL.Services;
using TicketSaleCore.Models.DAL;
using TicketSaleCore.Models.DAL.IRepository;
using TicketSaleCore.Models.DAL._Ef;
using TicketSaleCore.Models.Entities;

namespace TicketSaleCore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //use HTTPS
            //services.Configure<MvcOptions>(options =>
            //{
            //    options.Filters.Add(new RequireHttpsAttribute());
            //});

            //use localization

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en"),
                    new CultureInfo("ru"),
                    new CultureInfo("be")
                };

                options.DefaultRequestCulture = new RequestCulture(culture: "en", uiCulture: "en");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;

                options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(async context =>
                {
                    // My custom request culture logic
                    return new ProviderCultureResult("en");
                }));
            });



            //Add Pasword validator
            services.AddTransient<IPasswordValidator<AppUser>,
                CustomPasswordValidator>(serv => new CustomPasswordValidator(5));

            //Add localizaion based on Resx files
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            #region use EF in memory
            services.AddDbContext<ApplicationContext>(opt => opt.UseInMemoryDatabase());
            #endregion
            #region Use existing DB

            /*
             *services.AddDbContext<ApplicationContext>(options =>
             * options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
             */
            #endregion

            #region deCommend if use MemoryUnitOfWork

            //var userStore = new UserStoreWef();
            //var roleStore = new RoleStoreWef();


            // services.AddSingleton<IUserStore<AppUser>>(userStore);
            // services.AddSingleton<IUserPasswordStore<AppUser>>(userStore);
            // services.AddSingleton<IUserRoleStore<AppUser>>(userStore);
            // services.AddSingleton<IRoleStore<IdentityRole>>(roleStore);

            //  services.AddAuthentication();
            //  

            //https://github.com/timschreiber/Mvc5IdentityExample/tree/master/Mvc5IdentityExample
            //http://techbrij.com/generic-repository-unit-of-work-entity-framework-unit-testing-asp-net-mvc
            //https://aspnetboilerplate.com/Pages/Documents/Unit-Of-Work

            #endregion




            #region Identity
            //Add Identity to services
            services.AddIdentity<AppUser, IdentityRole>()
                  // .AddUserStore<UserStoreWef>()
                  // .AddRoleStore<RoleStoreWef>()
                  .AddEntityFrameworkStores<ApplicationContext>()//comment if use MemoryUnitOfWork
                .AddDefaultTokenProviders();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("age-policy", x => {
                    x.RequireClaim("age");
                });
                options.AddPolicy("Read", x =>
                {
                    x.RequireClaim("crud","r");
                });
                options.AddPolicy("delete", x =>
                {
                    x.RequireClaim("crud", "d");
                });
            });
            
            #endregion

            services.AddMvc(o=>o.Conventions.Add(new FeatureConvention()))
                .AddRazorOptions(options =>
                {
                    options.ConfigureFeatureFolders();
                })
                // Add support for finding localized views, based on file name suffix, e.g. Index.fr.cshtml
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix,
                    opts => { opts.ResourcesPath = "Resources"; })
                // Add support for localizing strings in data annotations (e.g. validation messages)
                .AddDataAnnotationsLocalization();

            //  services.AddScoped<LanguageActionFilter>();

            #region BLL Services
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<IEventService, EventService>();
            services.AddTransient<IOrdersService, OrdersService>();
            services.AddTransient<IOrderStatusService, OrdersService>();
            services.AddTransient<ITicketsService, TicketsService>();
            services.AddTransient<IVenuesService, VenuesService>();
            #endregion



            #region IUnitOfWork serv
            services.AddScoped<IUnitOfWork, ApplicationContext>();
            // services.AddSingleton<IUnitOfWork, MemoryUnitOfWork>(); 
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                IHostingEnvironment env,
                ILoggerFactory loggerFactory,
                IUnitOfWork applicationContext)
        {
            //Logger settings
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //Available localization
            var supportedCultures = new[]
            {
                new CultureInfo("en"),
                new CultureInfo("ru"),
                new CultureInfo("be")
            };
            //Add Localization (default is en-US)
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }


            app.UseStaticFiles();
            //use Identity
            app.UseIdentity();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


            #region DbInit
            //User&role Init 
            DbInit.UserInit(app.ApplicationServices).Wait();
            //Db content init
            DbInit.AddTestData(applicationContext).Wait();
            #endregion
        }
     }
}