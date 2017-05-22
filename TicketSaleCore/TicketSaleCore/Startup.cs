using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TicketSaleCore.Models;
using TicketSaleCore.AuthorizationPolit.Password;

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
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en"),
                    new CultureInfo("ru"),
                    new CultureInfo("ja"),
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




            services.AddTransient<IPasswordValidator<User>,
                CustomPasswordValidator>(serv => new CustomPasswordValidator(5));


            services.AddLocalization(options => options.ResourcesPath = "Resources");


            services.AddDbContext<ApplicationContext>(opt => opt.UseInMemoryDatabase());

            // services.AddDbContext<ApplicationContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();


            services.AddMvc()
                // Add support for finding localized views, based on file name suffix, e.g. Index.fr.cshtml
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix,
                    opts => { opts.ResourcesPath = "Resources"; })
                // Add support for localizing strings in data annotations (e.g. validation messages)
                .AddDataAnnotationsLocalization();

            /* services.AddMvc()
                 .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix,
                     opts => { opts.ResourcesPath = "Resources"; })
                 .AddDataAnnotationsLocalization();*/
            //.AddDataAnnotationsLocalization();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();


            var supportedCultures = new[]
            {
                new CultureInfo("en"),
                new CultureInfo("ru"),
                new CultureInfo("ja"),
                new CultureInfo("be")
            };

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

            app.UseIdentity();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            // инициализация базы данных

            DatabaseInitialize(app.ApplicationServices).Wait();
            AddTestData(app.ApplicationServices.GetService<ApplicationContext>());
        }

        private static void AddTestData(ApplicationContext context)
        {
            context.CityDbSet.Add(new City { Name = "Minsk" });
            context.CityDbSet.Add(new City { Name = "Gomel" });
            context.CityDbSet.Add(new City { Name = "Grodno" });
            context.CityDbSet.Add(new City { Name = "Vitebsk" });
            context.CityDbSet.Add(new City { Name = "Brest" });
            context.CityDbSet.Add(new City { Name = "Mogilev" });

            context.StatusDbSet.Add(new Status { StatusName = "Wiating for conformation" });
            context.StatusDbSet.Add(new Status { StatusName = "Confirmed" });
            context.StatusDbSet.Add(new Status { StatusName = "Rejected" });

          
          /*  context.UserDbSet.Add(new TicketSaleCore.Models.User
            {
                FirstName = "Firstname2",
                LastName = "LastName2",
                Localization = "ru-RU",
                Address = "adress1",
                PhoneNumber = "5-53-53-56"
            });
            context.UserDbSet.Add(new TicketSaleCore.Models.User
            {
                FirstName = "Firstname3",
                LastName = "LastName3",
                Localization = "ru-RU",
                Address = "adress1",
                PhoneNumber = "5-53-53-56"
            });
            context.UserDbSet.Add(new TicketSaleCore.Models.User
            {
                FirstName = "Firstname4",
                LastName = "LastName4",
                Localization = "ru-RU",
                Address = "adress1",
                PhoneNumber = "5-53-53-56"
            });
            context.UserDbSet.Add(new TicketSaleCore.Models.User
            {
                FirstName = "Firstname5",
                LastName = "LastName5",
                Localization = "ru-RU",
                Address = "adress1",
                PhoneNumber = "5-53-53-56"
            });
            context.UserDbSet.Add(new TicketSaleCore.Models.User
            {
                FirstName = "Firstname6",
                LastName = "LastName6",
                Localization = "ru-RU",
                Address = "adress1",
                PhoneNumber = "5-53-53-56"
            });
            context.UserDbSet.Add(new TicketSaleCore.Models.User
            {
                FirstName = "Firstname7",
                LastName = "LastName7",
                Localization = "ru-RU",
                Address = "adress1",
                PhoneNumber = "5-53-53-56"
            });
            context.UserDbSet.Add(new TicketSaleCore.Models.User
            {
                FirstName = "Firstname8",
                LastName = "LastName8",
                Localization = "ru-RU",
                Address = "adress1",
                PhoneNumber = "5-53-53-56"
            });
            context.UserDbSet.Add(new TicketSaleCore.Models.User
            {
                FirstName = "Firstname9",
                LastName = "LastName9",
                Localization = "ru-RU",
                Address = "adress1",
                PhoneNumber = "5-53-53-56"
            });
            */
            context.EventDbSet.Add(new Event
            {
                Name = "Event1",
                Date = DateTime.Now,
                Banner = null,
                Description = 1,
                Venue = new Venue
                {
                    Name = "Venue1",
                    Address = "Address1",
                    City = context.CityDbSet.Local.First(p => p.Name.Equals("Minsk")),
                }
            });
            context.EventDbSet.Add(new Event
            {
                Name = "Event2",
                Date = DateTime.Now,
                Banner = null,
                Description = 1,
                Venue = new Venue
                {
                    Name = "Venue2",
                    Address = "Address2",
                    City = context.CityDbSet.Local.First(p => p.Name.Equals("Gomel")),
                }
            });
            context.EventDbSet.Add(new Event
            {
                Name = "Event3",
                Date = DateTime.Now,
                Banner = null,
                Description = 1,
                Venue = new Venue
                {
                    Name = "Venue3",
                    Address = "Address3",
                    City = context.CityDbSet.Local.First(p => p.Name.Equals("Grodno")),
                }
            });
            context.SaveChanges();
        }

        public async Task DatabaseInitialize(IServiceProvider serviceProvider)
        {


            UserManager<User> userManager =
                serviceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string adminEmail = "admin@gmail.com";
            string password = "Admin";
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { Email = adminEmail, UserName = adminEmail, EmailConfirmed = true };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }

                User us1 =new User
                {
                    Email = "User1@me.com",
                    UserName = "User1@me.com",
                    EmailConfirmed = true,
                    FirstName = "Firstname1",
                    LastName = "LastName1",
                    Localization = "ru-RU",
                    Address = "adress1",
                    PhoneNumber = "5-53-53-56"
                };
                IdentityResult result1 = await userManager.CreateAsync(us1, password);
                if (result1.Succeeded)
                {
                    await userManager.AddToRoleAsync(us1, "user");
                }
            }
        }
    }
}