using System.Globalization;
using Microsoft.AspNetCore.Authorization;
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
using TicketSaleCore.AuthorizationPolit.ResourceBased;
using TicketSaleCore.AuthorizationPolit.UserAndPassword;
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

        public IConfigurationRoot Configuration
        {
            get;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
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
            services.AddTransient<IUserValidator<AppUser>, MyUserValidator>();

            //Add localizaion based on Resx files
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            #region use EF in memory
            //  services.AddDbContext<ApplicationContext>(opt => opt.UseInMemoryDatabase());
            #endregion

            #region Use existing DB

            services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            #endregion

            #region Identity
            //Add Identity to services
            services.AddIdentity<AppUser, IdentityRole>()
                  .AddEntityFrameworkStores<ApplicationContext>()//comment if use MemoryUnitOfWork
                .AddDefaultTokenProviders();
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Operations.ClaimTypeForDbWork, x =>
                              {
                                  x.RequireClaim(Operations.ClaimTypeForDbWork);
                              });
            });

            services.AddScoped<IAuthorizationHandler, UserManagerAccesHander>();

            #endregion

            services.AddMvc(o => o.Conventions.Add(new FeatureConvention()))
                .AddRazorOptions(options =>
                {
                    options.ConfigureFeatureFolders();
                })
                // Add support for finding localized views, based on file name suffix, e.g. Index.fr.cshtml
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix,
                    opts =>
                    {
                        opts.ResourcesPath = "Resources";
                    })
                // Add support for localizing strings in data annotations (e.g. validation messages)
                .AddDataAnnotationsLocalization();

            //  services.AddScoped<LanguageActionFilter>();

            #region BLL Services
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<IEventService, EventService>();
            services.AddTransient<IOrdersService, OrdersService>();
            services.AddTransient<IOrderStatusService, OrderStatusService>();
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

            if(env.IsDevelopment())
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

            #region del data in db 

            using(var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
              var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationContext>();
              context.Database.ExecuteSqlCommand($"DELETE FROM dbo.Citys;" +
                                                 $"DELETE FROM dbo.Events;" +
                                                 $"DELETE FROM dbo.EventsTypes;" +
                                                 $"DELETE FROM dbo.Orders;" +
                                                 $"DELETE FROM dbo.OrderStatuses;" +
                                                 $"DELETE FROM dbo.Tickets;" +
                                                 $"DELETE FROM dbo.Venues;"
                                                 );
            //    context.Database.EnsureCreated();
            //    //    .Migrate();
                }
            #endregion
            #region Data base Init
            //User&role&Claim Init 
            DbInit.UserInit(app.ApplicationServices).Wait();
            //Db content init

        
 DbInit.AddTestData(applicationContext).Wait();
            
            #endregion
        }
    }
}